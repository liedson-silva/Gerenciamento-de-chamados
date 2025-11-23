using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using System;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Services
{

    public class ChamadoService
    {
        private readonly IChamadoRepository _chamadoRepo;
        private readonly IArquivoRepository _arquivoRepo;
        private readonly IEmailService _emailService;
        private readonly IAIService _aiService;

        public ChamadoService(
            IChamadoRepository chamadoRepo,
            IArquivoRepository arquivoRepo,
            IEmailService emailService, 
            IAIService aiService)
        {
            _chamadoRepo = chamadoRepo;
            _arquivoRepo = arquivoRepo;
            _emailService = emailService; 
            _aiService = aiService;
        }


        public async Task<int> CriarChamadoBaseAsync(Chamado novoChamado, byte[] arquivoBytes, string nomeAnexo, string tipoAnexo)
        {
            // O chamado é salvo com status "Pendente" e sem dados da IA
            novoChamado.PrioridadeSugeridaIA = "Em análise";
            novoChamado.ProblemaSugeridoIA = "Em análise";
            novoChamado.SolucaoSugeridaIA = "Em análise";
            novoChamado.PrioridadeChamado = "Analise";

            // 1. Salvar o Chamado
            int idChamado = await _chamadoRepo.AdicionarAsync(novoChamado);
            if (idChamado == -1)
            {
                throw new Exception("Não foi possível salvar o chamado no banco de dados.");
            }


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

        public async Task EnviarConfirmacaoUsuarioAsync(Chamado chamado, Usuario usuario, int idChamado)
        {
            // O serviço agora delega a chamada para o _emailService
            await _emailService.EnviarEmailConfirmacaoUsuarioAsync(chamado, usuario, idChamado);
        }

        public async Task ProcessarAnaliseEAtualizarAsync(int idChamado, Chamado dadosChamado, Usuario usuario, byte[] arquivoBytes, string nomeAnexo, string tipoAnexo)
        {


            // 1. Lógica da IA
            try
            {
                var solucoesAnteriores = await _chamadoRepo.BuscarSolucoesAnterioresAsync(dadosChamado.Categoria);
                var (problema, prioridade, solucao) = await _aiService.AnalisarChamado(
                    dadosChamado.Titulo, dadosChamado.PessoasAfetadas, dadosChamado.OcorreuAnteriormente,
                    dadosChamado.ImpedeTrabalho, dadosChamado.Descricao, dadosChamado.Categoria,
                    solucoesAnteriores);

                // 2. Atualizar o Chamado no Banco com os dados da IA
                await _chamadoRepo.AtualizarSugestoesIAAsync(idChamado, prioridade, problema, solucao);

                // 3. Atualizar o objeto 'dadosChamado' para enviar o email correto
                dadosChamado.PrioridadeSugeridaIA = prioridade;
                dadosChamado.ProblemaSugeridoIA = problema;
                dadosChamado.SolucaoSugeridaIA = solucao;
                dadosChamado.PrioridadeChamado = prioridade;
            }
            catch (Exception aiEx)
            {
                Console.WriteLine($"Falha na análise de IA para Chamado #{idChamado}: {aiEx.Message}");
                
                dadosChamado.PrioridadeSugeridaIA = "Falha IA";
                dadosChamado.ProblemaSugeridoIA = "Falha IA";
                dadosChamado.SolucaoSugeridaIA = aiEx.Message; 
            }


            await _emailService.EnviarEmailNovoChamadoTIAsync(dadosChamado, usuario, idChamado, arquivoBytes, nomeAnexo);
        }
    }
}
