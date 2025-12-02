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
    /// <summary>
    /// Esta classe é o Repositório principal que interage com a tabela 'Chamado' no banco de dados.
    /// Ele contém toda a lógica de persistência e consulta dos tickets de suporte.
    /// </summary>
    public class ChamadoRepository : IChamadoRepository
    {
        // ====================================================================
        // I. CONFIGURAÇÃO DO BANCO DE DADOS
        // ====================================================================

        /// <summary>
        /// Guarda a string de conexão para que possamos falar com o SQL Server.
        /// Ela é lida do arquivo App.config.
        /// </summary>
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // ====================================================================
        // II. MÉTODOS DE CRIAÇÃO (INSERT)
        // ====================================================================

        /// <summary>
        /// Salva um novo chamado no banco de dados, incluindo as sugestões geradas pela IA.
        /// Ele retorna o ID (protocolo) do chamado recém-criado, o que é crucial para anexar arquivos.
        /// </summary>
        /// <param name="chamado">O objeto Chamado a ser salvo.</param>
        /// <returns>O IdChamado (protocolo) gerado pelo banco.</returns>
        public async Task<int> AdicionarAsync(Chamado chamado)
        {
            // Comando SQL para inserir e retornar o ID (OUTPUT INSERTED.IdChamado)
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
                    cmd.Parameters.AddWithValue("@PrioridadeChamado", chamado.PrioridadeChamado ?? "Pendente");
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
                return -1; // Retorna -1 para indicar falha na inserção
            }
        }

        // ====================================================================
        // III. MÉTODOS DE LEITURA (SELECT) - BUSCA E FILTRO
        // ====================================================================

        /// <summary>
        /// Busca as soluções aplicadas anteriormente para chamados da mesma categoria.
        /// Este é o método que alimenta o modelo de IA com o 'histórico' do problema.
        /// </summary>
        /// <param name="categoria">A categoria do chamado atual.</param>
        /// <returns>Uma lista de strings com as soluções aplicadas no passado.</returns>
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
                return new List<string>(); // Retorna lista vazia em caso de erro
            }
        }

        /// <summary>
        /// Busca um chamado específico pelo seu ID (protocolo).
        /// </summary>
        /// <param name="id">O ID do chamado a ser buscado.</param>
        /// <returns>O objeto Chamado preenchido, ou nulo se não encontrado.</returns>
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

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar chamado por ID: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Busca todos os chamados no banco de dados, permitindo filtro em diversos campos.
        /// Usado na tela de Administrador/Técnico para ter uma visão geral dos tickets.
        /// </summary>
        /// <param name="filtro">Termo de pesquisa.</param>
        /// <returns>Um DataTable com os resultados.</returns>
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

        /// <summary>
        /// Busca e filtra os chamados ABREM por um usuário específico.
        /// Usado na tela do Funcionário para ver 'Meus Chamados'.
        /// </summary>
        /// <param name="idUsuario">ID do usuário logado.</param>
        /// <param name="status">Status a ser filtrado (Ex: 'Pendente') ou vazio para todos.</param>
        /// <param name="filtroPesquisa">Termo de pesquisa livre.</param>
        /// <returns>Um DataTable com os resultados.</returns>
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

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar meus chamados: " + ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// Conta o número de chamados por Status. Usado para o gráfico da Home do Funcionário.
        /// </summary>
        /// <param name="idUsuario">ID do usuário logado.</param>
        /// <returns>Lista de ChartDataPoint (Rótulo/Valor) para o gráfico.</returns>
        public async Task<List<ChartDataPoint>> ContarPorStatusAsync(int idUsuario)
        {
            var list = new List<ChartDataPoint>();
            // Seleciona o status (Label) e a contagem (Value) para o usuário específico
            string sql = @"
                SELECT StatusChamado AS Label, COUNT(IdChamado) AS Value
                FROM Chamado
                WHERE FK_IdUsuario = @IdUsuario
                GROUP BY StatusChamado";

            try
            {
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
                                // Convert.ToDouble(reader["Value"]) é mais seguro
                                Value = Convert.ToDouble(reader["Value"])
                            });
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao contar chamados por status (usuário): " + ex.Message);
                return new List<ChartDataPoint>();
            }
        }

        /// <summary>
        /// Conta o número de chamados por Categoria. Usado para o gráfico da Home do Funcionário.
        /// </summary>
        /// <param name="idUsuario">ID do usuário logado.</param>
        /// <returns>Lista de ChartDataPoint (Rótulo/Valor) para o gráfico.</returns>
        public async Task<List<ChartDataPoint>> ContarPorCategoriaAsync(int idUsuario)
        {
            var list = new List<ChartDataPoint>();
            // Seleciona a categoria (Label) e a contagem (Value) para o usuário específico
            string sql = @"
                SELECT Categoria AS Label, COUNT(IdChamado) AS Value
                FROM Chamado
                WHERE FK_IdUsuario = @IdUsuario
                GROUP BY Categoria";

            try
            {
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
                                Value = Convert.ToDouble(reader["Value"])
                            });
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao contar chamados por categoria (usuário): " + ex.Message);
                return new List<ChartDataPoint>();
            }
        }

        /// <summary>
        /// Conta o número TOTAL de chamados por Status. Usado para o gráfico de Visão Geral (Admin/Técnico).
        /// </summary>
        /// <returns>Lista de ChartDataPoint (Rótulo/Valor) para o gráfico.</returns>
        public async Task<List<ChartDataPoint>> ContarPorStatusGeralAsync()
        {
            var list = new List<ChartDataPoint>();
            string sql = @"SELECT StatusChamado, COUNT(IdChamado) 
                           FROM Chamado 
                           GROUP BY StatusChamado";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new ChartDataPoint
                            {
                                // reader.GetString(0) pega a primeira coluna (StatusChamado)
                                Label = reader.GetString(0),
                                // reader.GetInt32(1) pega a segunda coluna (COUNT)
                                Value = reader.GetInt32(1)
                            });
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao contar chamados por status (Geral): " + ex.Message);
                return new List<ChartDataPoint>();
            }
        }

        // método para o gráfico de Categoria GERAL (Admin/Tecnico)
        /// <summary>
        /// Conta o número TOTAL de chamados por Categoria. Usado para o gráfico de Prioridades (Admin/Técnico).
        /// </summary>
        /// <returns>Lista de ChartDataPoint (Rótulo/Valor) para o gráfico.</returns>
        public async Task<List<ChartDataPoint>> ContarPorCategoriaGeralAsync()
        {
            var list = new List<ChartDataPoint>();
            string sql = @"SELECT Categoria, COUNT(IdChamado) 
                           FROM Chamado 
                           GROUP BY Categoria";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new ChartDataPoint
                            {
                                Label = reader.GetString(0),
                                Value = reader.GetInt32(1)
                            });
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao contar chamados por categoria (Geral): " + ex.Message);
                return new List<ChartDataPoint>();
            }
        }

        /// <summary>
        /// Busca chamados com base na Prioridade e filtra por pesquisa textual.
        /// Usado na tela de 'Responder Chamado' do Técnico/Admin.
        /// </summary>
        /// <param name="prioridade">Prioridade a ser buscada (Ex: 'Alta').</param>
        /// <param name="filtroPesquisa">Termo de pesquisa livre.</param>
        /// <returns>Um DataTable com os resultados.</returns>
        public async Task<DataTable> BuscarPorPrioridadeEFiltrarAsync(string prioridade, string filtroPesquisa)
        {
            var dt = new DataTable();
            // Nota: Removemos o filtro FK_IdUsuario para mostrar chamados de TODOS os usuários
            string sql = @"
                SELECT IdChamado, Titulo, PrioridadeChamado, Descricao, DataChamado, StatusChamado, Categoria
                FROM Chamado
                WHERE PrioridadeChamado = @Prioridade
                AND (@FiltroPesquisa = '' 
                     OR Titulo LIKE @LikeFiltro 
                     OR Descricao LIKE @LikeFiltro 
                     OR Categoria LIKE @LikeFiltro)
                ORDER BY DataChamado DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Prioridade", prioridade); // Ex: "Alta", "Média", "Baixa"
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
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar chamados por prioridade: " + ex.Message);
                return new DataTable();
            }
        }


        // ====================================================================
        // IV. MÉTODOS DE ATUALIZAÇÃO (UPDATE)
        // ====================================================================

        /// <summary>
        /// Atualiza as sugestões de Prioridade, Problema e Solução geradas pela IA.
        /// Este é um método de UPDATE simples, fora de transação.
        /// </summary>
        /// <param name="id">ID do chamado a ser atualizado.</param>
        /// <param name="prioridade">Nova prioridade sugerida.</param>
        /// <param name="problema">Novo problema sugerido.</param>
        /// <param name="solucao">Nova solução sugerida.</param>
        public async Task AtualizarSugestoesIAAsync(int id, string prioridade, string problema, string solucao)
        {
            string sqlUpdate = @"UPDATE Chamado 
                               SET PrioridadeSugeridaIA = @Prioridade,
                                   ProblemaSugeridoIA = @Problema, 
                                   SolucaoSugeridaIA = @Solucao,
                                   PrioridadeChamado = @Prioridade  -- Também atualiza a prioridade oficial com a da IA
                               WHERE IdChamado = @Id";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn))
                {
                    cmdUpdate.Parameters.AddWithValue("@Prioridade", prioridade ?? "Não Definida");
                    cmdUpdate.Parameters.AddWithValue("@Problema", problema ?? "Não identificado");
                    cmdUpdate.Parameters.AddWithValue("@Solucao", solucao ?? "Sem sugestão");
                    cmdUpdate.Parameters.AddWithValue("@Id", id);

                    await conn.OpenAsync();
                    await cmdUpdate.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar sugestões da IA: " + ex.Message);
            }
        }

        /// <summary>
        /// Atualiza o status do chamado (ex: 'Pendente' para 'Em Andamento') de forma simples.
        /// Este é um método de UPDATE simples, fora de transação.
        /// </summary>
        /// <param name="idChamado">ID do chamado.</param>
        /// <param name="novoStatus">Novo status (ex: 'Resolvido').</param>
        public async Task AtualizarStatusSimplesAsync(int idChamado, string novoStatus)
        {
            // Atualiza o Status e também a DataChamado para a data atual (indicando a última modificação)
            string sql = "UPDATE Chamado SET StatusChamado = @NovoStatus, DataChamado = GETDATE() WHERE IdChamado = @IdChamado";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NovoStatus", novoStatus);
                    cmd.Parameters.AddWithValue("@IdChamado", idChamado);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar status simples: " + ex.Message);
            }
        }

        /// <summary>
        /// Atualiza as informações de Status, Prioridade e Categoria.
        /// Este método é projetado para rodar DENTRO de uma transação.
        /// </summary>
        /// <param name="idChamado">ID do chamado.</param>
        /// <param name="novoStatus">Novo status.</param>
        /// <param name="novaPrioridade">Nova prioridade (Manual do técnico).</param>
        /// <param name="novaCategoria">Nova categoria (Manual do técnico).</param>
        /// <param name="conn">Conexão SQL ativa.</param>
        /// <param name="trans">Transação SQL ativa.</param>
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
            // Não tratamos exceção aqui pois o Rollback será feito na camada Service
        }

        /// <summary>
        /// Atualiza o Status e as sugestões de IA (Problema e Solução) no contexto da análise.
        /// Este método é projetado para rodar DENTRO de uma transação.
        /// </summary>
        /// <param name="chamado">Objeto Chamado com os dados atualizados.</param>
        /// <param name="conn">Conexão SQL ativa.</param>
        /// <param name="trans">Transação SQL ativa.</param>
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
            // Não tratamos exceção aqui pois o Rollback será feito na camada Service
        }
    }
}