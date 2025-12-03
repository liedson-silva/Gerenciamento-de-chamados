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
    /// <summary>
    /// **FORMULÁRIO: Cadastro de Novos Usuários**
    /// Usado por Administradores para registrar novos funcionários/técnicos no sistema.
    /// A validação dos campos é feita localmente e, em seguida, na camada de Serviço.
    /// </summary>
    public partial class Cadastro_de_Usuarios : Form
    {
        // Dependência para a camada de serviço, que contém a lógica de negócio e persistência.
        private readonly IUsuarioService _usuarioService;

        public Cadastro_de_Usuarios()
        {
            InitializeComponent();
            this.Load += Cadastro_de_Usuarios_Load;
            // Define a data inicial como a data atual
            dtpCadDN.Value = DateTime.Now;

            // **INJEÇÃO DE DEPENDÊNCIA MANUAL:**
            // Cria a instância do Repositório e injeta no Serviço.
            IUsuarioRepository usuarioRepository = new UsuarioRepository();
            _usuarioService = new UsuarioService(usuarioRepository);
        }

        /// <summary>
        /// Exibe o nome e a função do usuário logado ao carregar o formulário.
        /// </summary>
        private void Cadastro_de_Usuarios_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessaoUsuario.Nome))
                lbl_NomeUser.Text = ($"Olá, {SessaoUsuario.Nome} ({SessaoUsuario.FuncaoUsuario})");
            else
                lbl_NomeUser.Text = "Usuário não identificado";
        }

        /// <summary>
        /// **EVENTO: Clique no botão de Cadastro (Adicionar)**
        /// Coleta os dados, executa a validação do formulário e chama o serviço para adicionar o novo usuário.
        /// </summary>
        private async void btnCadastroAdd_Click(object sender, EventArgs e)
        {
            // 1. Coleta e Limpeza dos Dados
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

            // 2. Validações de Formulário (Quick Checks)
            // Estas são validações de front-end para dar feedback imediato ao usuário.

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

            // Validação de Login (verifica se o campo está preenchido e não é o texto de placeholder)
            if (!ValidadorUsuario.IsNomeValido(login) || login == "Digite seu usuário, apenas letras ou números.")
            {
                MessageBox.Show("O campo 'Login' é obrigatório e não pode ser o texto padrão.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidadorUsuario.IsSenhaForte(senhaDigitada))
            {
                MessageBox.Show("A 'Senha' é inválida. Requisitos: \n- Mínimo 8 caracteres\n- Pelo menos 1 letra maiúscula\n- Pelo menos 1 número", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validações simples para ComboBoxes (devem estar selecionados)
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

            // 3. Cria o Objeto Model
            Usuario novoUsuario = new Usuario
            {
                Nome = nome,
                CPF = cpf,
                RG = rg,
                FuncaoUsuario = funcao,
                Sexo = sexo,
                Setor = setor,
                DataDeNascimento = dataNascimento,
                Email = email,
                Login = login,
                
                Senha = senhaDigitada
            };


            // 4. Chama o Serviço para salvar e trata a resposta
            try
            {
                // O Service faz a validação de regra de negócio (ex: Login já existe) 
               
                await _usuarioService.AdicionarUsuarioAsync(novoUsuario);

                MessageBox.Show("✅ Usuário cadastrado com sucesso!");
                this.Close(); 
            }
            catch (Exception ex)
            {
                // Captura exceções lançadas pelo Serviço ou Repositório (ex: Login já existe)
                MessageBox.Show("❌ Erro ao cadastrar: " + ex.Message, "Erro de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Código de Estética e Navegação 

        // Método de pintura para aplicar o gradiente ao painel de navegação
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

        // Navegação para a tela Home (Admin, Técnico ou Funcionário)
        private void lbl_Inicio_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        // Navegação para a tela Home (via imagem/logo)
        private void PctBox_Inicio_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        // Ação de Sair (Logout)
        private void lbSair_Click(object sender, EventArgs e)
        {
            FormHelper.Sair(this);
        }

        // Garante que apenas dígitos possam ser digitados no campo CPF
        private void txtCadastroCpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Ignora a tecla (não deixa ela aparecer no textbox)
            }
        }

        // Garante que apenas dígitos possam ser digitados no campo RG
        private void txtCadastroRG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Método de pintura para aplicar o gradiente ao fundo do formulário
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

        // Navegação para a tela de FAQ
        private void label3_Click(object sender, EventArgs e)
        {
            FormHelper.FAQ(this);
        }

        // Navegação para a tela de Visualizar/Editar Minha Conta
        private void lblMconta_Click(object sender, EventArgs e)
        {
            // Abre a tela de visualização do próprio usuário logado
            var visualizarUsuario = new Visualizar_Usuario(SessaoUsuario.IdUsuario);
            visualizarUsuario.Show();
            this.Hide();
        }

        // Navegação para a tela de Meus Chamados (VisualizarChamado)
        private void label1_Click(object sender, EventArgs e)
        {
            var verChamado = new VisualizarChamado();
            this.Hide();
            verChamado.ShowDialog();
            this.Show();
        }

        // Botão Cancelar/Fechar
        private void btnCadastroCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}