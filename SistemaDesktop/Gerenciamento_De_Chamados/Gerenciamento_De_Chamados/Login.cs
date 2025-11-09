using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados
{
    public partial class Login : Form
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public Login()
        {
            InitializeComponent();
            _usuarioRepository = new UsuarioRepository();
            this.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtSenha.Text))
            {
                MessageBox.Show("Por favor, preencha o login e a senha.", "Campos Vazios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string loginLimpo = txtUsuario.Text.Trim();
                string senhaLimpa = txtSenha.Text.Trim();

                // Chama o método AutenticarAsync do repositório
                Usuario usuarioAutenticado = await _usuarioRepository.AutenticarAsync(loginLimpo, senhaLimpa);

                if (usuarioAutenticado != null)
                {
                    // Preenche a SessaoUsuario com os dados retornados pelo repositório
                    SessaoUsuario.IdUsuario = usuarioAutenticado.IdUsuario;
                    SessaoUsuario.Nome = usuarioAutenticado.Nome;
                    SessaoUsuario.Login = usuarioAutenticado.Login;
                    SessaoUsuario.FuncaoUsuario = usuarioAutenticado.FuncaoUsuario;
                    SessaoUsuario.Email = usuarioAutenticado.Email;
                    // Preencha outros dados da sessão se necessário

                    MessageBox.Show("Login bem-sucedido!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Lógica para abrir o Form correto baseado na Função
                    switch (usuarioAutenticado.FuncaoUsuario.ToLower())
                    {
                        case "admin":
                            HomeAdmin homeAdmin = new HomeAdmin();
                            homeAdmin.Show();
                            break;
                        case "tecnico": // Ou como estiver no seu banco
                            HomeTecnico homeTecnico = new HomeTecnico();
                            homeTecnico.Show();
                            break;
                        default: // Assume "funcionario" ou outros
                            HomeFuncionario homeFuncionario = new HomeFuncionario();
                            homeFuncionario.Show();
                            break;
                    }
                    this.Hide(); // Esconde o formulário de login
                }
                else
                {
                    MessageBox.Show("Login ou senha inválidos.", "Falha na Autenticação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro durante a autenticação: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Estética e comportamento dos campos

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
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                MessageBox.Show("Digite apenas letras ou números.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Login_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color corInicio = Color.White;
            Color corFim = ColorTranslator.FromHtml("#232325");

            using (LinearGradientBrush gradiente = new LinearGradientBrush(
                this.ClientRectangle, corInicio, corFim, LinearGradientMode.Horizontal))
            {
                g.FillRectangle(gradiente, this.ClientRectangle);
            }

            #endregion
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Entre em contato com o administrador .", "Ajuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
