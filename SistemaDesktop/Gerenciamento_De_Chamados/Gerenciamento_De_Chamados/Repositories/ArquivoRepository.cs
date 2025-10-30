
using Gerenciamento_De_Chamados.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados.Repositories
{
    public class ArquivoRepository : IArquivoRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public async Task AdicionarAsync(Arquivo arquivo)
        {
            string sqlArquivo = @"INSERT INTO Arquivo 
                                (TipoArquivo, NomeArquivo, Arquivo, FK_IdChamado)
                                VALUES (@TipoArquivo, @NomeArquivo, @Arquivo, @FK_IdChamado)";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmdArq = new SqlCommand(sqlArquivo, conn))
                {
                    cmdArq.Parameters.AddWithValue("@TipoArquivo", arquivo.TipoArquivo);
                    cmdArq.Parameters.AddWithValue("@NomeArquivo", arquivo.NomeArquivo);
                    cmdArq.Parameters.AddWithValue("@Arquivo", arquivo.ArquivoBytes);
                    cmdArq.Parameters.AddWithValue("@FK_IdChamado", arquivo.FK_IdChamado);

                    await conn.OpenAsync();
                    await cmdArq.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao anexar arquivo: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Lança a exceção para o form saber que falhou
                throw;
            }
        }
    }
}