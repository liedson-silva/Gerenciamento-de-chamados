using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Gerenciamento_De_Chamados.Services;
using Gerenciamento_De_Chamados.Repositories;
using Gerenciamento_De_Chamados.Models;
using System.Threading.Tasks;
using System;

namespace Gerenciamento_De_Chamados.Tests
{
    [TestClass]
    public class UsuarioServiceTests
    {
        // Mock do Repositório
        private Mock<IUsuarioRepository> _mockUsuarioRepo = null!;
        // Classe real a ser testada
        private IUsuarioService _usuarioService = null!;

        [TestInitialize]
        public void Setup()
        {
            // Cria um novo Mock para o repositório
            _mockUsuarioRepo = new Mock<IUsuarioRepository>();

            // Instancia o serviço, injetando o Mock
            _usuarioService = new UsuarioService(_mockUsuarioRepo.Object);
        }

        [TestMethod]
        public async Task AdicionarUsuarioAsync_ComDadosValidos_DeveChamarRepositorio()
        {
            // --- 1. Arrange (Organizar) ---

            // Cria um usuário com dados que PASSAM na validação
            var usuarioValido = new Usuario
            {
                Nome = "Usuario Teste",
                Email = "teste@valido.com",
                CPF = "123.456.789-00",
                Login = "teste123",
                Senha = "SenhaForte123"
            };

            // Configura o Mock: "Quando AdicionarAsync for chamado, apenas complete a Task"
            _mockUsuarioRepo.Setup(repo => repo.AdicionarAsync(It.IsAny<Usuario>()))
                            .Returns(Task.CompletedTask);

            // --- 2. Act (Agir) ---

            // Chama o método do serviço
            await _usuarioService.AdicionarUsuarioAsync(usuarioValido);

            // --- 3. Assert / Verify (Verificar) ---

            // Verifica se o método AdicionarAsync do repositório FAKE
            // foi chamado exatamente UMA vez.
            _mockUsuarioRepo.Verify(repo => repo.AdicionarAsync(It.IsAny<Usuario>()), Times.Once());
        }

        [TestMethod]
        public async Task AdicionarUsuarioAsync_ComEmailInvalido_DeveLancarExcecao()
        {
            // --- 1. Arrange (Organizar) ---

            // Cria um usuário com dados que FALHAM na validação
            var usuarioInvalido = new Usuario
            {
                Nome = "Usuario Teste",
                Email = "emailinvalido.com", // <-- Dado ruim
                CPF = "123.456.789-00",
                Login = "teste123",
                Senha = "SenhaForte123"
            };

            // --- 2. Act (Agir) & 3. Assert (Afirmar) ---

            try
            {
                // 2. Tenta executar o método que deve falhar
                await _usuarioService.AdicionarUsuarioAsync(usuarioInvalido);

                // 3. Se o código chegou até aqui, a exceção NÃO foi lançada.
                // Forçamos o teste a falhar.
                Assert.Fail("A exceção esperada (Exception) não foi lançada.");
            }
            catch (Exception ex)
            {
                // 3. O código pulou para o catch (sucesso!)
                // Agora, verificamos se é a exceção CORRETA.
                Assert.AreEqual("O formato do 'Email' é inválido.", ex.Message,
                    "A mensagem de exceção não era a esperada.");
            }
        }
    }
}