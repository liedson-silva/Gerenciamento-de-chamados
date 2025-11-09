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
        // 1. Mocks (Fakes) para cada dependência
        private Mock<IChamadoRepository> _mockChamadoRepo = null!;
        private Mock<IArquivoRepository> _mockArquivoRepo = null!;
        private Mock<IEmailService> _mockEmailService = null!;
        private Mock<IAIService> _mockAiService = null!; 

        // A classe real que estamos testando
        private ChamadoService _chamadoService = null!;

        // [TestInitialize] roda automaticamente ANTES de cada teste
        [TestInitialize]
        public void Setup()
        {
            // 2. Cria novas simulações "limpas" para cada teste
            _mockChamadoRepo = new Mock<IChamadoRepository>();
            _mockArquivoRepo = new Mock<IArquivoRepository>();
            _mockEmailService = new Mock<IEmailService>();
            _mockAiService = new Mock<IAIService>(); 

            // 3. Instancia o Serviço, injetando os FAKES
            _chamadoService = new ChamadoService(
                _mockChamadoRepo.Object,  // .Object entrega o objeto fake
                _mockArquivoRepo.Object,
                _mockEmailService.Object,
                _mockAiService.Object
            );
        }

        [TestMethod]
        public async Task CriarNovoChamadoAsync_ComArquivo_DeveChamarTodosOsPassos()
        {
            // --- 1. Arrange (Organizar) ---

            // Dados de entrada fakes
            var chamado = new Chamado
            {
                Titulo = "Teste",
                Categoria = "Hardware",
                DataChamado = DateTime.Now
            };
            byte[] arquivo = new byte[] { 0x01, 0x02 }; // Um arquivo binário fake
            int fakeIdChamado = 123; // O ID que esperamos que o banco retorne

            // Configura os Mocks (diz aos fakes o que fazer)

            // "Quando o repo de chamado for chamado para AdicionarAsync..."
            _mockChamadoRepo.Setup(repo => repo.AdicionarAsync(It.IsAny<Chamado>()))
                            .ReturnsAsync(fakeIdChamado); // "...finja que retornou o ID 123"

            // "Quando o repo de chamado for chamado para BuscarSolucoes..."
            _mockChamadoRepo.Setup(repo => repo.BuscarSolucoesAnterioresAsync(It.IsAny<string>()))
                            .ReturnsAsync(new List<string>()); // "...retorne uma lista vazia"

            // "Quando a IA for chamada para AnalisarChamado..."
            _mockAiService.Setup(ai => ai.AnalisarChamado(
                                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                It.IsAny<List<string>>()))
                          .ReturnsAsync(("Problema Fake", "Prioridade Fake", "Solucao Fake")); // "...retorne uma tupla fake"

            // --- 2. Act (Agir) ---
            // Chama o método que queremos testar com os dados fakes
            int idResultado = await _chamadoService.CriarNovoChamadoAsync(chamado, arquivo, "teste.png", "image/png");


            // --- 3. Assert / Verify (Verificar) ---

            // O ID retornado foi o ID fake (123) que configuramos?
            Assert.AreEqual(fakeIdChamado, idResultado);

            // VERIFICA se os métodos "fakes" foram chamados o número correto de vezes.
            

            // Verifica se a IA foi chamada 1 vez
            _mockAiService.Verify(ai => ai.AnalisarChamado(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<List<string>>()), Times.Once());

            // Verifica se o repositório de chamado foi chamado para salvar 1 vez
            _mockChamadoRepo.Verify(repo => repo.AdicionarAsync(It.IsAny<Chamado>()), Times.Once());

            // Verifica se o repositório de arquivo foi chamado 1 vez (pois o arquivo não era nulo)
            _mockArquivoRepo.Verify(repo => repo.AdicionarAsync(It.IsAny<Arquivo>()), Times.Once());

            // Verifica se o serviço de email foi chamado 1 vez
            _mockEmailService.Verify(email => email.EnviarEmailChamadoAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(),
                It.IsAny<byte[]>(), It.IsAny<string>()), Times.Once());
        }
    }
}