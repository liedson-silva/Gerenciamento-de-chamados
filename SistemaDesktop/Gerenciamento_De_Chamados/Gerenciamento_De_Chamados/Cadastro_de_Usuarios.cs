using Gerenciamento_De_Chamados.Helpers;
using Gerenciamento_De_Chamados.Models; 
using Gerenciamento_De_Chamados.Repositories;
using Gerenciamento_De_Chamados.Services;
using Gerenciamento_De_Chamados.Validacao;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks; 
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados
{
    public partial class Cadastro_de_Usuarios : Form
    {


        private readonly IUsuarioService _usuarioService;

        public Cadastro_de_Usuarios()
        {
            InitializeComponent();
            this.Load += Cadastro_de_Usuarios_Load;
            dtpCadDN.Value = DateTime.Now;


            IUsuarioRepository usuarioRepository = new UsuarioRepository();
            // Instancia o serviço REAL, injetando o repositório
            _usuarioService = new UsuarioService(usuarioRepository);
        }


        private async void btnCadastroAdd_Click(object sender, EventArgs e)
        {
            // Coleta e validação simples
            string login = txtCadastroLogin.Text.Trim();
            string senhaDigitada = txtCadastroSenha.Text.Trim();
            string nome = txtCadastroNome.Text.Trim();
            string cpf = txtCadastroCpf.Text.Trim();
            string rg = txtCadastroRG.Text.Trim();
            string funcao = cbxCadastroFuncao.Text;
            string sexo = comboBoxCadastroSexo.Text;
            string setor = cBoxCadSetor.Text;
            DateTime dataNascimento = dtpCadDN.Value;
            string email = txtCadastroEmail.Text.Trim();

            if (!ValidadorUsuario.IsNomeValido(nome))
            {
                MessageBox.Show("O campo 'Nome' é obrigatório.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidadorUsuario.IsEmailValido(email))
            {
                MessageBox.Show("O formato do 'Email' é inválido.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidadorUsuario.IsCPFValido(cpf))
            {
                MessageBox.Show("O formato do 'CPF' é inválido. Deve conter 11 dígitos.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validação de Login (pode usar a mesma regra do nome)
            if (!ValidadorUsuario.IsNomeValido(login) || login == "Digite seu usuário, apenas letras ou números.")
            {
                MessageBox.Show("O campo 'Login' é obrigatório.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidadorUsuario.IsSenhaForte(senhaDigitada))
            {
                MessageBox.Show("A 'Senha' é inválida. Requisitos: \n- Mínimo 8 caracteres\n- Pelo menos 1 letra maiúscula\n- Pelo menos 1 número", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validações simples para ComboBoxes
            if (string.IsNullOrWhiteSpace(funcao))
            {
                MessageBox.Show("Selecione uma 'Função'.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(setor))
            {
                MessageBox.Show("Selecione um 'Setor'.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                Senha = senhaDigitada
            };


            // Chama o Repositório e trata a resposta
            try
            {
                // Esta única linha agora faz a validação E o salvamento
                await _usuarioService.AdicionarUsuarioAsync(novoUsuario);

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
                lbl_NomeUser.Text = ($" {SessaoUsuario.Nome}");
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

        private void txtCadastroCpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Ignora a tecla (não deixa ela aparecer no textbox)
            }
        }

        private void txtCadastroRG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Cadastro_de_Usuarios_Paint(object sender, PaintEventArgs e)
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

        private void label3_Click(object sender, EventArgs e)
        {
            FormHelper.FAQ(this);
        }
    }
}