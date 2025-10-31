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
    public partial class Cadastro_de_Usuarios : Form
    {
       
        private readonly IUsuarioRepository _usuarioRepository;

        public Cadastro_de_Usuarios()
        {
            InitializeComponent();
            this.Load += Cadastro_de_Usuarios_Load;

            
            _usuarioRepository = new UsuarioRepository();
        }

        
        private async void btnCadastroAdd_Click(object sender, EventArgs e)
        {
            // Coleta e validação simples
            string login = txtCadastroLogin.Text.Trim();
            string senhaDigitada = txtCadastroSenha.Text.Trim();

            if (string.IsNullOrWhiteSpace(login) ||
                login == "Digite seu usuário, apenas letras ou números.")
            {
                MessageBox.Show("⚠️ Login inválido. Digite um login válido.");
                return;
            }

            if (string.IsNullOrWhiteSpace(senhaDigitada))
            {
                MessageBox.Show("⚠️ Senha é obrigatória.");
                return;
            }

            // 2. Cria o Objeto Model
            Usuario novoUsuario = new Usuario
            {
                Nome = txtCadastroNome.Text.Trim(),
                CPF = txtCadastroCpf.Text.Trim(),
                RG = txtCadastroRG.Text.Trim(),
                FuncaoUsuario = cbxCadastroFuncao.Text,
                Sexo = comboBoxCadastroSexo.Text,
                Setor = cBoxCadSetor.Text,
                DataDeNascimento = dtpCadDN.Value,
                Email = txtCadastroEmail.Text.Trim(),
                Login = login,
                Senha = senhaDigitada // Passa a senha em texto puro. O REPOSITÓRIO irá criptografar.
            };

            // Chama o Repositório e trata a resposta
            try
            {
                
                await _usuarioRepository.AdicionarAsync(novoUsuario);

                MessageBox.Show("✅ Usuário cadastrado com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("❌ Erro ao cadastrar: " + ex.Message);
            }
        }

        #region Código de Estética e Navegação 

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
        private void Cadastro_de_Usuarios_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessaoUsuario.Nome))
                lbl_NomeUser.Text = ($"Bem vindo {SessaoUsuario.Nome}");
            else
                lbl_NomeUser.Text = "Usuário não identificado";
        }

        private void lbl_Inicio_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void PctBox_Inicio_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void lbSair_Click(object sender, EventArgs e)
        {
            FormHelper.Sair(this);
        }
        #endregion
    }
}