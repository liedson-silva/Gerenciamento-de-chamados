using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados
{
    public static class SessaoUsuario
    {
        public static int IdUsuario { get; set; }
        public static string Login { get; set; }
        public static string Nome { get; set; }
        public static string Email { get; set; }
        public static string FuncaoUsuario { get; set; }

        public static bool UsuarioIdentificado()
        {
            return !string.IsNullOrEmpty(Nome) && IdUsuario > 0 && !string.IsNullOrEmpty(FuncaoUsuario);
        }
    }
}