using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Gerenciamento_De_Chamados
{
    public class Funcoes
    {
        // 🔒 Criptografia SHA256
        public static string Criptografar(string texto)
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder retorno = new StringBuilder();

                foreach (byte b in bytes)
                    retorno.Append(b.ToString("x2"));

                return retorno.ToString();
            }
        }

        // 📂 Selecionar arquivo e converter em byte[]
        public byte[] SelecionarArquivoEConverter()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (Image imagem = Image.FromFile(ofd.FileName))
                    {
                        return ImageToByteArray(imagem);
                    }
                }
            }
            return null;
        }

        public byte[] ImageToByteArray(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }

        public Image ByteArrayToImage(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        // 🧑‍💻 Sessão do usuário logado
        public static class SessaoUsuario
        {
            public static int IdUsuario { get; set; }
            public static string Login { get; set; }
            public static string Nome { get; set; }
            public static string Email { get; set; }
            public static string FuncaoUsuario { get; set; }

            public static bool UsuarioIdentificado()
            {
                return !string.IsNullOrEmpty(Nome) && IdUsuario > 0 && !string.IsNullOrEmpty(FuncaoUsuario);
            }
        }

        public static string ObterFuncaoDoUsuario(string login, SqlConnection conexao)
        {
            string sql = "SELECT FuncaoUsuario FROM Usuario WHERE Login = @login";

            using (SqlCommand cmd = new SqlCommand(sql, conexao))
            {
                cmd.Parameters.AddWithValue("@login", login);
                object result = cmd.ExecuteScalar();

                return (result != null && result != DBNull.Value) ? result.ToString() : string.Empty;
            }
        }
        // 🔎 Buscar nome do usuário
        public static string ObterNomeDoUsuario(string login, SqlConnection conexao)
        {
            string sql = "SELECT Nome FROM Usuario WHERE Login = @login";

            using (SqlCommand cmd = new SqlCommand(sql, conexao))
            {
                cmd.Parameters.AddWithValue("@login", login);
                object result = cmd.ExecuteScalar();

                return (result != null && result != DBNull.Value) ? result.ToString() : string.Empty;
            }
        }

        public static string ObterEmailDoUsuario(string login, SqlConnection conexao)
        {
            string sql = "SELECT Email FROM Usuario WHERE Login = @login";

            using (SqlCommand cmd = new SqlCommand(sql, conexao))
            {
                cmd.Parameters.AddWithValue("@login", login);
                // Executa a consulta SQL e retorna o primeiro valor da primeira linha do resultado (usado para buscar um único campo)
                object result = cmd.ExecuteScalar();

                return (result != null && result != DBNull.Value) ? result.ToString() : string.Empty;
            }
        }

        // 🔎 Buscar ID do usuário
        public static int ObterIdDoUsuario(string login, SqlConnection conexao)
        {
            string sql = "SELECT IdUsuario FROM Usuario WHERE Login = @login";

            using (SqlCommand cmd = new SqlCommand(sql, conexao))
            {
                cmd.Parameters.AddWithValue("@login", login);
                object result = cmd.ExecuteScalar();

                return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
            }
        }

        //  Exibir usuário logado em qualquer tela
        public static void ExibirUsuarioEmLabel(Label labelDestino)
        {
            labelDestino.Text = !string.IsNullOrEmpty(SessaoUsuario.Nome)
                ? SessaoUsuario.Nome
                : "Usuário não identificado";
        }

        //  Botão para voltar à Home admin
        public static void BotaoHomeAdmin(Form formAtual)
        {
            var homeAdmin = new HomeAdmin();
            homeAdmin.Show();
            formAtual.Hide();
        }

        // Botão para voltar à Home do Funcionario
        public static void BotaoHomeFuncionario(Form formAtual)
        {
            var homeFuncionario = new HomeFuncionario();
            homeFuncionario.Show();
            formAtual.Hide();
        }

        //  Botão para voltar à Home do Tecnico
        public static void BotaoHomeTecnico(Form formAtual)
        {
            var homeTecnico = new HomeTecnico();
            homeTecnico.Show();
            formAtual.Hide();
        }

        //  Enviar e-mail ao abrir chamado
        public static void EnviarEmailChamado(
            string titulo, string descricao, string categoria, int idChamado,
            string prioridade, string status, string pessoasAfetadas,
            string impedeTrabalho, string ocorreuAnteriormente,
            byte[] anexo, string nomeAnexo
        )
        {
            try
            {
                string usuario = SessaoUsuario.Nome ?? "Usuário não identificado";
                string emailUsuario = SessaoUsuario.Email ?? "sememail@dominio.com";
                string corpoEmail = $@"
            <h2>Prezado(a): {usuario}</h2>
            <p><b>Número:</b> {idChamado}</p>
            <p><b>Data:</b> {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p>
            <hr>
            <p><b>Título:</b> {titulo}</p>
            <p><b>Descrição:</b> {descricao}</p>
            <p><b>Categoria:</b> {categoria}</p>
            <p><b>Prioridade:</b> {prioridade}</p>
            <p><b>Status:</b> {status}</p>
            <hr>
            <p><b>Pessoas Afetadas:</b> {pessoasAfetadas}</p>
            <p><b>Impede o Trabalho:</b> {impedeTrabalho}</p>
            <p><b>Ocorreu Anteriormente:</b> {ocorreuAnteriormente}</p>
            <hr>
            
        ";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("fatalsystem.unip@gmail.com", "Sistema de Chamados Fatal System");
                    mail.To.Add("fatalsystem.unip@gmail.com"); // Destinatário principal
                    mail.CC.Add(emailUsuario); // Usuário que abriu o chamado recebe em cópia

                    mail.Subject = $"Novo Chamado #{idChamado} - {titulo}";
                    mail.Body = corpoEmail;
                    mail.IsBodyHtml = true;

                   
                    if (anexo != null && anexo.Length > 0)
                    {
                        MemoryStream ms = new MemoryStream(anexo);
                        
                            // Cria o anexo a partir do stream de memória
                            Attachment attachment = new Attachment(ms, nomeAnexo);
                            mail.Attachments.Add(attachment); // Adiciona o anexo ao e-mail
                        
                    }
                    

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("fatalsystem.unip@gmail.com", "wwtr xdst wpwm lavr");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                
                string mensagemErro = "Erro ao enviar e-mail: " + ex.Message;
                if (ex.InnerException != null)
                {
                    mensagemErro += "\n\nDetalhes: " + ex.InnerException.Message;
                }

                MessageBox.Show(mensagemErro, "Falha no Envio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}