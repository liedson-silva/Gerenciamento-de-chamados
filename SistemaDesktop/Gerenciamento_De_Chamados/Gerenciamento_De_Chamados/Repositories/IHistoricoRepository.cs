using Gerenciamento_De_Chamados.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Repositories
{
    /// <summary>
    /// Interface para o Repositório de Histórico.
    /// Define as operações para registrar e consultar ações e soluções aplicadas aos chamados.
    /// </summary>
    public interface IHistoricoRepository
    {
        /// <summary>
        /// Adiciona um novo registro de histórico ao banco de dados.
        /// Este método é obrigatório para transações e exige a conexão e transação ativas.
        /// </summary>
        /// <param name="historico">O objeto Historico a ser salvo (contém a ação e a solução).</param>
        /// <param name="conn">A conexão SQL ativa (SqlConnection).</param>
        /// <param name="trans">A transação SQL ativa (SqlTransaction).</param>
        Task AdicionarAsync(Historico historico, SqlConnection conn, SqlTransaction trans);

        /// <summary>
        /// Busca a última descrição de 'Solução Aplicada' registrada para um chamado específico.
        /// </summary>
        /// <param name="idChamado">O ID do chamado.</param>
        /// <returns>A string contendo a última solução registrada ou null se não houver.</returns>
        Task<string> BuscarUltimaSolucaoAsync(int idChamado);

        // Usado para eventos como a nota interna da IA, que não fazem parte da transação principal de criação do chamado.
        Task AdicionarSemTransacaoAsync(Historico historico);
    }
}