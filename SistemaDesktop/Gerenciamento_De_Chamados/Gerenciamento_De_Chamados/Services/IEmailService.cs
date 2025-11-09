using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Services
{
    public interface IEmailService
    {
        Task EnviarEmailChamadoAsync(
            string titulo, string descricao, string categoria, int idChamado,
            string prioridadeIA, string status, string pessoasAfetadas,
            string impedeTrabalho, string ocorreuAnteriormente, string problemaIA, string solucaoIA,
            DateTime dataAbertura, byte[] anexo, string nomeAnexo
        );
    }
}
