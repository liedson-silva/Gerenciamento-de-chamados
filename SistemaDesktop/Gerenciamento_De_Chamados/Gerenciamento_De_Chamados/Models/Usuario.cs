// Models/Usuario.cs
using System;

namespace Gerenciamento_De_Chamados.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; } 
        public string RG { get; set; }
        public string FuncaoUsuario { get; set; }
        public string Sexo { get; set; } 
        public string Setor { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; } 
        public string Login { get; set; }
    }
}