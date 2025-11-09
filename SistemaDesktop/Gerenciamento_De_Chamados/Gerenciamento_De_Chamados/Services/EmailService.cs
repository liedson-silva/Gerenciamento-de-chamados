using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados.Services
{
    public class EmailService : IEmailService
    {
        
        private string bolinhadeprioridade(string prioridade)
        {
            string estiloBase = "display: inline-block; width: 12px; height: 12px; border-radius: 50%; margin-right: 6px; vertical-align: middle;";
            string prioridadeNormalizada = (prioridade ?? "").Trim().ToLower();

            switch (prioridadeNormalizada)
            {
                case "alta":
                    return $"<span style='{estiloBase} background-color: red;'></span>";
                case "media":
                case "média":
                    return $"<span style='{estiloBase} background-color: #FFA500;'></span>";
                case "baixa":
                case "baixo":
                    return $"<span style='{estiloBase} background-color: green;'></span>";
                default:
                    return $"<span style='{estiloBase} background-color: gray;'></span>";
            }
        }


        public async Task EnviarEmailChamadoAsync(
            string titulo, string descricao, string categoria, int idChamado,
            string prioridadeIA, string status, string pessoasAfetadas,
            string impedeTrabalho, string ocorreuAnteriormente, string problemaIA, string solucaoIA, 
            DateTime dataAbertura,
            byte[] anexo, string nomeAnexo)
        {

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("fatalsystem.unip@gmail.com", "wwtr xdst wpwm lavr");
                smtp.EnableSsl = true;

                try
                {
                    // Envio para a Equipe de TI 

                    string usuario = SessaoUsuario.Nome ?? "Usuário não identificado";
                    string emailUsuario = SessaoUsuario.Email ?? "sememail@dominio.com";
                    string emailTecnico = "fatalsystem.unip@gmail.com";
                    string indicadorPrioridadeHtml = bolinhadeprioridade(prioridadeIA);

                    string corpoEmailTI = $@"
                        <h2>Novo Chamado #{idChamado} Recebido - Análise Necessária</h2>
                        <p>Um novo chamado foi registrado e precisa de análise.</p>
                        <p><b>Registrado por:</b> {usuario} ({emailUsuario})</p>
                        <p><b>Número:</b> {idChamado}</p>
                        <p><b>Data:</b> {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p>
                        <hr>
                        <p><b>Título:</b> {titulo}</p>
                        <p><b>Descrição do Usuário:</b> {descricao}</p>
                        <p><b>Categoria:</b> {categoria}</p>
                        <p><b>Status Inicial:</b> {status}</p>
                        <hr>
                        <p><b>Pessoas Afetadas:</b> {pessoasAfetadas}</p>
                        <p><b>Impede o Trabalho:</b> {impedeTrabalho}</p>
                        <p><b>Ocorreu Anteriormente:</b> {ocorreuAnteriormente}</p>
                        <hr>
                        <h3>Sugestões da Análise Preliminar (IA):</h3>
                        <p><b>Identificação do Problema:</b> {problemaIA}</p>
                        <p><b>Proposta de Solução:</b> {solucaoIA}</p>
                        <p><b>Prioridade Sugerida:</b> {prioridadeIA} {indicadorPrioridadeHtml}</p>
                        <hr>";

                    using (MailMessage mailTI = new MailMessage())
                    {
                        mailTI.From = new MailAddress("fatalsystem.unip@gmail.com", "Sistema de Chamados Fatal System");
                        mailTI.To.Add(emailTecnico);
                        mailTI.Subject = $"[Chamado #{idChamado} - {prioridadeIA}] Novo Chamado: {titulo}";
                        mailTI.Body = corpoEmailTI;
                        mailTI.IsBodyHtml = true;

                       
                        if (anexo != null && anexo.Length > 0)
                        {
                            using (MemoryStream msTI = new MemoryStream(anexo))
                            using (Attachment attachmentTI = new Attachment(msTI, nomeAnexo))
                            {
                                mailTI.Attachments.Add(attachmentTI);
                                await smtp.SendMailAsync(mailTI); 
                            }
                        }
                        else
                        {
                            await smtp.SendMailAsync(mailTI);
                        }
                    }

                   
                    if (!string.IsNullOrEmpty(emailUsuario) && emailUsuario != "sememail@dominio.com")
                    {
                        string corpoEmailUsuario = $@"
                            <h2>Olá, {usuario}!</h2>
                            <p>Seu chamado foi registrado com sucesso em nosso sistema.</p>
                            <p><b>Número do Chamado:</b> {idChamado}</p>
                            <p><b>Data:</b> {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p>
                            <hr>
                            <p><b>Título:</b> {titulo}</p>
                            <p><b>Descrição Fornecida:</b> {descricao}</p>
                            <p><b>Categoria:</b> {categoria}</p>
                            <p><b>Prioridade Definida: Em análise </b></p>
                            <p><b>Status Atual:</b> {status}</p>
                            <hr>
                            <p>Nossa equipe de TI já foi notificada e analisará sua solicitação em breve.</p>
                            <p>Atenciosamente,<br>Equipe Fatal System</p>";

                        using (MailMessage mailUsuario = new MailMessage())
                        {
                            mailUsuario.From = new MailAddress("fatalsystem.unip@gmail.com", "Sistema de Chamados Fatal System");
                            mailUsuario.To.Add(emailUsuario); 
                            mailUsuario.Subject = $"Confirmação: Chamado #{idChamado} Registrado - {titulo}";
                            mailUsuario.Body = corpoEmailUsuario;
                            mailUsuario.IsBodyHtml = true;

                            if (anexo != null && anexo.Length > 0)
                            {
                             
                                using (MemoryStream msUser = new MemoryStream(anexo))
                                using (Attachment attachmentUser = new Attachment(msUser, nomeAnexo))
                                {
                                    mailUsuario.Attachments.Add(attachmentUser);
                                    await smtp.SendMailAsync(mailUsuario); 
                                }
                            }
                            else
                            {
                                await smtp.SendMailAsync(mailUsuario);
                            }
                        }
                    }
                }
            
                catch (Exception ex)
                {
                    
                    string mensagemErro = "Erro ao enviar e-mail(s): " + ex.Message;
                    if (ex.InnerException != null)
                    {
                        mensagemErro += "\n\nDetalhes: " + ex.InnerException.Message;
                    }
                    MessageBox.Show(mensagemErro, "Falha no Envio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
        }
    }
}