using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Services
{
    /// <summary>
    /// Esta interface define o contrato do serviço de Inteligência Artificial.
    /// É o ponto de contato do nosso sistema com o modelo Gemini para a triagem de chamados.
    /// Isso garante que a aplicação (Windows Forms) não dependa diretamente da API do Google, 
    /// facilitando testes e futuras trocas de tecnologia de IA.
    /// </summary>
    public interface IAIService
    {
        /// <summary>
        /// Método principal que realiza a análise de um chamado recém-criado.
        /// Ele utiliza o contexto do chamado e o histórico de soluções anteriores para solicitar 
        /// a classificação de prioridade e sugestões de solução.
        /// </summary>
        Task<(string problema, string prioridade, string solucao)> AnalisarChamado(
            string titulo, string pessoaAfetadas, string ocorreuAnteriormente,
            string impedeTrabalho, string descricao, string categoria,
            List<string> solucoesAnteriores);
    }
}