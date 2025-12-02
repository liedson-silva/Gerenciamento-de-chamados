using Gerenciamento_De_Chamados.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gerenciamento_De_Chamados.Helpers; // Necessário para SenhaHelper, se estiver na pasta Helpers

namespace Gerenciamento_De_Chamados.Repositories
{
    /// <summary>
    /// Repositório de dados para a entidade Usuario, implementando IUsuarioRepository.
    /// Gerencia o CRUD, Autenticação e validações de unicidade de login.
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // ====================================================================
        // I. CRIAÇÃO (ADD)
        // ====================================================================

        /// <summary>
        /// Adiciona um novo usuário ao banco de dados, realizando a verificação de login antes.
        /// </summary>
        /// <param name="usuario">Dados do novo usuário, incluindo a senha em texto claro.</param>
        public async Task AdicionarAsync(Usuario usuario)
        {
            // 1. Verifica se o login já existe no banco
            if (await VerificarLoginExistenteAsync(usuario.Login))
            {
                throw new Exception("O login informado já existe.");
            }

            // 2. Query de inserção
            string sql = @"INSERT INTO Usuario 
                         (Nome, CPF, RG, FuncaoUsuario, Sexo, Setor, DataDeNascimento, Email, Senha, Login) 
                         VALUES 
                         (@Nome, @CPF, @RG, @Funcao, @Sexo, @Setor, @DataNasc, @Email, @Senha, @Login)";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@CPF", (object)usuario.CPF ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RG", (object)usuario.RG ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Funcao", usuario.FuncaoUsuario);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo.ToString());
                    cmd.Parameters.AddWithValue("@Setor", usuario.Setor);
                    cmd.Parameters.AddWithValue("@DataNasc", usuario.DataDeNascimento);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Senha", SenhaHelper.GerarHashSenha(usuario.Senha));
                    cmd.Parameters.AddWithValue("@Login", usuario.Login);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro SQL ao adicionar usuário: {ex.Message}");
                throw new Exception("Erro ao salvar o usuário no banco de dados.");
            }
        }

        // ====================================================================
        // II. ATUALIZAÇÃO (UPDATE)
        // ====================================================================

        /// <summary>
        /// Atualiza os dados cadastrais de um usuário (sem alterar a senha).
        /// </summary>
        /// <param name="usuario">Objeto Usuario com os dados a serem atualizados.</param>
        public async Task AtualizarAsync(Usuario usuario)
        {
            // 1. Verifica se o login já existe, excluindo o próprio ID do usuário da checagem
            if (await VerificarLoginExistenteAsync(usuario.Login, usuario.IdUsuario))
            {
                throw new Exception("O login informado já pertence a outro usuário.");
            }

            // 2. Query de atualização (sem o campo Senha)
            string sql = @"UPDATE Usuario SET 
                            Nome = @Nome, CPF = @CPF, RG = @RG, FuncaoUsuario = @Funcao, Sexo = @Sexo, 
                            Setor = @Setor, DataDeNascimento = @DataNasc, Email = @Email, Login = @Login 
                          WHERE IdUsuario = @Id";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@CPF", (object)usuario.CPF ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RG", (object)usuario.RG ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Funcao", usuario.FuncaoUsuario);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo.ToString());
                    cmd.Parameters.AddWithValue("@Setor", usuario.Setor);
                    cmd.Parameters.AddWithValue("@DataNasc", usuario.DataDeNascimento);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Login", usuario.Login);
                    cmd.Parameters.AddWithValue("@Id", usuario.IdUsuario);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro SQL ao atualizar usuário: {ex.Message}");
                throw new Exception("Erro ao atualizar o usuário no banco de dados.");
            }
        }

        /// <summary>
        /// Altera a senha de um usuário específico, gerando um novo hash.
        /// </summary>
        /// <param name="idUsuario">ID do usuário a ter a senha alterada.</param>
        /// <param name="novaSenha">Nova senha em texto claro.</param>
        public async Task AlterarSenhaAsync(int idUsuario, string novaSenha)
        {
            if (string.IsNullOrWhiteSpace(novaSenha) || novaSenha.Length < 4)
            {

                throw new Exception("A nova senha deve ter no mínimo 4 caracteres.");
            }
            string hashSenha = SenhaHelper.GerarHashSenha(novaSenha);

            string sql = "UPDATE Usuario SET Senha = @Senha WHERE IdUsuario = @Id";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Senha", hashSenha);
                    cmd.Parameters.AddWithValue("@Id", idUsuario);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro SQL ao alterar senha: {ex.Message}");
                throw new Exception("Erro ao alterar a senha do usuário no banco de dados.");
            }
        }

        /// <summary>
        /// Exclui um usuário do banco de dados pelo seu ID.
        /// </summary>
        /// <param name="id">ID do usuário a ser excluído.</param>
        public async Task ExcluirAsync(int id)
        {

            string sql = "DELETE FROM Usuario WHERE IdUsuario = @Id";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro SQL ao excluir usuário: {ex.Message}");
                // Se houver FK (chave estrangeira), este é o ponto onde a exceção é mais provável
                throw new Exception("Não é possível excluir o usuário. Ele possui chamados associados.");
            }
        }

        // ====================================================================
        // III. LEITURA E BUSCA
        // ====================================================================

        /// <summary>
        /// Busca um usuário específico pelo seu ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Objeto Usuario preenchido ou null.</returns>
        public async Task<Usuario> BuscarPorIdAsync(int id)
        {
            Usuario usuario = null;
            string sql = "SELECT * FROM Usuario WHERE IdUsuario = @IdUsuario";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", id);
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                Nome = reader["Nome"].ToString(),
                                CPF = reader["CPF"] != DBNull.Value ? reader["CPF"].ToString() : string.Empty,
                                RG = reader["RG"] != DBNull.Value ? reader["RG"].ToString() : string.Empty,
                                FuncaoUsuario = reader["FuncaoUsuario"].ToString(),
                                Sexo = reader["Sexo"] != DBNull.Value ? reader["Sexo"].ToString() : string.Empty,
                                Setor = reader["Setor"].ToString(),
                                DataDeNascimento = reader["DataDeNascimento"] != DBNull.Value ? Convert.ToDateTime(reader["DataDeNascimento"]) : DateTime.MinValue,
                                Email = reader["Email"].ToString(),
                                Login = reader["Login"].ToString(),
                                // Senha não é carregada por segurança
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário por ID: {ex.Message}");
                return null;
            }
            return usuario;
        }

        /// <summary>
        /// Busca todos os usuários, aplicando filtro na consulta SQL.
        /// Usado na DataGridView da tela GerenciarUsuarios.
        /// </summary>
        /// <param name="filtro">Texto para pesquisar em Nome, Login, Cargo, Setor e Email.</param>
        /// <returns>DataTable com os resultados.</returns>
        public DataTable BuscarTodosFiltrados(string filtro)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT IdUsuario, Nome, Login, FuncaoUsuario AS Cargo, Email, Setor 
                           FROM Usuario 
                           WHERE @filtro = '' 
                              OR Nome LIKE @likeFiltro 
                              OR CPF LIKE @likeFiltro 
                              OR FuncaoUsuario LIKE @likeFiltro 
                              OR Setor LIKE @likeFiltro 
                              OR Email LIKE @likeFiltro 
                              OR Login LIKE @likeFiltro
                           ORDER BY Nome";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    // Trata filtro nulo e adiciona parâmetros
                    string filtroTratado = filtro ?? string.Empty;
                    da.SelectCommand.Parameters.AddWithValue("@filtro", filtroTratado);
                    da.SelectCommand.Parameters.AddWithValue("@likeFiltro", $"%{filtroTratado}%");
                    da.Fill(dt);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro SQL ao buscar usuários filtrados: {ex.Message}");

                return new DataTable();
            }
            return dt;
        }

        // ====================================================================
        // IV. AUTENTICAÇÃO E VALIDAÇÃO
        // ====================================================================

        /// <summary>
        /// Autentica o usuário comparando a senha fornecida com o hash armazenado.
        /// </summary>
        /// <param name="login">Login do usuário.</param>
        /// <param name="senha">Senha em texto claro.</param>
        /// <returns>Objeto Usuario preenchido (se a senha for válida) ou null.</returns>
        public async Task<Usuario> AutenticarAsync(string login, string senha)
        {
            Usuario usuario = null;
            string sql = "SELECT IdUsuario, Nome, FuncaoUsuario, Login, Email, Senha FROM Usuario WHERE Login = @Login";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Login", login);
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // 1. Obtém o hash da senha armazenado no banco
                            string senhaDoBanco = reader["Senha"].ToString().Trim();

                            // 2. Valida a senha usando o SenhaHelper
                            if (SenhaHelper.ValidarSenha(senha, senhaDoBanco))
                            {
                                usuario = new Usuario // Carrega dados essenciais para a sessão
                                {
                                    IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                    Nome = reader["Nome"].ToString(),
                                    FuncaoUsuario = reader["FuncaoUsuario"].ToString(),
                                    Login = reader["Login"].ToString(),
                                    Email = reader["Email"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na autenticação: {ex.Message}");
            }
            return usuario; 
        }

        /// <summary>
        /// Verifica se um login já está cadastrado.
        /// </summary>
        /// <param name="login">O login a ser verificado.</param>
        /// <param name="idUsuarioExcluir">ID do usuário a ser ignorado na checagem (usado na edição).</param>
        /// <returns>True se o login já existir, False caso contrário.</returns>
        public async Task<bool> VerificarLoginExistenteAsync(string login, int? idUsuarioExcluir = null)
        {
            string sql = "SELECT COUNT(1) FROM Usuario WHERE Login = @Login";

            if (idUsuarioExcluir.HasValue)
            {
                sql += " AND IdUsuario != @IdExcluir";
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Login", login);
                    if (idUsuarioExcluir.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@IdExcluir", idUsuarioExcluir.Value);
                    }
                    await conn.OpenAsync();
                    int count = (int)await cmd.ExecuteScalarAsync();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao verificar login existente: {ex.Message}");

                return true; 
            }
        }
    }
}