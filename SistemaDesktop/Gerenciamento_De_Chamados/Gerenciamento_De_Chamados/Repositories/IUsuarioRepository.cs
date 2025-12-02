using Gerenciamento_De_Chamados.Models;
using System.Collections.Generic; 
using System.Data; 
using System.Threading.Tasks; 

namespace Gerenciamento_De_Chamados.Repositories
{
    /// <summary>
    /// Interface para o Repositório de Usuários.
    /// Define todas as operações de CRUD, autenticação e validação para a entidade Usuario.
    /// </summary>
    public interface IUsuarioRepository
    {
        // ====================================================================
        // I. CRIAÇÃO, ATUALIZAÇÃO E EXCLUSÃO (CRUD)
        // ====================================================================

        /// <summary>
        /// Adiciona um novo usuário ao banco de dados (Cadastro).
        /// </summary>
        /// <param name="usuario">Objeto Usuario a ser salvo.</param>
        Task AdicionarAsync(Usuario usuario);

        /// <summary>
        /// Atualiza os dados cadastrais de um usuário existente.
        /// </summary>
        /// <param name="usuario">Objeto Usuario com os dados atualizados.</param>
        Task AtualizarAsync(Usuario usuario);

        /// <summary>
        /// Altera a senha de um usuário, recebendo o ID e a nova senha em texto claro (que será hasheada no repositório).
        /// </summary>
        Task AlterarSenhaAsync(int idUsuario, string novaSenha);

        // ====================================================================
        // II. LEITURA E BUSCA
        // ====================================================================

        /// <summary>
        /// Busca um usuário específico pelo seu ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Objeto Usuario preenchido.</returns>
        Task<Usuario> BuscarPorIdAsync(int id);

        /// <summary>
        /// Busca todos os usuários, permitindo filtro textual em Nome, Login e Outros.
        /// Usado na tela de gerenciamento de usuários (Admin).
        /// </summary>
        /// <param name="filtro">Termo de pesquisa.</param>
        /// <returns>Um DataTable com os resultados.</returns>
        DataTable BuscarTodosFiltrados(string filtro);

        // ====================================================================
        // III. AUTENTICAÇÃO E VALIDAÇÃO
        // ====================================================================

        /// <summary>
        /// Autentica um usuário pelo Login e Senha.
        /// </summary>
        /// <param name="login">Login digitado.</param>
        /// <param name="senha">Senha em texto claro (para ser comparada com o hash).</param>
        /// <returns>Objeto Usuario autenticado ou null se falhar.</returns>
        Task<Usuario> AutenticarAsync(string login, string senha);

        /// <summary>
        /// Verifica se um Login já está em uso no banco de dados.
        /// Útil durante o cadastro e edição para garantir unicidade.
        /// </summary>
        /// <param name="login">Login a ser verificado.</param>
        /// <param name="idUsuarioExcluir">ID a ser excluído da verificação (durante edição).</param>
        /// <returns>True se o login já existir, False caso contrário.</returns>
        Task<bool> VerificarLoginExistenteAsync(string login, int? idUsuarioExcluir = null);
    }
}