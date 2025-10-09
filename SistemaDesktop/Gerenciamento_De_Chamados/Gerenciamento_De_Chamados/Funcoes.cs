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

            public static bool UsuarioIdentificado()
            {
                return !string.IsNullOrEmpty(Nome) && IdUsuario > 0;
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

        // 🧩 Exibir usuário logado em qualquer tela
        public static void ExibirUsuarioEmLabel(Label labelDestino)
        {
            labelDestino.Text = !string.IsNullOrEmpty(SessaoUsuario.Nome)
                ? SessaoUsuario.Nome
                : "Usuário não identificado";
        }

        // 🏠 Botão para voltar à Home
        public static void BotaoHome(Form formAtual)
        {
            var home = new Home();
            home.Show();
            formAtual.Hide();
        }

        // 📧 Enviar e-mail ao abrir chamado
        public static void EnviarEmailChamado(string titulo, string descricao, string categoria, int idChamado)
        {
            try
            {
                string usuario = SessaoUsuario.Nome ?? "Usuário não identificado";
                string emailUsuario = SessaoUsuario.Email ?? "sememail@dominio.com";

                string corpoEmail = $@"
                    <h2>Novo Chamado Criado</h2>
                    <p><b>Número:</b> {idChamado}</p>
                    <p><b>Usuário:</b> {usuario}</p>
                    <p><b>Título:</b> {titulo}</p>
                    <p><b>Descrição:</b> {descricao}</p>
                    <p><b>Categoria:</b> {categoria}</p>
                    <p><i>Data:</i> {DateTime.Now}</p>
                ";

                using (MailMessage mail = new MailMessage())
                {
                    // Remetente fixo (sua conta SMTP real)
                    mail.From = new MailAddress("fatalsystem.unip@gmail.com");

                    // Destinatário principal (suporte)
                    mail.To.Add("fatalsystem.unip@gmail.com");

                    // O usuário logado também recebe cópia
                    mail.CC.Add(emailUsuario);

                    mail.Subject = $"Novo Chamado #{idChamado} - {titulo}";
                    mail.Body = corpoEmail;
                    mail.IsBodyHtml = true;

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
                MessageBox.Show("Erro ao enviar e-mail: " + ex.Message);
            }
        }
    }
}