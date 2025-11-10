using Gerenciamento_De_Chamados.Services;
using Gerenciamento_De_Chamados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados.Repositories
{
    public class ChamadoRepository : IChamadoRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public async Task<int> AdicionarAsync(Chamado chamado)
        {
            string sql = @"INSERT INTO Chamado
                         (Titulo, PrioridadeChamado, Descricao, DataChamado, StatusChamado, Categoria,
                         FK_IdUsuario, PessoasAfetadas, ImpedeTrabalho, OcorreuAnteriormente,
                         PrioridadeSugeridaIA, ProblemaSugeridoIA, SolucaoSugeridaIA)
                         OUTPUT INSERTED.IdChamado
                         VALUES (@Titulo, @PrioridadeChamado, @Descricao, @DataChamado,
                         @StatusChamado, @Categoria, @FK_IdUsuario, @PessoasAfetadas,
                         @ImpedeTrabalho, @Ocorreu, @PrioridadeSugeridaIA, @ProblemaSugeridoIA, @SolucaoSugeridaIA)";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Titulo", chamado.Titulo);
                    cmd.Parameters.AddWithValue("@PrioridadeChamado", chamado.PrioridadeChamado);
                    cmd.Parameters.AddWithValue("@Descricao", chamado.Descricao);
                    cmd.Parameters.AddWithValue("@DataChamado", chamado.DataChamado);
                    cmd.Parameters.AddWithValue("@StatusChamado", chamado.StatusChamado);
                    cmd.Parameters.AddWithValue("@Categoria", chamado.Categoria);
                    cmd.Parameters.AddWithValue("@FK_IdUsuario", chamado.FK_IdUsuario);
                    cmd.Parameters.AddWithValue("@PessoasAfetadas", (object)chamado.PessoasAfetadas ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ImpedeTrabalho", (object)chamado.ImpedeTrabalho ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ocorreu", (object)chamado.OcorreuAnteriormente ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PrioridadeSugeridaIA", (object)chamado.PrioridadeSugeridaIA ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ProblemaSugeridoIA", (object)chamado.ProblemaSugeridoIA ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SolucaoSugeridaIA", (object)chamado.SolucaoSugeridaIA ?? DBNull.Value);

                    await conn.OpenAsync();
                    int idChamado = (int)await cmd.ExecuteScalarAsync();
                    return idChamado;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar chamado no repositório: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; 
            }
        }

        public DataTable BuscarTodosFiltrados(string filtro)
        {
            var chamadosTable = new DataTable();
            string sql = @"
                SELECT 
                    u.IdUsuario, c.IdChamado, u.Nome AS Usuario, 
                    c.Titulo, c.PrioridadeChamado AS Prioridade, c.Descricao, 
                    c.DataChamado AS Data, c.StatusChamado AS Status, c.Categoria 
                FROM Chamado c
                JOIN Usuario u ON c.FK_IdUsuario = u.IdUsuario
                WHERE (@filtro = '' OR c.Titulo LIKE '%' + @filtro + '%'
                    OR c.PrioridadeChamado LIKE '%' + @filtro + '%'
                    OR c.Descricao LIKE '%' + @filtro + '%'
                    OR c.StatusChamado LIKE '%' + @filtro + '%'
                    OR c.Categoria LIKE '%' + @filtro + '%'
                    OR u.Nome LIKE '%' + @filtro + '%')
                ORDER BY c.DataChamado DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@filtro", filtro ?? string.Empty);
                    da.Fill(chamadosTable);
                    return chamadosTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar chamados no repositório: " + ex.Message);
                return null;
            }
        }

        public async Task<List<string>> BuscarSolucoesAnterioresAsync(string categoria)
        {
            List<string> solucoes = new List<string>();
            string sql = "SELECT Solucao FROM Historico WHERE FK_IdChamado IN (SELECT IdChamado FROM Chamado WHERE Categoria = @Categoria) AND Acao = 'Resolução Final'";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Categoria", categoria);
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            solucoes.Add(reader["Solucao"].ToString());
                        }
                    }
                }
                return solucoes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar soluções anteriores: " + ex.Message);
                return new List<string>(); 
            }
        }
        public async Task<Chamado> BuscarPorIdAsync(int id)
        {
            Chamado chamado = null;
            // Seleciona todas as colunas necessárias para a análise
            string sqlSelect = @"
        SELECT Titulo, Descricao, Categoria, StatusChamado,
               PrioridadeSugeridaIA, ProblemaSugeridoIA, SolucaoSugeridaIA,
               PrioridadeChamado, PessoasAfetadas, ImpedeTrabalho, OcorreuAnteriormente, FK_IdUsuario, DataChamado
        FROM Chamado 
        WHERE IdChamado = @IdChamado";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmdSelect = new SqlCommand(sqlSelect, conn))
            {
                cmdSelect.Parameters.AddWithValue("@IdChamado", id);
                await conn.OpenAsync();

                using (SqlDataReader reader = await cmdSelect.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        chamado = new Chamado
                        {
                            IdChamado = id,
                            Titulo = reader["Titulo"].ToString(),
                            Descricao = reader["Descricao"].ToString(),
                            Categoria = reader["Categoria"].ToString(),
                            StatusChamado = reader["StatusChamado"].ToString(), 
                            PrioridadeChamado = reader["PrioridadeChamado"]?.ToString(), 
                            PessoasAfetadas = reader["PessoasAfetadas"]?.ToString(),
                            ImpedeTrabalho = reader["ImpedeTrabalho"]?.ToString(),
                            OcorreuAnteriormente = reader["OcorreuAnteriormente"]?.ToString(),
                            PrioridadeSugeridaIA = reader["PrioridadeSugeridaIA"]?.ToString(),
                            ProblemaSugeridoIA = reader["ProblemaSugeridoIA"]?.ToString(),
                            SolucaoSugeridaIA = reader["SolucaoSugeridaIA"]?.ToString(),
                            FK_IdUsuario = (int)reader["FK_IdUsuario"],
                            DataChamado = (DateTime)reader["DataChamado"]
                        };
                    }
                }
            }
            return chamado;
        }


        public async Task AtualizarSugestoesIAAsync(int id, string prioridade, string problema, string solucao)
        {
            string sqlUpdate = @"
                UPDATE Chamado 
                SET ProblemaSugeridoIA = @Problema, 
                    SolucaoSugeridaIA = @Solucao, 
                    PrioridadeSugeridaIA = @Prioridade 
                WHERE IdChamado = @IdChamado";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn))
            {
                cmdUpdate.Parameters.AddWithValue("@Problema", (object)problema ?? DBNull.Value);
                cmdUpdate.Parameters.AddWithValue("@Solucao", (object)solucao ?? DBNull.Value);
                cmdUpdate.Parameters.AddWithValue("@Prioridade", (object)prioridade ?? DBNull.Value);
                cmdUpdate.Parameters.AddWithValue("@IdChamado", id);

                await conn.OpenAsync();
                await cmdUpdate.ExecuteNonQueryAsync();
            }
        }

        public async Task AtualizarAnaliseAsync(Chamado chamado, SqlConnection conn, SqlTransaction trans)
        {
            string sql = @"
        UPDATE Chamado 
        SET StatusChamado = @Status, 
            PrioridadeChamado = @Prioridade,
            ProblemaSugeridoIA = @Problema, 
            SolucaoSugeridaIA = @Solucao 
        WHERE IdChamado = @IdChamado";

            using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
            {
                cmd.Parameters.AddWithValue("@Status", chamado.StatusChamado);
                cmd.Parameters.AddWithValue("@Prioridade", chamado.PrioridadeChamado);
                cmd.Parameters.AddWithValue("@Problema", (object)chamado.ProblemaSugeridoIA ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Solucao", (object)chamado.SolucaoSugeridaIA ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IdChamado", chamado.IdChamado);

                await cmd.ExecuteNonQueryAsync();
            }
        }
        public async Task AtualizarStatusAsync(int idChamado, string novoStatus, string novaPrioridade, string novaCategoria, SqlConnection conn, SqlTransaction trans)
        {
            string sql = @"UPDATE Chamado SET 
                       StatusChamado = @Status, 
                       PrioridadeChamado = @Prioridade, 
                       Categoria = @Categoria 
                   WHERE IdChamado = @IdChamado";

            using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
            {
                cmd.Parameters.AddWithValue("@Status", novoStatus);
                cmd.Parameters.AddWithValue("@Prioridade", (object)novaPrioridade ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Categoria", (object)novaCategoria ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IdChamado", idChamado);

                await cmd.ExecuteNonQueryAsync();
            }
        }
        public async Task<List<ChartDataPoint>> ContarPorStatusAsync(int idUsuario)
        {
            var list = new List<ChartDataPoint>();
            // CORREÇÃO: Mudado de 'AS Count' para 'AS Value'
            string sql = @"
                SELECT StatusChamado AS Label, COUNT(IdChamado) AS Value
                FROM Chamado
                WHERE FK_IdUsuario = @IdUsuario
                GROUP BY StatusChamado";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new ChartDataPoint
                        {
                            Label = reader["Label"].ToString(),
                            // CORREÇÃO: Usando 'Value' e convertendo para double
                            Value = Convert.ToDouble(reader["Value"])
                        });
                    }
                }
            }
            return list;
        }
        public async Task<List<ChartDataPoint>> ContarPorCategoriaAsync(int idUsuario)
        {
            var list = new List<ChartDataPoint>();
            // CORREÇÃO: Mudado de 'AS Count' para 'AS Value'
            string sql = @"
                SELECT Categoria AS Label, COUNT(IdChamado) AS Value
                FROM Chamado
                WHERE FK_IdUsuario = @IdUsuario
                GROUP BY Categoria";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new ChartDataPoint
                        {
                            Label = reader["Label"].ToString(),
                            // CORREÇÃO: Usando 'Value' e convertendo para double
                            Value = Convert.ToDouble(reader["Value"])
                        });
                    }
                }
            }
            return list;
        }
        public async Task<DataTable> BuscarMeusChamadosFiltrados(int idUsuario, string status, string filtroPesquisa)
        {
            var dt = new DataTable();
            string sql = @"
        SELECT IdChamado, Titulo, PrioridadeChamado, Descricao, DataChamado, StatusChamado, Categoria
        FROM Chamado
        WHERE FK_IdUsuario = @IdUsuario
        AND (@Status = '' OR StatusChamado = @Status)
        AND (@FiltroPesquisa = '' 
             OR Titulo LIKE @LikeFiltro 
             OR Descricao LIKE @LikeFiltro 
             OR Categoria LIKE @LikeFiltro)
        ORDER BY DataChamado DESC";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@Status", status ?? ""); // Se 'status' for nulo, usa string vazia
                cmd.Parameters.AddWithValue("@FiltroPesquisa", filtroPesquisa ?? "");
                cmd.Parameters.AddWithValue("@LikeFiltro", $"%{filtroPesquisa ?? ""}%");

                await conn.OpenAsync();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
}
