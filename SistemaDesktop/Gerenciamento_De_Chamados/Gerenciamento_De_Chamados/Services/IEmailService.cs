using Gerenciamento_De_Chamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Services
{
    public interface IEmailService
    {
        
        Task EnviarEmailConfirmacaoUsuarioAsync(Chamado chamado, Usuario usuario, int idChamado);


       
        Task EnviarEmailNovoChamadoTIAsync(Chamado chamado, Usuario usuario, int idChamado, byte[] anexo, string nomeAnexo);
    }
}
