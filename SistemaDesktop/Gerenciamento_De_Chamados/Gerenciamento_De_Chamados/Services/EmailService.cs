using Gerenciamento_De_Chamados.Models;
using System;
using System.IO;
using System.Linq.Expressions;
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


        public async Task EnviarEmailConfirmacaoUsuarioAsync(Chamado chamado, Usuario usuario, int idChamado)
        {
           
            if (usuario == null || string.IsNullOrEmpty(usuario.Email) || usuario.Email == "sememail@dominio.com")
            {

                Console.WriteLine($"Email do usuário (ID: {usuario?.IdUsuario}) inválido ou não fornecido. Pulando envio de confirmação.");
                return; 
            }

            string corpoEmailUsuario = $@"
                <h2>Olá, {usuario.Nome}!</h2>
                <p>Seu chamado foi registrado com sucesso em nosso sistema.</p>
                <p><b>Número do Chamado:</b> {idChamado}</p>
                <p><b>Data:</b> {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p>
                <hr>
                <p><b>Título:</b> {chamado.Titulo}</p>
                <p><b>Descrição Fornecida:</b> {chamado.Descricao}</p>
                <p><b>Categoria:</b> {chamado.Categoria}</p>
                <p><b>Prioridade Definida:</b> Em análise</p>
                <p><b>Status Atual:</b> {chamado.StatusChamado}</p>
                <hr>
                <p>Nossa equipe de TI já foi notificada e analisará sua solicitação em breve.</p>
                <p>Atenciosamente,<br>Equipe Fatal System</p>";

        
            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            using (MailMessage mailUsuario = new MailMessage())
            {
                smtp.Credentials = new NetworkCredential("fatalsystem.unip@gmail.com", "wwtr xdst wpwm lavr");
                smtp.EnableSsl = true;

                mailUsuario.From = new MailAddress("fatalsystem.unip@gmail.com", "Sistema de Chamados Fatal System");
                mailUsuario.To.Add(usuario.Email);
                mailUsuario.Subject = $"Confirmação: Chamado #{idChamado} Registrado - {chamado.Titulo}";
                mailUsuario.Body = corpoEmailUsuario;
                mailUsuario.IsBodyHtml = true;

                try
                {
                    await smtp.SendMailAsync(mailUsuario);
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Falha ao enviar email de confirmação para {usuario.Email}: {ex.Message}");
                }
            }
        }
        public async Task EnviarEmailResolucaoUsuarioAsync(Chamado chamado, Usuario usuario, string solucao)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Email) || usuario.Email == "sememail@dominio.com")
            {
                Console.WriteLine("Email do usuário inválido para envio de resolução.");
                return;
            }

            string corpoEmail = $@"
                <div style='font-family: Arial, sans-serif; color: #333;'>
                    <h2 style='color: #4CAF50;'>Chamado #{chamado.IdChamado} Resolvido!</h2>
                    <p>Olá, <b>{usuario.Nome}</b>.</p>
                    <p>Informamos que seu chamado foi concluído pela nossa equipe técnica.</p>
                    <hr style='border: 1px solid #eee;'>
                    <p><b>Título:</b> {chamado.Titulo}</p>
                    <p><b>Data Abertura:</b> {chamado.DataChamado:dd/MM/yyyy}</p>
                    <br>
                    <div style='background-color: #f9f9f9; padding: 15px; border-left: 5px solid #4CAF50;'>
                        <p style='margin-top: 0;'><b>Solução Aplicada:</b></p>
                        <p>{solucao}</p>
                    </div>
                    <br>
                    <p>Se o problema persistir ou precisar de mais ajuda, por favor, abra um novo chamado.</p>
                    <p>Atenciosamente,<br><b>Equipe Fatal System</b></p>
                </div>";

            await EnviarEmailGenerico(usuario.Email, $"✔ Resolvido: Chamado #{chamado.IdChamado} - {chamado.Titulo}", corpoEmail);
        }

        private async Task EnviarEmailGenerico(string destinatario, string assunto, string corpo, byte[] anexo = null, string nomeAnexo = null)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                using (MailMessage mail = new MailMessage())
                {
                    smtp.Credentials = new NetworkCredential("fatalsystem.unip@gmail.com", "wwtr xdst wpwm lavr");
                    smtp.EnableSsl = true;

                    mail.From = new MailAddress("fatalsystem.unip@gmail.com", "Fatal System Support");
                    mail.To.Add(destinatario);
                    mail.Subject = assunto;
                    mail.Body = corpo;
                    mail.IsBodyHtml = true;

                    if (anexo != null && anexo.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(anexo))
                        using (Attachment attachment = new Attachment(ms, nomeAnexo ?? "anexo.dat"))
                        {
                            mail.Attachments.Add(attachment);
                            await smtp.SendMailAsync(mail);
                        }
                    }
                    else
                    {
                        await smtp.SendMailAsync(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                // Em produção, logar o erro em arquivo ou banco
                Console.WriteLine($"Erro ao enviar email para {destinatario}: {ex.Message}");
                // Não relançamos o erro para não travar o fluxo principal da aplicação
            }
        }
        public async Task EnviarEmailNovoChamadoTIAsync(Chamado chamado, Usuario usuario, int idChamado, byte[] anexo, string nomeAnexo)
        {
            string emailTecnico = "fatalsystem.unip@gmail.com";
            string indicadorPrioridadeHtml = bolinhadeprioridade(chamado.PrioridadeSugeridaIA);

            // Note que usamos os dados da IA (chamado.ProblemaSugeridoIA, etc.)
            string corpoEmailTI = $@"
                <h2>Novo Chamado #{idChamado} Recebido - Análise Necessária</h2>
                <p>Um novo chamado foi registrado e precisa de análise.</p>
                <p><b>Registrado por:</b> {usuario.Nome} ({usuario.Email})</p>
                <p><b>Número:</b> {idChamado}</p>
                <p><b>Data:</b> {chamado.DataChamado:dd/MM/yyyy HH:mm:ss}</p>
                <hr>
                <p><b>Título:</b> {chamado.Titulo}</p>
                <p><b>Descrição do Usuário:</b> {chamado.Descricao}</p>
                <p><b>Categoria:</b> {chamado.Categoria}</p>
                <p><b>Status Inicial:</b> {chamado.StatusChamado}</p>
                <hr>
                <p><b>Pessoas Afetadas:</b> {chamado.PessoasAfetadas}</p>
                <p><b>Impede o Trabalho:</b> {chamado.ImpedeTrabalho}</p>
                <p><b>Ocorreu Anteriormente:</b> {chamado.OcorreuAnteriormente}</p>
                <hr>
                <h3>Sugestões da Análise Preliminar (IA):</h3>
                <p><b>Identificação do Problema:</b> {chamado.ProblemaSugeridoIA}</p>
                <p><b>Proposta de Solução:</b> {chamado.SolucaoSugeridaIA}</p>
                <p><b>Prioridade Sugerida:</b> {indicadorPrioridadeHtml} {chamado.PrioridadeSugeridaIA}</p>
                <hr>";

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            using (MailMessage mailTI = new MailMessage())
            {
                smtp.Credentials = new NetworkCredential("fatalsystem.unip@gmail.com", "wwtr xdst wpwm lavr");
                smtp.EnableSsl = true;

                mailTI.From = new MailAddress("fatalsystem.unip@gmail.com", "Sistema de Chamados Fatal System");
                mailTI.To.Add(emailTecnico);
                mailTI.Subject = $"[Chamado #{idChamado} - {chamado.PrioridadeSugeridaIA}] Novo Chamado: {chamado.Titulo}";
                mailTI.Body = corpoEmailTI;
                mailTI.IsBodyHtml = true;

                try
                {
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
                catch (Exception ex)
                {
                    Console.WriteLine($"FALHA CRÍTICA: Email para TI (Chamado #{idChamado}) falhou: {ex.Message}");
                   
                }
            }
        }
    }
}