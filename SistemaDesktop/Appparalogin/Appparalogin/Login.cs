using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Appparalogin
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string senhaDigitada = txtSenha.Text.Trim();

            //Usuario admin padrão

            const string adminLogin = "admin";
            const string adminSenha = "admin1234";

            if (usuario == adminLogin && senhaDigitada == adminSenha)
            {
                MessageBox.Show("✅ Login de administrador realizado com sucesso!");
                // Abrir menu restrito
                var menu = new MenuRestrito();
                menu.Show();
                this.Hide(); // Oculta o form de login
                return;
            }

            string connectionString = "Server=fatalsystemsrv1.database.windows.net;Database=DbaFatal-System;User Id=fatalsystem;Password=F1234567890m@;";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();

                    // Busca o hash da senha pelo login
                    string sql = "SELECT Senha FROM Usuario WHERE Login = @usuario";
                    using (SqlCommand cmd = new SqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);

                        object resultado = cmd.ExecuteScalar();

                        if (resultado != null)
                        {
                            string hashSalvo = resultado.ToString();

                            // Valida a senha usando o hash
                            if (SenhaHelper.ValidarSenha(senhaDigitada, hashSalvo))
                            {
                                MessageBox.Show("✅ Login realizado com sucesso!");

                                // Abrir menu restrito
                                var menu = new MenuRestrito();
                                menu.Show();
                                this.Hide(); // Oculta o form de login
                            }
                            else
                            {
                                MessageBox.Show("❌ Senha incorreta!");
                                txtSenha.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("❌ Usuário não encontrado!");
                            txtUsuario.Focus();
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
    }
}
