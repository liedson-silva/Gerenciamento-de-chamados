using Gerenciamento_De_Chamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados
{
    public static class SessaoUsuario
    {
        // Tempo da sessão em minutos
        private const int TEMPO_SESSAO_MINUTOS = 30;

        
        public static int IdUsuario { get; private set; }
        public static string Login { get; private set; }
        public static string Nome { get; private set; }
        public static string Email { get; private set; }
        public static string FuncaoUsuario { get; private set; }

        // O "Token" de Validade
        private static DateTime _horaExpiracao;
        /// Inicia uma nova sessão de usuário ("Gera o Token").
      
        public static void IniciarSessao(Usuario usuario)
        {
            IdUsuario = usuario.IdUsuario;
            Login = usuario.Login;
            Nome = usuario.Nome;
            Email = usuario.Email;
            FuncaoUsuario = usuario.FuncaoUsuario;

            // Define a hora que a sessão vai expirar
            _horaExpiracao = DateTime.Now.AddMinutes(TEMPO_SESSAO_MINUTOS);
        }


        /// Verifica se a sessão atual ainda é válida ("Valida o Token").
      
        public static bool SessaoEstaValida()
        {
            // Se o ID for 0, não há sessão.
            // Se a hora atual for maior que a expiração, a sessão expirou.
            if (IdUsuario == 0 || DateTime.Now > _horaExpiracao)
            {
                EncerrarSessao(); // Garante que tudo seja limpo
                return false;
            }

            return true;
        }


        /// Encerra a sessão do usuário ("Revoga o Token").

        public static void EncerrarSessao()
        {
            IdUsuario = 0;
            Login = null;
            Nome = null;
            Email = null;
            FuncaoUsuario = null;
            _horaExpiracao = DateTime.MinValue; // Expira a sessão
        }
    }
}