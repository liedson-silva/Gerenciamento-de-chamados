using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Services
{
    public interface IAIService
    {
            Task<(string problema, string prioridade, string solucao)> AnalisarChamado(
            string titulo, string pessoaAfetadas, string ocorreuAnteriormente,
            string impedeTrabalho, string descricao, string categoria,
            List<string> solucoesAnteriores);
    }
}