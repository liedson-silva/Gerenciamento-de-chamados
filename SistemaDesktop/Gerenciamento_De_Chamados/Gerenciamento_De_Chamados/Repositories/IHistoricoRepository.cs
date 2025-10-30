using Gerenciamento_De_Chamados.Models;
using System.Data.SqlClient; 
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Repositories
{
    public interface IHistoricoRepository
    {
        // Adiciona um registro de histórico DENTRO de uma transação existente
        Task AdicionarAsync(Historico historico, SqlConnection conn, SqlTransaction trans);

        // Busca a última solução aplicada (para o CarregarDadosChamado)
        Task<string> BuscarUltimaSolucaoAsync(int idChamado);
    }
}