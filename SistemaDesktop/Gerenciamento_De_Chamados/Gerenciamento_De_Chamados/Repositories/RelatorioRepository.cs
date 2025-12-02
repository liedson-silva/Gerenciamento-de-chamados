using Gerenciamento_De_Chamados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Repositories
{
    /// <summary>
    /// Implementação do IRelatorioRepository, responsável por acessar e agregar dados
    /// de chamados para a geração de relatórios e gráficos.
    /// </summary>
    public class RelatorioRepository : IRelatorioRepository
    {
       
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        /// <summary>
        /// Gera um <see cref="DataTable"/> com informações detalhadas dos chamados em um período.
        /// Este relatório inclui dados de Chamado, Usuário e a Solução do Histórico.
        /// </summary>
        /// <param name="dataInicio">Data inicial (inclusiva) para o filtro.</param>
        /// <param name="dataFim">Data final (inclusiva) para o filtro.</param>
        /// <returns>DataTable com os resultados do relatório.</returns>
        public async Task<DataTable> GerarRelatorioDetalhadoAsync(DateTime dataInicio, DateTime dataFim)
        {
            var relatorioDataTable = new DataTable();

            // Query SQL que une Chamado, Usuário e Histórico 
            string query = @"
                SELECT 
                    c.IdChamado AS Protocolo,
                    c.DataChamado AS Abertura,
                    c.Titulo,
                    c.Categoria,
                    c.Descricao AS Detalhes,
                    c.StatusChamado AS Status,
                    u.Nome AS Solicitante,
                    h.Solucao AS SolucaoAplicada
                FROM 
                    Chamado c
                JOIN 
                    Usuario u ON c.FK_IdUsuario = u.IdUsuario
                LEFT JOIN 
                    -- Usamos LEFT JOIN para garantir que chamados sem histórico de solução apareçam
                    Historico h ON c.IdChamado = h.FK_IdChamado AND h.Acao = 'Solução Aplicada'
                WHERE 
                    c.DataChamado BETWEEN @dataInicio AND @dataFim
                ORDER BY
                    c.DataChamado DESC;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                   
                    command.Parameters.AddWithValue("@dataInicio", dataInicio.Date);

                    command.Parameters.AddWithValue("@dataFim", dataFim.Date.AddDays(1).AddTicks(-1));

                    await Task.Run(() => adapter.Fill(relatorioDataTable));
                }
                return relatorioDataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar relatório detalhado: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Busca dados de contagem de chamados agrupados por Status.
        /// </summary>
        /// <returns>Lista de <see cref="ChartDataPoint"/> para o gráfico de Status.</returns>
        public async Task<List<ChartDataPoint>> GetStatusChartDataAsync()
        {
            var dataPoints = new List<ChartDataPoint>();

            string query = @"
                SELECT StatusChamado, COUNT(IdChamado) 
                FROM Chamado 
                -- Filtra apenas os status relevantes para o gráfico
                WHERE StatusChamado IN ('Pendente', 'Em Andamento', 'Resolvido') 
                GROUP BY StatusChamado";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        dataPoints.Add(new ChartDataPoint
                        {
                            Label = reader.GetString(0),
                            Value = reader.GetInt32(1),
                            Count = reader.GetInt32(1)
                        });
                    }
                }
            }
            return dataPoints;
        }

        /// <summary>
        /// Busca dados de contagem de chamados agrupados por Prioridade.
        /// </summary>
        /// <returns>Lista de <see cref="ChartDataPoint"/> para o gráfico de Prioridade.</returns>
        public async Task<List<ChartDataPoint>> GetPriorityChartDataAsync()
        {
            var dataPoints = new List<ChartDataPoint>();

            string query = @"
                SELECT PrioridadeChamado, COUNT(IdChamado) 
                FROM Chamado 
                -- Filtra apenas as prioridades padrões
                WHERE PrioridadeChamado IN ('Alta', 'Média', 'Baixa') 
                GROUP BY PrioridadeChamado";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        dataPoints.Add(new ChartDataPoint
                        {
                            Label = reader.GetString(0),
                            Value = reader.GetInt32(1),
                            Count = reader.GetInt32(1)
                        });
                    }
                }
            }
            return dataPoints;
        }
    }
}