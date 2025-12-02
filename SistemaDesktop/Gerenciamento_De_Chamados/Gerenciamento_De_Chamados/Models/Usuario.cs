using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Models
{
    /// <summary>
    /// Esta classe é o modelo de dados para a tabela 'Usuario'.
    /// Ela define todas as informações de cadastro e autenticação de cada pessoa no sistema.
    /// </summary>
    public class Usuario
    {
        // ====================================================================
        // I. CHAVES E AUTENTICAÇÃO
        // ====================================================================

        /// <summary>
        /// ID Único do usuário (chave primária). É o nosso identificador principal.
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Login que o usuário digita na tela de acesso.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Senha criptografada do usuário.
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Nível de acesso do usuário no sistema (Admin, Funcionário, Técnico).
        /// </summary>
        public string FuncaoUsuario { get; set; }


        // ====================================================================
        // II. DADOS CADASTRAIS E PESSOAIS
        // ====================================================================

        /// <summary>
        /// Nome completo do usuário.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Cadastro de Pessoa Física.
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Registro Geral.
        /// </summary>
        public string RG { get; set; }

        /// <summary>
        /// Sexo do usuário (Ex: M ou F).
        /// </summary>
        public string Sexo { get; set; }

        /// <summary>
        /// Setor ou área de trabalho do usuário.
        /// </summary>
        public string Setor { get; set; }

        /// <summary>
        /// Data de Nascimento do usuário.
        /// </summary>
        public DateTime DataDeNascimento { get; set; }

        /// <summary>
        /// E-mail do usuário, usado para comunicação e recuperação.
        /// </summary>
        public string Email { get; set; }
    }
}