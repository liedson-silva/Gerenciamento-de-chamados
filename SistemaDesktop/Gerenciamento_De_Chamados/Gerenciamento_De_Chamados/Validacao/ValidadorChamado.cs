using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Validacao
{
    public static class ValidadorChamado
    {
        private const int MIN_TAMANHO_TITULO = 5;
        private const int MIN_TAMANHO_DESCRICAO = 10;

        /// Valida se o título do chamado é aceitável.
        /// <returns>True se for válido, False se for inválido.</returns>
        public static bool IsTituloValido(string titulo)
        {
            // string.IsNullOrWhiteSpace checa se é nulo, vazio ("") ou só espaço em branco ("   ")
            if (string.IsNullOrWhiteSpace(titulo))
            {
                return false;
            }

            return titulo.Length > MIN_TAMANHO_TITULO;
        }


        /// Valida se a descrição tem um conteúdo mínimo.

        public static bool IsDescricaoValida(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                return false;
            }

            return descricao.Length > MIN_TAMANHO_DESCRICAO;
        }

        public static bool IsPessoasAfetadasValido(string pessoasAfetadas)
        {
            // A regra é simplesmente que não pode ser nulo ou vazio.
            // O usuário DEVE selecionar uma das opções.
            return !string.IsNullOrWhiteSpace(pessoasAfetadas);
        }

    }
}
