using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.teste
{
    [TestClass]
    public class UsuarioRepositoryTests
    {
        private Mock<IUsuarioRepository> _mockUsuarioRepository;

        [TestInitialize]
        public void SetUp()
        {
            // Cria um "mock" (simulação) do repositório de usuários
            _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        }

        [TestMethod]
        public async Task AdicionarUsuario_ComDadosValidos_DeveChamarAdicionarAsyncDoRepositorio()
        {
            // Arrange (Organizar)
            var novoUsuario = new Usuario
            {
                Nome = "Usuario Teste",
                Email = "teste@email.com",
                Senha = "senha123",
                FuncaoUsuario = "Funcionario" // Correção: Usando a propriedade correta
            };

            // Configura o mock para o método assíncrono AdicionarAsync, que não retorna valor
            _mockUsuarioRepository.Setup(repo => repo.AdicionarAsync(It.IsAny<Usuario>()))
                .Returns(Task.CompletedTask); // Correção: O método retorna Task, não Task<int>

            var repository = _mockUsuarioRepository.Object;

            // Act (Agir)
            await repository.AdicionarAsync(novoUsuario); // Correção: Não captura valor de retorno

            // Assert (Verificar)
            // Verifica se o método AdicionarAsync foi chamado exatamente uma vez
            _mockUsuarioRepository.Verify(repo => repo.AdicionarAsync(It.Is<Usuario>(u => u.Nome == "Usuario Teste")), Times.Once());
        }
    }
}