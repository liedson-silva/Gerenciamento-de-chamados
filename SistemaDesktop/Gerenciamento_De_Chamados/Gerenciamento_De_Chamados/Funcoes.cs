using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados
{
    public class Funcoes
    {
        // 🔒 Criptografia SHA256
        public static string Criptografar(string Texto)
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(Texto));
                StringBuilder retorno = new StringBuilder();
                foreach (byte b in bytes)
                {
                    retorno.Append(b.ToString("x2"));
                }
                return retorno.ToString();
            }
        }

        // 📂 Selecionar arquivo e converter em byte[]
        public byte[] SelecionarArquivoEConverter()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image imagem = Image.FromFile(ofd.FileName);
                return ImageToByteArray(imagem);
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
            public static bool UsuarioIdentificado()
            {
                return IdUsuario > 0 && !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Nome);
            }
        }
        // Método para validar se o usuário está identificado

        // 🔎 Buscar nome do usuário
        public static string ObterNomeDoUsuario(string login, SqlConnection conexao)
        {
            string sql = "SELECT Nome FROM Usuario WHERE Login = @login";
            using (SqlCommand cmd = new SqlCommand(sql, conexao))
            {
                cmd.Parameters.Add("@login", System.Data.SqlDbType.NVarChar).Value = login;
                object result = cmd.ExecuteScalar();
                if (result == null)
                {
                    throw new Exception("Usuário não encontrado no banco.");
                }
                return result.ToString();
            }
        }

        // 🔎 Buscar ID do usuário
        public static int ObterIdDoUsuario(string login, SqlConnection conexao)
        {
            string sql = "SELECT IdUsuario FROM Usuario WHERE Login = @login";
            using (SqlCommand cmd = new SqlCommand(sql, conexao))
            {
                cmd.Parameters.Add("@login", System.Data.SqlDbType.NVarChar).Value = login;
                object result = cmd.ExecuteScalar();
                if (result == null)
                {
                    throw new Exception("Usuário não encontrado no banco.");
                }
                return Convert.ToInt32(result);
            }
        }
    }
}