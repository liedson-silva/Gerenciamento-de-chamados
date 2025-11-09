using Gerenciamento_De_Chamados.Models;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Services
{
    // Adicione "public" aqui
    public interface IUsuarioService
    {
        Task AdicionarUsuarioAsync(Usuario novoUsuario);
    }
}