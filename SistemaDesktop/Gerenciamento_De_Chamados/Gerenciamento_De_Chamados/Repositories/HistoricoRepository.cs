using Gerenciamento_De_Chamados.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Repositories
{
    /// <summary>
    /// Repositório para gerenciar a persistência dos registros de histórico na tabela 'Historico'.
    /// Esta classe é crucial para rastrear o que aconteceu com cada chamado (análise, solução, etc.).
    /// </summary>
    public class HistoricoRepository : IHistoricoRepository
    {
        // ====================================================================
        // I. CONFIGURAÇÃO DO BANCO DE DADOS
        // ====================================================================

        /// <summary>
        /// String de conexão obtida do App.config.
        /// </summary>
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // ====================================================================
        // II. MÉTODOS DE ESCRITA (INSERT)
        // ====================================================================

        /// <summary>
        /// Adiciona um novo registro de histórico no banco de dados.
        /// Este método é projetado para ser executado DENTRO de uma transação
        /// para garantir que as atualizações do chamado e do histórico sejam atômicas.
        /// </summary>
        /// <param name="historico">O objeto Historico a ser salvo.</param>
        /// <param name="conn">A conexão SQL ativa.</param>
        /// <param name="trans">A transação SQL ativa.</param>
        public async Task AdicionarAsync(Historico historico, SqlConnection conn, SqlTransaction trans)
        {
            string sql = @"INSERT INTO Historico (DataSolucao, Solucao, FK_IdChamado, Acao) 
                           VALUES (@Data, @Solucao, @IdChamado, @Acao)";

          
            using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
            {
                cmd.Parameters.AddWithValue("@Data", historico.DataSolucao);
                
                cmd.Parameters.AddWithValue("@Solucao", (object)historico.Solucao ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IdChamado", historico.FK_IdChamado);
                cmd.Parameters.AddWithValue("@Acao", historico.Acao);

                await cmd.ExecuteNonQueryAsync();
            }

        }

        // ====================================================================
        // III. MÉTODOS DE LEITURA (SELECT)
        // ====================================================================

        /// <summary>
        /// Busca a última solução que foi "Aplicada" para um determinado chamado.
        /// Usado na tela de Análise/Resolução para pré-preencher a solução atual.
        /// </summary>
        /// <param name="idChamado">ID do chamado.</param>
        /// <returns>A string da última solução aplicada ou null se não houver.</returns>
        public async Task<string> BuscarUltimaSolucaoAsync(int idChamado)
        {
            string sql = @"SELECT TOP 1 Solucao FROM Historico 
                           WHERE FK_IdChamado = @IdChamado AND Acao = 'Solução Aplicada' 
                           ORDER BY DataSolucao DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdChamado", idChamado);
                    await conn.OpenAsync();
                    // ExecuteScalar retorna o valor da primeira coluna na primeira linha do resultado
                    object result = await cmd.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }

                    return null; 
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Erro ao buscar a última solução do histórico: " + ex.Message);
                return null;
            }
        }/// <summary>
         /// Adiciona um registro de histórico FORA de uma transação.
         /// Este é ideal para "Notas Internas" ou eventos que não precisam de rollback
         /// em conjunto com outras operações, como o registro da triagem da IA.
         /// </summary>
        public async Task AdicionarSemTransacaoAsync(Historico historico)
        {
            string sql = @"INSERT INTO Historico (DataSolucao, Solucao, FK_IdChamado, Acao) 
                           VALUES (@Data, @Solucao, @IdChamado, @Acao)";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Data", historico.DataSolucao);
                    cmd.Parameters.AddWithValue("@Solucao", historico.Solucao);
                    cmd.Parameters.AddWithValue("@IdChamado", historico.FK_IdChamado);
                    cmd.Parameters.AddWithValue("@Acao", historico.Acao);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                // É importante que a falha em salvar a nota interna não interrompa o processo principal.
                // Apenas logamos o erro.
                Console.WriteLine($"Erro ao adicionar histórico sem transação para o chamado {historico.FK_IdChamado}: {ex.Message}");
            }
        }
    }
}