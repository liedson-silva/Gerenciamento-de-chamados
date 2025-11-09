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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        
        public async Task AdicionarAsync(Usuario usuario)
        {
            if (await VerificarLoginExistenteAsync(usuario.Login))
            {
                throw new Exception("O login informado já existe.");
            }

            string sql = @"INSERT INTO Usuario 
                         (Nome, CPF, RG, FuncaoUsuario, Sexo, Setor, DataDeNascimento, Email, Senha, Login) 
                         VALUES 
                         (@Nome, @CPF, @RG, @Funcao, @Sexo, @Setor, @DataNasc, @Email, @Senha, @Login)"; 

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

        
        public async Task AtualizarAsync(Usuario usuario)
        {
            if (await VerificarLoginExistenteAsync(usuario.Login, usuario.IdUsuario))
            {
                throw new Exception("O login informado já pertence a outro usuário.");
            }

            string sql = @"UPDATE Usuario SET 
                            Nome = @Nome, CPF = @CPF, RG = @RG, FuncaoUsuario = @Funcao, Sexo = @Sexo, 
                            Setor = @Setor, DataDeNascimento = @DataNasc, Email = @Email, Login = @Login 
                         WHERE IdUsuario = @Id";
            // NÃO atualiza senha aqui por segurança. Criar método separado se necessário.

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

       
        public async Task<Usuario> BuscarPorIdAsync(int id)
        {
            Usuario usuario = null;
            string sql = "SELECT * FROM Usuario WHERE IdUsuario = @IdUsuario";

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
                            CPF = reader["CPF"].ToString(),
                            RG = reader["RG"].ToString(),
                            FuncaoUsuario = reader["FuncaoUsuario"].ToString(),
                            Sexo = reader["Sexo"] != DBNull.Value ? reader["Sexo"].ToString() : string.Empty,
                            Setor = reader["Setor"].ToString(),
                            DataDeNascimento = reader["DataDeNascimento"] != DBNull.Value ? Convert.ToDateTime(reader["DataDeNascimento"]) : DateTime.MinValue,
                            Email = reader["Email"].ToString(),
                            Login = reader["Login"].ToString(),
                            
                        };
                    }
                }
            }
            return usuario;
        }

        // Baseado em GerenciarUsuarios.cs [file: GerenciarUsuarios.cs]
        public DataTable BuscarTodosFiltrados(string filtro)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT IdUsuario, Nome, Login, FuncaoUsuario AS Cargo, Email, Setor 
                         FROM Usuario 
                         WHERE @filtro = '' OR Nome LIKE @likeFiltro OR CPF LIKE @likeFiltro OR FuncaoUsuario LIKE @likeFiltro OR Setor 
                         LIKE @likeFiltro OR Email LIKE @likeFiltro OR Login LIKE @likeFiltro
                         ORDER BY Nome";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
            {
                da.SelectCommand.Parameters.AddWithValue("@filtro", filtro ?? string.Empty);
                da.SelectCommand.Parameters.AddWithValue("@likeFiltro", "%" + (filtro ?? string.Empty) + "%");
                da.Fill(dt);
            }
            return dt;
        }

        // Baseado em Login.cs [file: Login.cs]
        public async Task<Usuario> AutenticarAsync(string login, string senha)
        {
            Usuario usuario = null;
            string sql = "SELECT * FROM Usuario WHERE Login = @Login";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Login", login);
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        
                        string senhaDoBanco = reader["Senha"].ToString().Trim();

                        if (SenhaHelper.ValidarSenha(senha, senhaDoBanco))
                        {
                            usuario = new Usuario // Carrega dados do usuário logado
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
            return usuario; // Retorna null se autenticação falhar
        }

        public async Task<bool> VerificarLoginExistenteAsync(string login, int? idUsuarioExcluir = null)
        {
            string sql = "SELECT COUNT(1) FROM Usuario WHERE Login = @Login";
            if (idUsuarioExcluir.HasValue)
            {
                sql += " AND IdUsuario != @IdExcluir";
            }

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

        // Método Excluir (Adicionado)
        public async Task ExcluirAsync(int id)
        {
            // Adicionar verificação se usuário tem chamados associados antes de excluir?
            string sql = "DELETE FROM Usuario WHERE IdUsuario = @Id";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task AlterarSenhaAsync(int idUsuario, string novaSenha)
        {
            // Validação básica
            if (string.IsNullOrWhiteSpace(novaSenha) || novaSenha.Length < 0)
            {
                throw new Exception("A nova senha deve ter no mínimo 4 caracteres.");
            }

            // Gera o hash seguro usando o SenhaHelper
            string hashSenha = SenhaHelper.GerarHashSenha(novaSenha);

            string sql = "UPDATE Usuario SET Senha = @Senha WHERE IdUsuario = @Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Senha", hashSenha);
                cmd.Parameters.AddWithValue("@Id", idUsuario);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}