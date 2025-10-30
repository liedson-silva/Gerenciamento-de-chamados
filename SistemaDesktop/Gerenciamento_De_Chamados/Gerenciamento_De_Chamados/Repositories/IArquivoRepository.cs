
using Gerenciamento_De_Chamados.Models;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Repositories
{
    public interface IArquivoRepository
    {
        Task AdicionarAsync(Arquivo arquivo);
    }
}