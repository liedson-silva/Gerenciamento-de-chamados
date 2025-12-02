using Gerenciamento_De_Chamados.Models;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Repositories
{
    /// <summary>
    /// Interface para o Repositório de Arquivos.
    /// Define as operações de acesso a dados para a entidade Arquivo (anexos dos chamados).
    /// </summary>
    public interface IArquivoRepository
    {
        /// <summary>
        /// Adiciona um novo arquivo (anexo) associado a um chamado ao banco de dados.
        /// </summary>
        /// <param name="arquivo">O objeto Arquivo contendo o tipo, nome e os bytes do anexo.</param>
        Task AdicionarAsync(Arquivo arquivo);
    }
}