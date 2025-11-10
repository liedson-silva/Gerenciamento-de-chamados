using Gerenciamento_De_Chamados.Repositories;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Helpers;

namespace Gerenciamento_De_Chamados
{
    public partial class GerenciarUsuarios : Form
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private DataTable usuariosTable = new DataTable();

        public GerenciarUsuarios()
        {
            InitializeComponent();

           
            _usuarioRepository = new UsuarioRepository();

            ConfigurarGrade();
            EstilizarGrade();

            this.Load += GerenciarUsuarios_Load;
            txtPesquisarUser.TextChanged += TxtPesquisar_TextChanged;
        }

        private void ConfigurarGrade()
        {
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.Columns.Clear();

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdUsuario", DataPropertyName = "IdUsuario", HeaderText = "ID", Width = 60 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nome", DataPropertyName = "Nome", HeaderText = "Nome", Width = 180 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "Login", DataPropertyName = "Login", HeaderText = "Login", Width = 80 });

          
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cargo", DataPropertyName = "Cargo", HeaderText = "Função", Width = 100 }); 

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", DataPropertyName = "Email", HeaderText = "Email", Width = 250 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { Name = "Setor", DataPropertyName = "Setor", HeaderText = "Setor", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private void EstilizarGrade()
        {
           
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, dgvUsuarios, new object[] { true });
            dgvUsuarios.ColumnHeadersHeight = 35;
            dgvUsuarios.RowTemplate.Height = 35;
        }

        private void GerenciarUsuarios_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessaoUsuario.Nome))
                lbl_NomeUser.Text = $" {SessaoUsuario.Nome}";
            else
                lbl_NomeUser.Text = "Usuário não identificado";

            CarregarUsuarios();
        }

       
        private void CarregarUsuarios(string filtro = "")
        {
            try
            {
              
                usuariosTable = _usuarioRepository.BuscarTodosFiltrados(filtro);
                dgvUsuarios.DataSource = usuariosTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar usuários: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtPesquisar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtPesquisarUser.Text.Trim();
            CarregarUsuarios(filtro);
        }

        private void btnEditarUsuario_Click(object sender, EventArgs e)
        {

            if (dgvUsuarios.CurrentRow == null || dgvUsuarios.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Por favor, selecione um usuário para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            object idValue = dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value;
            if (idValue != null && int.TryParse(idValue.ToString(), out int idUsuarioSelecionado))
            {
                
                var telaDeEdicao = new Editar_Usuario(idUsuarioSelecionado);
                telaDeEdicao.ShowDialog();

               
                CarregarUsuarios(txtPesquisarUser.Text.Trim());
            }
            else
            {
                MessageBox.Show("Não foi possível identificar o ID do usuário selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCadastroUser_Click(object sender, EventArgs e)
        {
          
            var cadastro = new Cadastro_de_Usuarios();
            cadastro.ShowDialog();
            CarregarUsuarios(); 
        }

        private async void btnExcluirUsuario_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null || dgvUsuarios.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Por favor, selecione um usuário para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                object idValue = dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value;
                object nomeValue = dgvUsuarios.CurrentRow.Cells["Nome"].Value;

                if (idValue != null && int.TryParse(idValue.ToString(), out int idUsuarioSelecionado))
                {
                    string nomeUsuario = nomeValue?.ToString() ?? "usuário selecionado";

                    var confirmResult = MessageBox.Show(
                        $"Deseja realmente excluir o usuário: {nomeUsuario}?",
                        "Confirmar Exclusão",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                       
                        await _usuarioRepository.ExcluirAsync(idUsuarioSelecionado);
                        MessageBox.Show("Usuário excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                      
                        CarregarUsuarios(txtPesquisarUser.Text.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Erro ao excluir usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Código de Estética e Navegação (Sem Alterações)

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

        private void GerenciarUsuarios_Paint(object sender, PaintEventArgs e)
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