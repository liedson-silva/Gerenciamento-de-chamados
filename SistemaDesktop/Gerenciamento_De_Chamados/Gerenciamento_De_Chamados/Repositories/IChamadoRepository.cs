
using Gerenciamento_De_Chamados.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Repositories
{
    public interface IChamadoRepository
    {
        Task<int> AdicionarAsync(Chamado chamado);
        DataTable BuscarTodosFiltrados(string filtro);
        Task<List<string>> BuscarSolucoesAnterioresAsync(string categoria);

        Task<Chamado> BuscarPorIdAsync(int id);

        Task AtualizarSugestoesIAAsync(int id, string prioridade, string problema, string solucao);
        
        Task AtualizarAnaliseAsync(Chamado chamado, SqlConnection conn, SqlTransaction trans);

        Task AtualizarStatusAsync(int idChamado, string novoStatus, string novaPrioridade, string novaCategoria, SqlConnection conn, SqlTransaction trans);
        Task<List<ChartDataPoint>> ContarPorStatusAsync(int idUsuario);
        Task<List<ChartDataPoint>> ContarPorCategoriaAsync(int idUsuario);
        Task<DataTable> BuscarMeusChamadosFiltrados(int idUsuario, string status, string filtroPesquisa);
    }
}