using Gerenciamento_De_Chamados.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Repositories
{
    public class HistoricoRepository : IHistoricoRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public async Task AdicionarAsync(Historico historico, SqlConnection conn, SqlTransaction trans)
        {
            string sql = @"INSERT INTO Historico (DataSolucao, Solucao, FK_IdChamado, Acao) 
                           VALUES (@Data, @Solucao, @IdChamado, @Acao)";

            using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
            {
                cmd.Parameters.AddWithValue("@Data", historico.DataSolucao);
                cmd.Parameters.AddWithValue("@Solucao", historico.Solucao);
                cmd.Parameters.AddWithValue("@IdChamado", historico.FK_IdChamado);
                cmd.Parameters.AddWithValue("@Acao", historico.Acao);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<string> BuscarUltimaSolucaoAsync(int idChamado)
        {
            string sql = @"SELECT TOP 1 Solucao FROM Historico 
                           WHERE FK_IdChamado = @IdChamado AND Acao = 'Solução Aplicada' 
                           ORDER BY DataSolucao DESC";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IdChamado", idChamado);
                await conn.OpenAsync();
                object result = await cmd.ExecuteScalarAsync();

                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }

                return null; 
            }
        }
    }
}