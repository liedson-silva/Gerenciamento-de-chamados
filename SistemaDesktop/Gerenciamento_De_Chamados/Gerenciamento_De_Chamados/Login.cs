using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string senhaDigitada = txtSenha.Text.Trim();

            // Usuário admin padrão
            const string adminLogin = "admin";
            const string adminSenha = "admin1234";

            if (usuario == adminLogin && senhaDigitada == adminSenha)
            {
                MessageBox.Show("✅ Login de administrador realizado com sucesso!");
                var home = new Home();
                home.Show();
                this.Hide();
                return;
            }

            string connectionString = "Server=fatalsystemsrv1.database.windows.net;Database=DbaFatal-System;User Id=fatalsystem;Password=F1234567890m@;";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();


                    string sql = "SELECT Senha FROM Usuario WHERE Login = @usuario";
                    using (SqlCommand cmd = new SqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);

                        object resultado = cmd.ExecuteScalar();

                        if (resultado == null)
                        {
                            MessageBox.Show($"❌ Usuário '{usuario}' não encontrado no banco.");
                            return;
                        }

                        string hashSalvo = resultado.ToString();

                        // Valida a senha usando o hash
                        if (SenhaHelper.ValidarSenha(senhaDigitada, hashSalvo))
                        {
                            try
                            {
                                // Preenche sessão com dados do usuário
                                Funcoes.SessaoUsuario.Login = usuario; 
                                Funcoes.SessaoUsuario.Nome = Funcoes.ObterNomeDoUsuario(usuario, conexao);
                                Funcoes.SessaoUsuario.IdUsuario = Funcoes.ObterIdDoUsuario(usuario, conexao);

                                if (string.IsNullOrEmpty(Funcoes.SessaoUsuario.Nome) || Funcoes.SessaoUsuario.IdUsuario == 0)
                                {
                                    MessageBox.Show("Usuário não identificado corretamente. Verifique os dados do usuário no banco.");
                                    return;
                                }

                                MessageBox.Show("✅ Login realizado com sucesso!\nBem-vindo, " + Funcoes.SessaoUsuario.Nome);

                                // Abre a tela Home já com o usuário logado
                                var home = new Home();
                                home.Show();
                                this.Hide();
                            }
                            catch (Exception exSessao)
                            {
                                MessageBox.Show("Erro ao carregar dados do usuário: " + exSessao.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("❌ Senha incorreta!");
                            txtSenha.Focus();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro de conexão: " + ex.Message);
                }
            }
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            txtUsuario.BackColor = Color.LightYellow;
            if (txtUsuario.Text == "Digite seu usuário, apenas letras ou números.")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.Black;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            txtUsuario.BackColor = Color.White;
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "Digite seu usuário, apenas letras ou números.";
                txtUsuario.ForeColor = SystemColors.InactiveCaption;
            }
        }

        private void txtSenha_Enter(object sender, EventArgs e)
        {
            txtSenha.BackColor = Color.LightYellow;
            if (txtSenha.Text == "Senha")
            {
                txtSenha.Text = "";
                txtSenha.ForeColor = Color.Black;
                txtSenha.UseSystemPasswordChar = true;
            }
        }

        private void txtSenha_Leave(object sender, EventArgs e)
        {
            txtSenha.BackColor = Color.White;
            if (txtSenha.Text == "")
            {
                txtSenha.Text = "Senha";
                txtSenha.ForeColor = SystemColors.InactiveCaption;
                txtSenha.UseSystemPasswordChar = false;
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            int tecla = (int)e.KeyChar;

            if (!char.IsLetterOrDigit(e.KeyChar) && tecla != 08)
            {
                e.Handled = true;
                MessageBox.Show("Digite apenas letras ou números",
                                    "Ops",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                txtUsuario.SelectionStart = 0;
                txtUsuario.SelectionLength = txtUsuario.Text.Length;

                txtUsuario.Focus();
            }
        }

        private void Login_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Color corInicio = Color.White;
            Color corFim = ColorTranslator.FromHtml("#232325");

            LinearGradientBrush gradiente = new LinearGradientBrush(
                this.ClientRectangle,
                corInicio,
                corFim,
                LinearGradientMode.Horizontal);

            g.FillRectangle(gradiente, this.ClientRectangle);
        }
    }
}