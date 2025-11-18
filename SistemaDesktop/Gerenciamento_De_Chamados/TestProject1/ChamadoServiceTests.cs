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

        private Mock<IChamadoRepository> _mockChamadoRepo = null!;
        private Mock<IArquivoRepository> _mockArquivoRepo = null!;
        private Mock<IEmailService> _mockEmailService = null!;
        private Mock<IAIService> _mockAiService = null!;


        private ChamadoService _chamadoService = null!;

        // Dados fakes reutilizáveis
        private Chamado _fakeChamado;
        private Usuario _fakeUsuario;
        private byte[] _fakeArquivo;
        private int _fakeIdChamado = 123;



        [TestInitialize]
        public void Setup()
        {

            _mockChamadoRepo = new Mock<IChamadoRepository>();
            _mockArquivoRepo = new Mock<IArquivoRepository>();
            _mockEmailService = new Mock<IEmailService>();
            _mockAiService = new Mock<IAIService>();


            _chamadoService = new ChamadoService(
                _mockChamadoRepo.Object,
                _mockArquivoRepo.Object,
                _mockEmailService.Object,
                _mockAiService.Object
            );


            _fakeChamado = new Chamado { Titulo = "Teste", Categoria = "Hardware" };
            _fakeUsuario = new Usuario { Nome = "Test User", Email = "test@user.com" };
            _fakeArquivo = new byte[] { 0x01, 0x02 };
        }


        [TestMethod]
        public async Task CriarChamadoBaseAsync_ComArquivo_DeveSalvarChamadoEArquivo_E_RetornarId()
        {
            // --- 1. Arrange (Organizar) ---

            // "Quando o repo de chamado for chamado para AdicionarAsync..."
            _mockChamadoRepo.Setup(repo => repo.AdicionarAsync(_fakeChamado))
                            .ReturnsAsync(_fakeIdChamado); // "...finja que retornou o ID 123"

            // --- 2. Act (Agir) ---
            int idResultado = await _chamadoService.CriarChamadoBaseAsync(_fakeChamado, _fakeArquivo, "teste.png", "image/png");

            // --- 3. Assert / Verify (Verificar) ---

            // O ID retornado foi o ID fake (123) que configuramos?
            Assert.AreEqual(_fakeIdChamado, idResultado);

            // Verifica se o repositório de chamado foi chamado para salvar 1 vez
            _mockChamadoRepo.Verify(repo => repo.AdicionarAsync(_fakeChamado), Times.Once());

            // Verifica se o repositório de arquivo foi chamado 1 vez (pois o arquivo não era nulo)
            _mockArquivoRepo.Verify(repo => repo.AdicionarAsync(It.IsAny<Arquivo>()), Times.Once());

            // VERIFICA SE NÃO FEZ NADA A MAIS:
            // A IA NÃO pode ser chamada nesta etapa
            _mockAiService.Verify(ai => ai.AnalisarChamado(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<string>>()), Times.Never());

            // O Email (para TI) NÃO pode ser chamado nesta etapa
            _mockEmailService.Verify(email => email.EnviarEmailNovoChamadoTIAsync(It.IsAny<Chamado>(), It.IsAny<Usuario>(), It.IsAny<int>(), It.IsAny<byte[]>(), It.IsAny<string>()), Times.Never());
        }

        // --- TESTE 2: Foco na Etapa 2/3 (Lenta / Background) ---
        [TestMethod]
        public async Task ProcessarAnaliseEAtualizarAsync_DeveChamar_IA_AtualizarRepo_E_EmailTI()
        {
            // --- 1. Arrange (Organizar) ---

            // Configura o Mock da IA
            _mockAiService.Setup(ai => ai.AnalisarChamado(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<List<string>>()))
            .ReturnsAsync(("Problema IA", "Prioridade IA", "Solucao IA"));

            // Configura o Mock do Repositório (para buscar soluções antigas)
            _mockChamadoRepo.Setup(repo => repo.BuscarSolucoesAnterioresAsync(It.IsAny<string>()))
                            .ReturnsAsync(new List<string>());

            // --- 2. Act (Agir) ---
            await _chamadoService.ProcessarAnaliseEAtualizarAsync(_fakeIdChamado, _fakeChamado, _fakeUsuario, _fakeArquivo, "teste.png", "image/png");


            // --- 3. Assert / Verify (Verificar) ---

            // Verifica se a IA foi chamada 1 vez
            _mockAiService.Verify(ai => ai.AnalisarChamado(
                 It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                 It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                 It.IsAny<List<string>>()), Times.Once());

            // Verifica se o repositório foi chamado para ATUALIZAR o chamado (com os dados da IA)
            _mockChamadoRepo.Verify(repo => repo.AtualizarSugestoesIAAsync(
                _fakeIdChamado, "Prioridade IA", "Problema IA", "Solucao IA"), Times.Once());

            // Verifica se o email para a TI foi enviado 1 vez
            _mockEmailService.Verify(email => email.EnviarEmailNovoChamadoTIAsync(
                _fakeChamado, _fakeUsuario, _fakeIdChamado, _fakeArquivo, "teste.png"), Times.Once());
        }
    }
}