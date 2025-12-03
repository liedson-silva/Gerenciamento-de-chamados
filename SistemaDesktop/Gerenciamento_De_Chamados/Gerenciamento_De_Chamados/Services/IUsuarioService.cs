using Gerenciamento_De_Chamados.Models;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Services
{
    /// <summary>
    ///  Esta interface define o Contrato do Serviço de Lógica de Negócio para a entidade Usuário.
    /// 
    ///  Ela serve como uma camada intermediária entre a tela  
    /// e a camada de acesso a dados (Repository), garantindo que todas as regras de negócio complexas, 
    /// como validação de CPF, força de senha e verificação de login duplicado, sejam executadas
    /// ANTES de interagir com o banco de dados.
    /// </summary>
    public interface IUsuarioService
    {
        /// <summary>
        /// Método assíncrono para adicionar um novo usuário ao sistema.
        /// Este método contém toda a lógica de validação do objeto Usuario antes de persistir no banco de dados.
        /// </summary>
        /// <returns>Uma Task para operação assíncrona.</returns>
        Task AdicionarUsuarioAsync(Usuario novoUsuario);
        
    }
}