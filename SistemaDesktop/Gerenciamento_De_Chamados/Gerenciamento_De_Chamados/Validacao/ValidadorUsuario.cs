using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Validacao
{
    public static class ValidadorUsuario
    {
        private const int TAMANHO_MINIMO_SENHA = 8;
        private const int TAMANHO_CPF = 11;

        
        /// Valida se um email possui um formato básico aceitável.
        public static bool IsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Esta é uma expressão regular simples para validar emails.
            // Garante que tenha algo, um '@', algo, um '.', e algo no final.
            try
            {
                // Regex.IsMatch lança exceção se o email for muito complexo/longo
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        /// Valida se a senha atende os requisitos mínimos de força.
        /// (Ex: Mínimo 8 caracteres, 1 número, 1 letra maiúscula)

        public static bool IsSenhaForte(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                return false;

            // Regra 1: Tamanho mínimo
            if (senha.Length < TAMANHO_MINIMO_SENHA)
                return false;

            // Regra 2: Deve conter pelo menos um número
            if (!senha.Any(char.IsDigit))
                return false;

            // Regra 3: Deve conter pelo menos uma letra maiúscula
            if (!senha.Any(char.IsUpper))
                return false;

            return true;
        }

        /// Valida o formato básico de um CPF (apenas se tem 11 dígitos numéricos).
        public static bool IsCPFValido(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            // Remove caracteres não numéricos (como . e -)
            string cpfLimpo = new string(cpf.Where(char.IsDigit).ToArray());

            // Regra 1: Deve ter 11 dígitos
            if (cpfLimpo.Length != TAMANHO_CPF)
                return false;

            // Regra 2: Não pode ser uma sequência (ex: 111.111.111-11)
            if (cpfLimpo.Distinct().Count() == 1)
                return false;

            return true;
        }

        /// Valida se um campo obrigatório (como Nome) foi preenchido.
        public static bool IsNomeValido(string nome)
        {
            return !string.IsNullOrWhiteSpace(nome);
        }
    }
}
