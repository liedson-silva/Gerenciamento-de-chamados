// Repositories/IUsuarioRepository.cs
using Gerenciamento_De_Chamados.Models;
using System.Collections.Generic; // Para o GetAll
using System.Data; // Para o DataTable
using System.Threading.Tasks; // Para métodos assíncronos

namespace Gerenciamento_De_Chamados.Repositories
{
    public interface IUsuarioRepository
    {
        Task AdicionarAsync(Usuario usuario); 
        Task AtualizarAsync(Usuario usuario); 
        Task<Usuario> BuscarPorIdAsync(int id); 
        DataTable BuscarTodosFiltrados(string filtro); 
        Task<Usuario> AutenticarAsync(string login, string senha); 
        Task<bool> VerificarLoginExistenteAsync(string login, int? idUsuarioExcluir = null); 
        Task ExcluirAsync(int id);
        Task AlterarSenhaAsync(int idUsuario, string novaSenha);
    }
}