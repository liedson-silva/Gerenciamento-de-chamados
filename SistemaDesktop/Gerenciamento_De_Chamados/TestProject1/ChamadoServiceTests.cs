using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Gerenciamento_De_Chamados.Services;
using Gerenciamento_De_Chamados.Repositories;
using Gerenciamento_De_Chamados.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Gerenciamento_De_Chamados.Tests
{
    [TestClass]
    public class ChamadoServiceTests
    {
        // Mocking todas as 5 dependências
        private Mock<IChamadoRepository> _mockChamadoRepo = null!;
        private Mock<IArquivoRepository> _mockArquivoRepo = null!;
        private Mock<IEmailService> _mockEmailService = null!;
        private Mock<IAIService> _mockAiService = null!;
        private Mock<IHistoricoRepository> _mockHistoricoRepo = null!; // ADICIONADO: Mock para o Histórico

        private ChamadoService _chamadoService = null!;

        // Dados fakes reutilizáveis
        private Chamado _fakeChamado;
        private Usuario _fakeUsuario;
        private byte[] _fakeArquivo;
        private int _fakeIdChamado = 123;
        private string _fakeNomeAnexo = "teste.png";
        private string _fakeTipoAnexo = "image/png";


        [TestInitialize]
        public void Setup()
        {
            // Inicialização de todos os Mocks
            _mockChamadoRepo = new Mock<IChamadoRepository>();
            _mockArquivoRepo = new Mock<IArquivoRepository>();
            _mockEmailService = new Mock<IEmailService>();
            _mockAiService = new Mock<IAIService>();
            _mockHistoricoRepo = new Mock<IHistoricoRepository>(); // ADICIONADO: Inicialização do Mock

            // Instancia o Serviço com TODAS as 5 dependências
            _chamadoService = new ChamadoService(
                _mockChamadoRepo.Object,
                _mockArquivoRepo.Object,
                _mockEmailService.Object,
                _mockAiService.Object,
                _mockHistoricoRepo.Object // ADICIONADO: Injeção do Mock
            );

            // Configuração dos dados fake
            _fakeChamado = new Chamado
            {
                Titulo = "Teste",
                Categoria = "Hardware",
                PessoasAfetadas = "1",
                ImpedeTrabalho = "Sim",
                OcorreuAnteriormente = "Não"
            };
            _fakeUsuario = new Usuario { Nome = "Test User", Email = "test@user.com" };
            _fakeArquivo = new byte[] { 0x01, 0x02 };

            // Configuração comum para AdicionarAsync (para CriarChamadoBaseAsync)
            _mockChamadoRepo.Setup(repo => repo.AdicionarAsync(It.IsAny<Chamado>()))
                             .ReturnsAsync(_fakeIdChamado);
        }


        [TestMethod]
        public async Task CriarChamadoBaseAsync_ComArquivo_DeveSalvarChamadoEArquivo_E_RetornarId()
        {
            // --- 1. Arrange (Organizar) ---
            // A configuração padrão no Setup já cuida do AdicionarAsync

            // --- 2. Act (Agir) ---
            int idResultado = await _chamadoService.CriarChamadoBaseAsync(_fakeChamado, _fakeArquivo, _fakeNomeAnexo, _fakeTipoAnexo);

            // --- 3. Assert / Verify (Verificar) ---

            // 3.1. Verifica ID
            Assert.AreEqual(_fakeIdChamado, idResultado);

            // 3.2. Verifica Chamado e Arquivo
            _mockChamadoRepo.Verify(repo => repo.AdicionarAsync(_fakeChamado), Times.Once());
            _mockArquivoRepo.Verify(repo => repo.AdicionarAsync(It.IsAny<Arquivo>()), Times.Once());

            // 3.3. Verifica se NENHUMA outra dependência foi chamada (IA, Email, Histórico)
            _mockAiService.Verify(ai => ai.AnalisarChamado(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<string>>()), Times.Never());
            _mockEmailService.Verify(email => email.EnviarEmailNovoChamadoTIAsync(It.IsAny<Chamado>(), It.IsAny<Usuario>(), It.IsAny<int>(), It.IsAny<byte[]>(), It.IsAny<string>()), Times.Never());
            _mockEmailService.Verify(email => email.EnviarEmailConfirmacaoUsuarioAsync(It.IsAny<Chamado>(), It.IsAny<Usuario>(), It.IsAny<int>()), Times.Never());
            _mockHistoricoRepo.Verify(h => h.AdicionarSemTransacaoAsync(It.IsAny<Historico>()), Times.Never());
        }

        // --- TESTE 2: Foco na Etapa 2/3 (Lenta / Background) ---
        [TestMethod]
        public async Task ProcessarAnaliseEAtualizarAsync_DeveChamar_IA_AtualizarRepo_Histórico_E_EmailTI()
        {
            // --- 1. Arrange (Organizar) ---
            string problemaSugerido = "Problema no Sistema X";
            string prioridadeSugerida = "Alta";
            string solucaoSugerida = "Solucao IA"; // A solução que a IA vai retornar

            // Configura o Mock da IA para retornar dados de triagem
            _mockAiService.Setup(ai => ai.AnalisarChamado(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<List<string>>()))
            .ReturnsAsync((problemaSugerido, prioridadeSugerida, solucaoSugerida));

            // Configura o Mock do Repositório (para buscar soluções antigas)
            _mockChamadoRepo.Setup(repo => repo.BuscarSolucoesAnterioresAsync(It.IsAny<string>()))
                            .ReturnsAsync(new List<string>());

            // --- 2. Act (Agir) ---
            // Nota: O método ProcessarAnaliseEAtualizarAsync não exige mais o objeto Usuario, 
            // e os detalhes do anexo foram simplificados para bytes e nomeAnexo (sem tipo)
            await _chamadoService.ProcessarAnaliseEAtualizarAsync(_fakeIdChamado, _fakeChamado, _fakeArquivo, _fakeNomeAnexo);


            // --- 3. Assert / Verify (Verificar) ---

            // 3.1. Verifica se a IA foi chamada 1 vez (Incluindo a busca por soluções)
            _mockChamadoRepo.Verify(repo => repo.BuscarSolucoesAnterioresAsync(It.IsAny<string>()), Times.Once());
            _mockAiService.Verify(ai => ai.AnalisarChamado(
                 It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                 It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                 It.IsAny<List<string>>()), Times.Once());

            // 3.2. Verifica se o repositório foi chamado para ATUALIZAR o chamado (com os dados da IA)
            _mockChamadoRepo.Verify(repo => repo.AtualizarSugestoesIAAsync(
                _fakeIdChamado, prioridadeSugerida, problemaSugerido, solucaoSugerida), Times.Once());

            // 3.3. Verifica se o HISTÓRICO foi salvo (Nota Interna - Ação simplificada)
            _mockHistoricoRepo.Verify(h => h.AdicionarSemTransacaoAsync(
                It.Is<Historico>(h => h.FK_IdChamado == _fakeIdChamado &&
                                      h.Acao == "Nota Interna" && // Ação corrigida para "Nota Interna"
                                      h.Solucao == $"Nota interna: {solucaoSugerida}")), // Conteúdo corrigido para o formato simplificado
                Times.Once());

            // 3.4. Verifica se o email para a TI foi enviado 1 vez (assinatura corrigida)
            _mockEmailService.Verify(email => email.EnviarEmailNovoChamadoTIAsync(
                It.IsAny<Chamado>(), It.IsAny<Usuario>(), _fakeIdChamado, _fakeArquivo, _fakeNomeAnexo), Times.Once());
        }

        // --- NOVO TESTE: Verifica o fluxo de falha da IA ---
        [TestMethod]
        public async Task ProcessarAnaliseEAtualizarAsync_FalhaIA_DeveAtualizarRepoComFalha_E_EnviarEmailTI()
        {
            // --- 1. Arrange (Organizar) ---
            string mensagemErro = "Falha de comunicação com a API Gemini";

            // Configura o Mock da IA para LANÇAR uma exceção
            _mockAiService.Setup(ai => ai.AnalisarChamado(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<List<string>>()))
            .ThrowsAsync(new Exception(mensagemErro));

            // Configura o Mock do Repositório (para buscar soluções antigas)
            _mockChamadoRepo.Setup(repo => repo.BuscarSolucoesAnterioresAsync(It.IsAny<string>()))
                            .ReturnsAsync(new List<string>());

            // --- 2. Act (Agir) ---
            await _chamadoService.ProcessarAnaliseEAtualizarAsync(_fakeIdChamado, _fakeChamado, _fakeArquivo, _fakeNomeAnexo);


            // --- 3. Assert / Verify (Verificar) ---

            // 3.1. Verifica se a IA foi chamada 1 vez
            _mockAiService.Verify(ai => ai.AnalisarChamado(
                 It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                 It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                 It.IsAny<List<string>>()), Times.Once());

            // 3.2. Verifica se o repositório foi chamado para ATUALIZAR com os status de falha
            // O serviço de chamado atualiza SolucaoSugeridaIA com a mensagem de erro da exceção.
            _mockChamadoRepo.Verify(repo => repo.AtualizarSugestoesIAAsync(
                _fakeIdChamado, "Falha IA", "Falha IA", It.Is<string>(s => s.Contains(mensagemErro))), Times.Once());

            // 3.3. Verifica se o HISTÓRICO NÃO foi salvo (apenas a falha do chamado é atualizada)
            _mockHistoricoRepo.Verify(h => h.AdicionarSemTransacaoAsync(It.IsAny<Historico>()), Times.Never());

            // 3.4. Verifica se o email para a TI FOI enviado, mesmo com a falha da IA
            _mockEmailService.Verify(email => email.EnviarEmailNovoChamadoTIAsync(
                It.IsAny<Chamado>(), It.IsAny<Usuario>(), _fakeIdChamado, _fakeArquivo, _fakeNomeAnexo), Times.Once());
        }
    }
}