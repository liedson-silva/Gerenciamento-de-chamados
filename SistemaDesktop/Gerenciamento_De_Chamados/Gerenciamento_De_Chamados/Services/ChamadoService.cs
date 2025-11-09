using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using System;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Services
{
    // Esta é a classe que VAMOS testar.
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

        // Movemos a lógica do seu "btnConcluirCH_Click" para cá
        public async Task<int> CriarNovoChamadoAsync(Chamado novoChamado, byte[] arquivoBytes, string nomeAnexo, string tipoAnexo)
        {
            // 1. Lógica da IA 
            try
            {
                var solucoesAnteriores = await _chamadoRepo.BuscarSolucoesAnterioresAsync(novoChamado.Categoria);
                var (problema, prioridade, solucao) = await _aiService.AnalisarChamado(
                    novoChamado.Titulo, novoChamado.PessoasAfetadas, novoChamado.OcorreuAnteriormente,
                    novoChamado.ImpedeTrabalho, novoChamado.Descricao, novoChamado.Categoria, solucoesAnteriores);

                novoChamado.PrioridadeSugeridaIA = prioridade;
                novoChamado.ProblemaSugeridoIA = problema;
                novoChamado.SolucaoSugeridaIA = solucao;
            }
            catch (Exception)
            {
                // Não para o processo, só registra que IA falhou 
                novoChamado.PrioridadeSugeridaIA = "Falha IA";
                novoChamado.ProblemaSugeridoIA = "Falha IA";
                novoChamado.SolucaoSugeridaIA = "Falha IA";
            }

            // 2. Salvar o Chamado
            int idChamado = await _chamadoRepo.AdicionarAsync(novoChamado);
            if (idChamado == -1)
            {
                throw new Exception("Não foi possível salvar o chamado no banco de dados.");
            }

            // 3. Salvar o Arquivo (se existir)
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

            // 4. Enviar o Email
            await _emailService.EnviarEmailChamadoAsync(
                novoChamado.Titulo, novoChamado.Descricao, novoChamado.Categoria, idChamado,
                novoChamado.PrioridadeSugeridaIA, novoChamado.StatusChamado, novoChamado.PessoasAfetadas, novoChamado.ImpedeTrabalho,
                novoChamado.OcorreuAnteriormente, novoChamado.ProblemaSugeridoIA, novoChamado.SolucaoSugeridaIA, novoChamado.DataChamado,
                arquivoBytes, nomeAnexo
            );

            return idChamado;
        }
    }
}