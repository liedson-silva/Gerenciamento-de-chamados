using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Gerenciamento_De_Chamados; 

namespace Gerenciamento_De_Chamados.Services
{
    public class ChamadoService
    {

        private readonly IChamadoRepository _chamadoRepo;
        private readonly IArquivoRepository _arquivoRepo;
        private readonly IEmailService _emailService;
        private readonly IAIService _aiService;
        private readonly IHistoricoRepository _historicoRepo; 


        public ChamadoService(
            IChamadoRepository chamadoRepo,
            IArquivoRepository arquivoRepo,
            IEmailService emailService,
            IAIService aiService,
            IHistoricoRepository historicoRepository) 
        {
            _chamadoRepo = chamadoRepo;
            _arquivoRepo = arquivoRepo;
            _emailService = emailService;
            _aiService = aiService;
            _historicoRepo = historicoRepository; 
        }

        /// <summary>
        /// 1. Cria o chamado inicial no banco de dados com status 'Pendente' e salva anexos.
        /// </summary>
        /// <returns>O ID do chamado criado.</returns>
        public async Task<int> CriarChamadoBaseAsync(Chamado novoChamado, byte[] arquivoBytes, string nomeAnexo, string tipoAnexo)
        {
            // O chamado é salvo com status "Pendente" e sem sugestões da IA
            novoChamado.PrioridadeSugeridaIA = "Em análise";
            novoChamado.ProblemaSugeridoIA = "Em análise";
            novoChamado.SolucaoSugeridaIA = "Em análise";
            novoChamado.PrioridadeChamado = "Analise"; 
            novoChamado.StatusChamado = "Pendente";

            // 1. Salvar o Chamado
            int idChamado = await _chamadoRepo.AdicionarAsync(novoChamado);
            if (idChamado == -1)
            {
                throw new Exception("Não foi possível salvar o chamado no banco de dados.");
            }

            // 2. Salvar o Arquivo (se existir)
            if (arquivoBytes != null && arquivoBytes.Length > 0)
            {
                Arquivo novoArquivo = new Arquivo
                {
                    TipoArquivo = tipoAnexo,
                    NomeArquivo = nomeAnexo,
                    ArquivoBytes = arquivoBytes,
                    FK_IdChamado = idChamado
                };
                await _arquivoRepo.AdicionarAsync(novoArquivo);
            }

            return idChamado;
        }

        /// <summary>
        /// Envia o e-mail de confirmação de abertura para o usuário (usando o método correto).
        /// </summary>
        public async Task EnviarConfirmacaoUsuarioAsync(Chamado chamado)
        {
            // Assumindo que a informação básica do usuário está na SessaoUsuario
            Usuario usuario = new Usuario
            {
                Nome = SessaoUsuario.Nome,
                Email = SessaoUsuario.Email
            };

            // Usando o método de e-mail de confirmação do usuário conforme a interface fornecida
            await _emailService.EnviarEmailConfirmacaoUsuarioAsync(chamado, usuario, chamado.IdChamado);
        }

        /// <summary>
        /// 2. Executa a análise da IA, atualiza o chamado no banco, registra a nota interna no histórico e envia o e-mail para o TI (usando o método correto).
        /// </summary>
        public async Task ProcessarAnaliseEAtualizarAsync(int idChamado, Chamado dadosChamado, byte[] arquivoBytes, string nomeAnexo)
        {
            string problemaIA = "Não identificado";
            string prioridadeIA = "Não identificado";
            string solucaoIA = "Nenhuma";

            // 1. Lógica da IA
            try
            {
                var solucoesAnteriores = await _chamadoRepo.BuscarSolucoesAnterioresAsync(dadosChamado.Categoria);
                var (problema, prioridade, solucao) = await _aiService.AnalisarChamado(
                    dadosChamado.Titulo, dadosChamado.PessoasAfetadas, dadosChamado.OcorreuAnteriormente,
                    dadosChamado.ImpedeTrabalho, dadosChamado.Descricao, dadosChamado.Categoria,
                    solucoesAnteriores);

                problemaIA = problema;
                prioridadeIA = prioridade;
                solucaoIA = solucao;

                // 2. Atualizar o Chamado no Banco com os dados da IA
                await _chamadoRepo.AtualizarSugestoesIAAsync(idChamado, prioridade, problema, solucao);

                // 3. Atualizar o objeto 'dadosChamado' (necessário para o e-mail da TI)
                dadosChamado.IdChamado = idChamado;
                dadosChamado.PrioridadeSugeridaIA = prioridade;
                dadosChamado.ProblemaSugeridoIA = problema;
                dadosChamado.SolucaoSugeridaIA = solucao;
                dadosChamado.PrioridadeChamado = prioridade;


                Historico historicoIA = new Historico
                {
                    DataSolucao = DateTime.Now,
                    Solucao = $"Nota interna: {solucaoIA.Trim()}",
                    FK_IdChamado = idChamado,
                    Acao = "Nota Interna"
                };

                // Utiliza o método sem transação para salvar a nota interna
                await _historicoRepo.AdicionarSemTransacaoAsync(historicoIA);
            }
            catch (Exception aiEx)
            {
                // Se a IA falhar, registra a falha nos campos de sugestão do chamado.
                Console.WriteLine($"Falha na análise de IA para Chamado #{idChamado}: {aiEx.Message}");

                problemaIA = "Falha IA";
                prioridadeIA = "Falha IA";
                solucaoIA = $"A triagem da IA falhou: {aiEx.Message}";

                // Atualiza o chamado no banco com a falha
                await _chamadoRepo.AtualizarSugestoesIAAsync(idChamado, prioridadeIA, problemaIA, solucaoIA);
            }

            // 5. Enviar o Email para a TI 
            Usuario usuario = new Usuario
            {
                Nome = SessaoUsuario.Nome,
                Email = SessaoUsuario.Email
            };
            
            await _emailService.EnviarEmailNovoChamadoTIAsync(dadosChamado, usuario, idChamado, arquivoBytes, nomeAnexo);
        }
    }
}