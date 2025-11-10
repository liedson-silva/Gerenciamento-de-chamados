using Gerenciamento_De_Chamados.Helpers;
using Gerenciamento_De_Chamados.Models; 
using Gerenciamento_De_Chamados.Repositories; 
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks; 
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados
{
    public partial class Editar_Usuario : Form
    {
        private readonly int _usuarioId;
        private readonly IUsuarioRepository _usuarioRepository;


        public Editar_Usuario(int idUsuario)
        {
            InitializeComponent();
            this._usuarioId = idUsuario;

            _usuarioRepository = new UsuarioRepository();


            this.Load += Editar_Usuario_Load;
            this.btnSalvar.Click += btnSalvar_Click;
            this.btnCancelar.Click += btnCancelar_Click;


            this.btnAlterarSenha.Click += btnAlterarSenha_Click;
        }

        private async void Editar_Usuario_Load(object sender, EventArgs e)
        {

            await CarregarDadosDoUsuarioAsync();
        }

        private async Task CarregarDadosDoUsuarioAsync()
        {
            try
            {
                // Busca o usuário pelo ID usando o repositório
                Usuario usuario = await _usuarioRepository.BuscarPorIdAsync(this._usuarioId);

                if (usuario != null)
                {

                    txtNome.Text = usuario.Nome;
                    txtCPF.Text = usuario.CPF;
                    txtRG.Text = usuario.RG;
                    cmbFuncao.SelectedItem = usuario.FuncaoUsuario;
                    cmbSexo.SelectedItem = usuario.Sexo;
                    txtSetor.Text = usuario.Setor;
                    txtEmail.Text = usuario.Email;
                    txtLogin.Text = usuario.Login;


                    txtSenha.Text = "";

                    if (usuario.DataDeNascimento > dtpDataNascimento.MinDate)
                    {
                        dtpDataNascimento.Value = usuario.DataDeNascimento;
                    }
                    else
                    {
                        dtpDataNascimento.Value = DateTime.Today; // Ou qualquer valor padrão
                    }
                }
                else
                {
                    MessageBox.Show("Usuário não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados do usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }


        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Os campos Nome e Email são obrigatórios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                Usuario usuario = new Usuario
                {
                    IdUsuario = this._usuarioId,
                    Nome = txtNome.Text,
                    CPF = txtCPF.Text,
                    RG = txtRG.Text,
                    Setor = txtSetor.Text,
                    DataDeNascimento = dtpDataNascimento.Value,
                    Email = txtEmail.Text,
                    Login = txtLogin.Text,
                    FuncaoUsuario = cmbFuncao.SelectedItem?.ToString(),
                    Sexo = cmbSexo.SelectedItem?.ToString()

                };

                // Chama o método AtualizarAsync do repositório
                await _usuarioRepository.AtualizarAsync(usuario);

                MessageBox.Show("Usuário atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Indica que salvou
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar alterações: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- NOVO MÉTODO PARA O BOTÃO btnAlterarSenha ---
        private async void btnAlterarSenha_Click(object sender, EventArgs e)
        {
            string novaSenha = txtSenha.Text;

            // 1. Validação da Senha
            if (string.IsNullOrWhiteSpace(novaSenha) || novaSenha.Length < 0)
            {
                MessageBox.Show("Por favor, digite uma nova senha de no mínimo 4 caracteres.", "Senha Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Focus();
                return;
            }

            // 2. Confirmação
            var confirmResult = MessageBox.Show(
                $"Deseja realmente alterar a senha do usuário '{txtNome.Text}'?",
                "Confirmar Alteração de Senha",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // 3. Chama o Repositório
                    await _usuarioRepository.AlterarSenhaAsync(this._usuarioId, novaSenha);

                    MessageBox.Show("Senha alterada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSenha.Text = ""; // Limpa a caixa de senha
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao alterar a senha: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #region Código de Estética (Sem Alterações)
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color corInicioPanel = Color.White;
            Color corFimPanel = ColorTranslator.FromHtml("#232325");
            LinearGradientBrush gradientePanel = new LinearGradientBrush(
                         panel1.ClientRectangle,
                         corInicioPanel,
                         corFimPanel,
                         LinearGradientMode.Vertical);
            g.FillRectangle(gradientePanel, panel1.ClientRectangle);
        }

        private void lbl_Inicio_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void PctBox_Logo_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void lbSair_Click(object sender, EventArgs e)
        {
            FormHelper.Sair(this);
        }
        #endregion

        private void label3_Click(object sender, EventArgs e)
        {
            FormHelper.FAQ(this);
        }

        private void Editar_Usuario_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color corInicio = Color.White;
            Color corFim = ColorTranslator.FromHtml("#232325");

            using (LinearGradientBrush gradiente = new LinearGradientBrush(
                this.ClientRectangle, corInicio, corFim, LinearGradientMode.Horizontal))
            {
                g.FillRectangle(gradiente, this.ClientRectangle);
            }
        }
    }
}