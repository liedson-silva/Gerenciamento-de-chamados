using System;
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
    }
}