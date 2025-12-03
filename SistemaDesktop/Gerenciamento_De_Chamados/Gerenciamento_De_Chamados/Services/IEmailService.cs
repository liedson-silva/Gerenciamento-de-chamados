using Gerenciamento_De_Chamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Services
{
    /// <summary>
    /// Esta interface define o contrato de todas as notificações do sistema.
    /// Ela garante que a aplicação possa enviar e-mails de forma assíncrona, desacoplando 
    /// a lógica de negócio do serviço de envio.
    /// </summary>
    public interface IEmailService
    {

        /// <summary>
        /// Responsável por enviar a confirmação de registro de um chamado para o funcionário que o abriu.
        /// Garante ao usuário a transparência de que a requisição entrou no sistema.
        /// </summary>
        Task EnviarEmailConfirmacaoUsuarioAsync(Chamado chamado, int idChamado, string emailDestino, string nomeDestino);

        /// <summary>
        ///  Este método notifica a equipe de Suporte (TI) sobre o novo chamado.
        /// É aqui que enviamos as sugestões de triagem geradas pela nossa Inteligência Artificial (Prioridade, Problema, Solução).
        /// Também é responsável por anexar arquivos enviados pelo usuário.
        /// </summary>
        Task EnviarEmailNovoChamadoTIAsync(Chamado chamado, Usuario usuario, int idChamado, byte[] anexo, string nomeAnexo);

        /// <summary>
        /// Envia um e-mail ao usuário final informando que o problema foi resolvido e
        /// inclui a descrição da solução aplicada pelo técnico (Solução Final).
        /// </summary>
        Task EnviarEmailResolucaoUsuarioAsync(Chamado chamado, Usuario usuario, string solucao);


        /// <summary>
        /// Método orquestrador que envia os e-mails iniciais (Confirmação para Usuário e Alerta para TI)
        /// de uma única vez.
        /// </summary>
        Task EnviarEmailChamadoAsync(
            string titulo, string descricao, string categoria, int idChamado,
            string prioridadeIA, string status, string pessoasAfetadas,
            string impedeTrabalho, string ocorreuAnteriormente, string problemaIA, string solucaoIA,
            DateTime dataAbertura, byte[] anexo, string nomeAnexo
        );
    }
}