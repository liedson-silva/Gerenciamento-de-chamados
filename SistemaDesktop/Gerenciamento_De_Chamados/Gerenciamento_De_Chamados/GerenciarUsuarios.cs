using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados
{
    public partial class GerenciarUsuarios : Form
    {
        string connectionString = "Server=fatalsystemsrv1.database.windows.net;Database=DbaFatal-System;User Id=fatalsystem;Password=F1234567890m@;";
        private DataTable usuariosTable = new DataTable();

        public GerenciarUsuarios()
        {
            InitializeComponent();
            ConfigurarGrade(); // PASSO 1: Configura a grade assim que a tela é criada

            // Associa os eventos
            this.Load += GerenciarUsuarios_Load;
            txtPesquisarUser.TextChanged += TxtPesquisar_TextChanged;
        }

        // NOVO MÉTODO: Apenas para configurar o design e as colunas da grade
        private void ConfigurarGrade()
        {
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.Columns.Clear(); // Limpa quaisquer colunas do modo design

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdUsuario", // Nome da coluna
                DataPropertyName = "IdUsuario", // De onde vem o dado na tabela
                HeaderText = "ID",
                Visible = false // Coluna oculta
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nome",
                DataPropertyName = "Nome",
                HeaderText = "Nome",
                Width = 250
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                DataPropertyName = "Email",
                HeaderText = "Email",
                Width = 250
            });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cargo",
                DataPropertyName = "Cargo",
                HeaderText = "Cargo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill // Ocupa o espaço restante
            });
        }


        private void GerenciarUsuarios_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Funcoes.SessaoUsuario.Nome))
                lbl_NomeUser.Text = $"Olá: {Funcoes.SessaoUsuario.Nome}";
            else
                lbl_NomeUser.Text = "Usuário não identificado";

            CarregarUsuarios();
        }

        private void CarregarUsuarios(string filtro = "")
        {
            string sql = @"
                SELECT IdUsuario, Nome, Email, FuncaoUsuario AS Cargo 
                FROM Usuario
                WHERE (@filtro = '' OR Nome LIKE '%' + @filtro + '%'
                                    OR Email LIKE '%' + @filtro + '%' 
                                    OR FuncaoUsuario LIKE '%' + @filtro + '%')
                ORDER BY Nome";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@filtro", filtro ?? string.Empty);
                    usuariosTable.Clear();
                    da.Fill(usuariosTable);
                    dgvUsuarios.DataSource = usuariosTable; // Apenas atualiza a fonte de dados
                }
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
            int idUsuarioSelecionado;

            if (idValue != null && int.TryParse(idValue.ToString(), out idUsuarioSelecionado))
            {
                var telaDeEdicao = new Editar_Usuario(idUsuarioSelecionado);

                telaDeEdicao.ShowDialog(); //Abre a tela e ESPERA ela ser fechada.

                CarregarUsuarios(); //Recarrega a grade com os dados atualizados!
            }
            else
            {
                MessageBox.Show("Não foi possível identificar o ID do usuário selecionado...", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCadastroUser_Click(object sender, EventArgs e)
        {
            // Abre a tela de cadastro para um NOVO usuário
            var cadastro = new Cadastro_de_Usuarios();
            cadastro.ShowDialog();
            CarregarUsuarios();
            
        }


        private void btnExcluirUsuario_Click(object sender, EventArgs e)
        {

        }

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
            Funcoes.BotaoHomeAdmin(this);
        }
    }
    }
