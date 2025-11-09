using Gerenciamento_De_Chamados.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamado.Tests
{
    [TestClass]
    public class ValidadorUsuarioTests
    {

        [TestMethod]
        public void IsEmailValido_ComEmailCorreto_DeveRetornarVerdadeiro()
        {
            // Arrange
            string email = "teste.paravalidar@gmail.com";
            // Act
            bool resultado = ValidadorUsuario.IsEmailValido(email);
            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void IsEmailValido_SemArroba_DeveRetornarFalso()
        {
            // Arrange
            string email = "teste.invalidogmail.com";
            // Act
            bool resultado = ValidadorUsuario.IsEmailValido(email);
            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void IsEmailValido_SemDominio_DeveRetornarFalso()
        {
            // Arrange
            string email = "teste@gmail";
            // Act
            bool resultado = ValidadorUsuario.IsEmailValido(email);
            // Assert
            Assert.IsFalse(resultado);
        }


        [TestMethod]
        public void IsSenhaForte_SenhaCorreta_DeveRetornarVerdadeiro()
        {
            // Arrange
            string senha = "MinhaSenha123"; // >8 chars, 1 maiúscula, 1 número
            // Act
            bool resultado = ValidadorUsuario.IsSenhaForte(senha);
            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void IsSenhaForte_SenhaCurta_DeveRetornarFalso()
        {
            // Arrange
            string senha = "Senha1"; // < 8 chars
            // Act
            bool resultado = ValidadorUsuario.IsSenhaForte(senha);
            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void IsSenhaForte_SemNumero_DeveRetornarFalso()
        {
            // Arrange
            string senha = "MinhaSenhaForte"; // Sem número
            // Act
            bool resultado = ValidadorUsuario.IsSenhaForte(senha);
            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void IsSenhaForte_SemMaiuscula_DeveRetornarFalso()
        {
            // Arrange
            string senha = "minhasenha123"; // Sem maiúscula
            // Act
            bool resultado = ValidadorUsuario.IsSenhaForte(senha);
            // Assert
            Assert.IsFalse(resultado);
        }



        [TestMethod]
        public void IsCPFValido_ComMascara_DeveRetornarVerdadeiro()
        {
            // Arrange
            string cpf = "123.456.789-00"; // 11 dígitos
            // Act
            bool resultado = ValidadorUsuario.IsCPFValido(cpf);
            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void IsCPFValido_SemMascara_DeveRetornarVerdadeiro()
        {
            // Arrange
            string cpf = "12345678900"; // 11 dígitos
            // Act
            bool resultado = ValidadorUsuario.IsCPFValido(cpf);
            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void IsCPFValido_Curto_DeveRetornarFalso()
        {
            // Arrange
            string cpf = "123.456.789-0"; // 10 dígitos
            // Act
            bool resultado = ValidadorUsuario.IsCPFValido(cpf);
            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void IsCPFValido_Sequencia_DeveRetornarFalso()
        {
            // Arrange
            string cpf = "111.111.111-11"; // CPF inválido
            // Act
            bool resultado = ValidadorUsuario.IsCPFValido(cpf);
            // Assert
            Assert.IsFalse(resultado);
        }
    }
}
