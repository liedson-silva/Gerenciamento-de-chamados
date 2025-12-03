using Gerenciamento_De_Chamados.Models;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados.Services
{
    // A classe EmailService é responsável por toda a comunicação por e-mail do sistema.
    // Ela implementa a interface IEmailService para seguir o padrão de serviço.
    public class EmailService : IEmailService
    {

        /// <summary>
        /// Método auxiliar privado que gera um HTML simples para representar a prioridade (bolinha colorida).
        /// Isso torna os emails mais visuais para o técnico.
        /// </summary>
        /// <param name="prioridade">A string de prioridade ('Alta', 'Média', 'Baixa').</param>
        /// <returns>Uma string HTML (span) com a cor correspondente.</returns>
        private string bolinhadeprioridade(string prioridade)
        {
            // Estilo CSS básico para criar um círculo
            string estiloBase = "display: inline-block; width: 12px; height: 12px; border-radius: 50%; margin-right: 6px; vertical-align: middle;";
            string prioridadeNormalizada = (prioridade ?? "").Trim().ToLower();

            // Atribui a cor com base na prioridade sugerida pela IA
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

        // ====================================================================================
        // MÉTODOS DE ENVIO (Não usados diretamente, mas necessários para a interface IEmailService)
        // O método 'EnviarEmailChamadoAsync' é o que realmente faz a orquestração.
        // ====================================================================================

        public async Task EnviarEmailConfirmacaoUsuarioAsync(Chamado chamado, int idChamado, string emailDestino, string nomeDestino)
        {
            // 1. Validações
            if (string.IsNullOrEmpty(emailDestino) || emailDestino == "sememail@dominio.com")
            {
                Console.WriteLine($"Email do usuário (ID: {chamado?.FK_IdUsuario}) inválido ou não fornecido. Pulando envio de confirmação.");
                return;
            }

            // 2. Construção do corpo do e-mail em formato HTML
            // Repare que agora usamos {idChamado} diretamente
            string corpoEmailUsuario = $@"
        <h2>Olá, {nomeDestino}!</h2>
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

            // 3. Lógica de Envio de Email
            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("fatalsystem.unip@gmail.com", "wwtr xdst wpwm lavr");

                try
                {
                    using (MailMessage mailUsuario = new MailMessage())
                    {
                        mailUsuario.From = new MailAddress("fatalsystem.unip@gmail.com", "Fatal System Support");
                        mailUsuario.To.Add(emailDestino);
                        mailUsuario.Subject = $"Confirmação de Chamado Registrado - {chamado.Titulo}";
                        mailUsuario.Body = corpoEmailUsuario;
                        mailUsuario.IsBodyHtml = true;

                        await smtp.SendMailAsync(mailUsuario);
                    }
                }
                catch (Exception ex)
                {
                    // Tratamento de erro de envio de e-mail de confirmação
                }
            }
        }

        /// <summary>
        /// Envia e-mail ao usuário notificando que seu chamado foi resolvido pelo técnico.
        /// </summary>
        /// <param name="chamado">Dados do chamado resolvido.</param>
        /// <param name="usuario">Dados do usuário que abriu o chamado.</param>
        /// <param name="solucao">O texto da solução final aplicada pelo técnico.</param>
        public async Task EnviarEmailResolucaoUsuarioAsync(Chamado chamado, Usuario usuario, string solucao)
        {
            // Checagem de segurança
            if (usuario == null || string.IsNullOrEmpty(usuario.Email) || usuario.Email == "sememail@dominio.com")
            {
                Console.WriteLine("Email do usuário inválido para envio de resolução.");
                return;
            }

            // Corpo do e-mail de resolução, com design em HTML
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

            // Chama o método genérico para realizar o envio
            await EnviarEmailGenerico(usuario.Email, $"✔ Resolvido: Chamado #{chamado.IdChamado} - {chamado.Titulo}", corpoEmail);
        }

        /// <summary>
        /// Método privado para centralizar a lógica de conexão SMTP, evitando repetição de código.
        /// </summary>
        private async Task EnviarEmailGenerico(string destinatario, string assunto, string corpo, byte[] anexo = null, string nomeAnexo = null)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                using (MailMessage mail = new MailMessage())
                {
                    // Credenciais e SSL (mesmas credenciais de aplicação)
                    smtp.Credentials = new NetworkCredential("fatalsystem.unip@gmail.com", "wwtr xdst wpwm lavr");
                    smtp.EnableSsl = true;

                    mail.From = new MailAddress("fatalsystem.unip@gmail.com", "Fatal System Support");
                    mail.To.Add(destinatario);
                    mail.Subject = assunto;
                    mail.Body = corpo;
                    mail.IsBodyHtml = true;

                    // Lógica para anexar o arquivo, se houver
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
                        // Envia sem anexo
                        await smtp.SendMailAsync(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                // Importante: Tratamento de erro silencioso para que o erro do email não interrompa o app
                Console.WriteLine($"Erro ao enviar email para {destinatario}: {ex.Message}");
            }
        }

        /// <summary>
        /// Envia e-mail para o e-mail de suporte (TI) alertando sobre um novo chamado.
        /// Este e-mail inclui as sugestões de triagem da Inteligência Artificial.
        /// </summary>
        public async Task EnviarEmailNovoChamadoTIAsync(Chamado chamado, Usuario usuario, int idChamado, byte[] anexo, string nomeAnexo)
        {
            // O email de destino é fixo no TI/Suporte
            string emailTecnico = "fatalsystem.unip@gmail.com";

            // Usa o método auxiliar para adicionar a 'bolinha' de cor da prioridade
            string indicadorPrioridadeHtml = bolinhadeprioridade(chamado.PrioridadeSugeridaIA);

            // Corpo do e-mail TI, destacando a análise da IA
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

            // Configuração e envio (semelhante ao genérico, mas específico para a equipe técnica)
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
                        // Envia com anexo, lendo os bytes da memória
                        using (MemoryStream msTI = new MemoryStream(anexo))
                        using (Attachment attachmentTI = new Attachment(msTI, nomeAnexo))
                        {
                            mailTI.Attachments.Add(attachmentTI);
                            await smtp.SendMailAsync(mailTI);
                        }
                    }
                    else
                    {
                        // Envia sem anexo
                        await smtp.SendMailAsync(mailTI);
                    }
                }
                catch (Exception ex)
                {
                    // Tratamento de erro para o e-mail do TI
                    Console.WriteLine($"FALHA CRÍTICA: Email para TI (Chamado #{idChamado}) falhou: {ex.Message}");

                }
            }
        }

        /// <summary>
        /// Método de orquestração que envia os dois emails (Usuário e TI)
        /// Este método é o ponto de chamada real da aplicação.
        /// </summary>
        public async Task EnviarEmailChamadoAsync(
            string titulo, string descricao, string categoria, int idChamado,
            string prioridadeIA, string status, string pessoasAfetadas,
            string impedeTrabalho, string ocorreuAnteriormente, string problemaIA, string solucaoIA,
            DateTime dataAbertura, byte[] anexo, string nomeAnexo
        )
        {

            try
            {
                // 1. Configurar dados do usuário logado (Quem abriu o chamado)
                Usuario usuarioLogado = new Usuario
                {
                    IdUsuario = SessaoUsuario.IdUsuario,
                    Nome = SessaoUsuario.Nome,
                    Email = SessaoUsuario.Email
                };

                // 2. Criar objeto Chamado com as sugestões da IA
                Chamado chamado = new Chamado
                {
                    IdChamado = idChamado,
                    Titulo = titulo,
                    Descricao = descricao,
                    Categoria = categoria,
                    DataChamado = dataAbertura,
                    StatusChamado = status,
                    PessoasAfetadas = pessoasAfetadas,
                    ImpedeTrabalho = impedeTrabalho,
                    OcorreuAnteriormente = ocorreuAnteriormente,
                    PrioridadeSugeridaIA = prioridadeIA,
                    ProblemaSugeridoIA = problemaIA,
                    SolucaoSugeridaIA = solucaoIA
                };

                // Envia e-mail de confirmação para o usuário que abriu
                await EnviarEmailConfirmacaoUsuarioAsync(chamado, idChamado, usuarioLogado.Email, usuarioLogado.Nome);

                // Envia e-mail de alerta para o TI (incluindo as sugestões da IA e o anexo)
                await EnviarEmailNovoChamadoTIAsync(chamado, usuarioLogado, idChamado, anexo, nomeAnexo);

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