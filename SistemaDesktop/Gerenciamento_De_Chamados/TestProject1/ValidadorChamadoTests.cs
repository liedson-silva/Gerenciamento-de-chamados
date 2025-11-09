using Gerenciamento_De_Chamados.Validacao;

namespace Gerenciamento_De_Chamados.Tests
{
    [TestClass]
    public sealed class ValidadorChamadoTests
    {
        [TestMethod]
        public void IsTituloValido_ComTituloCorreto_DeveRetornarVerdadeiro()
        {
            // Arrange (Organizar)
            string titulo = "Computador não liga"; // Tem mais de 5 chars

            // Act (Agir)
            bool resultado = ValidadorChamado.IsTituloValido(titulo);

            // Assert (Afirmar)
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void IsTituloValido_ComTituloCurto_DeveRetornarFalso()
        {
            // Arrange
            string titulo = "Ajuda"; // Tem 5 ou menos chars

            // Act
            bool resultado = ValidadorChamado.IsTituloValido(titulo);

            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void IsTituloValido_ComTituloNulo_DeveRetornarFalso()
        {
            // Arrange
            string titulo = null;

            // Act
            bool resultado = ValidadorChamado.IsTituloValido(titulo);

            // Assert
            Assert.IsFalse(resultado);
        }
        [TestMethod]
        public void IsDescricaoValida_ComDescricaoCorreta_DeveRetornarVerdadeiro()
        {
            // Arrange
            string desc = "Meu computador está fazendo um barulho estranho."; // > 10 chars

            // Act
            bool resultado = ValidadorChamado.IsDescricaoValida(desc);

            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void IsDescricaoValida_ComDescricaoCurta_DeveRetornarFalso()
        {
            // Arrange
            string desc = "PC ruim."; // < 10 chars

            // Act
            bool resultado = ValidadorChamado.IsDescricaoValida(desc);

            // Assert
            Assert.IsFalse(resultado);
        }

        //--- Testes para IsPessoasAfetadasValido ---

        [TestMethod]
        public void IsPessoasAfetadasValido_ComOpcaoSelecionada_DeveRetornarVerdadeiro()
        {
            // Arrange
            string selecao = "meu setor"; // Simulando uma seleção

            // Act
            bool resultado = ValidadorChamado.IsPessoasAfetadasValido(selecao);

            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void IsPessoasAfetadasValido_ComNulo_DeveRetornarFalso()
        {
            // Arrange
            string selecao = null; // Simulando nenhuma seleção

            // Act
            bool resultado = ValidadorChamado.IsPessoasAfetadasValido(selecao);

            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void IsPessoasAfetadasValido_ComVazio_DeveRetornarFalso()
        {
            // Arrange
            string selecao = ""; // Simulando nenhuma seleção

            // Act
            bool resultado = ValidadorChamado.IsPessoasAfetadasValido(selecao);

            // Assert
            Assert.IsFalse(resultado);
        }
    }
}
