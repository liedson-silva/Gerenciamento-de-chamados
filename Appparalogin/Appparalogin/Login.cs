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
            try
            {
                if (txtUsuario.Text == "admin" && txtSenha.Text == "123")
                {
                    var menu = new MenuRestrito();

                    menu.Show();


                    this.Visible = false;
                    //this.Dispose();
                    

                }
                else
                {
                    MessageBox.Show("Usuário ou senha inválidos",
                                    "Atenção!!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    txtUsuario.Focus();
                    txtSenha.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message,
                                "Atenção!!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

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
