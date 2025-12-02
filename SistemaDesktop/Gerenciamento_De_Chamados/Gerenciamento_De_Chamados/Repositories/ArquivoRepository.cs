using Gerenciamento_De_Chamados.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados.Repositories
{
    /// <summary>
    /// Esta classe é o Repositório que gerencia a comunicação com a tabela 'Arquivo' no banco de dados.
    /// É a ponte entre a nossa aplicação e a persistência dos anexos.
    /// </summary>
    public class ArquivoRepository : IArquivoRepository
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
        // II. MÉTODOS DE ESCRITA NO BANCO
        // ====================================================================

        /// <summary>
        /// Pega um objeto 'Arquivo' (que contém os bytes do anexo) e salva no banco de dados.
        /// Este é um método assíncrono para não travar a aplicação enquanto o banco processa.
        /// </summary>
        /// <param name="arquivo">O objeto Arquivo com todos os dados do anexo.</param>
        public async Task AdicionarAsync(Arquivo arquivo)
        {
            // O comando SQL para inserir um novo arquivo na tabela
            string sqlArquivo = @"INSERT INTO Arquivo 
                                (TipoArquivo, NomeArquivo, Arquivo, FK_IdChamado)
                                VALUES (@TipoArquivo, @NomeArquivo, @Arquivo, @FK_IdChamado)";

            try
            {
                // Conexão e o comando que será executado no BD
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmdArq = new SqlCommand(sqlArquivo, conn))
                {
                    // Passamos os valores do objeto Arquivo para os parâmetros do SQL
                    cmdArq.Parameters.AddWithValue("@TipoArquivo", arquivo.TipoArquivo);
                    cmdArq.Parameters.AddWithValue("@NomeArquivo", arquivo.NomeArquivo);
                    cmdArq.Parameters.AddWithValue("@Arquivo", arquivo.ArquivoBytes);
                    cmdArq.Parameters.AddWithValue("@FK_IdChamado", arquivo.FK_IdChamado);

                    // Abrimos a conexão e executamos o comando de inserção
                    await conn.OpenAsync();
                    await cmdArq.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Erro ao anexar arquivo: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
              
                throw;
            }
        }
    }
}