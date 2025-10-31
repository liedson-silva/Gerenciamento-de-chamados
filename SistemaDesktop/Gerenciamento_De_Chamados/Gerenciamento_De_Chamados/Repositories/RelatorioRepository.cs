using Gerenciamento_De_Chamados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Repositories
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public async Task<DataTable> GerarRelatorioDetalhadoAsync(DateTime dataInicio, DateTime dataFim)
        {
            var relatorioDataTable = new DataTable();


            string query = @"
                SELECT 
                    c.IdChamado,
                    c.DataChamado,
                    c.Titulo,
                    c.Categoria,
                    c.Descricao,
                    c.StatusChamado,
                    u.Nome AS Usuario,
                    h.Solucao
                FROM 
                    Chamado c
                JOIN 
                    Usuario u ON c.FK_IdUsuario = u.IdUsuario
                LEFT JOIN 
                    Historico h ON c.IdChamado = h.FK_IdChamado
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
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<ChartDataPoint>> GetStatusChartDataAsync()
        {
            var dataPoints = new List<ChartDataPoint>();
            // A query é a mesma que estava no seu formulário
            string query = @"
                SELECT StatusChamado, COUNT(IdChamado) 
                FROM Chamado 
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
                            Value = reader.GetInt32(1)
                        });
                    }
                }
            }
            return dataPoints;
        }
        public async Task<List<ChartDataPoint>> GetPriorityChartDataAsync()
        {
            var dataPoints = new List<ChartDataPoint>();
            // A query é a mesma que estava no seu formulário
            string query = @"
                SELECT PrioridadeChamado, COUNT(IdChamado) 
                FROM Chamado 
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
                            Value = reader.GetInt32(1)
                        });
                    }
                }
            }
            return dataPoints;
        }
    }
}