using System;
using System.Data;
using System.Data.SqlClient;
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

            // evento para carregar os dados ao abrir o form
            this.Load += GerenciarUsuarios_Load;

            // evento para pesquisar
            txtPesquisarUser.TextChanged += TxtPesquisar_TextChanged;
        }

        private void btnCadastroUser_Click(object sender, EventArgs e)
        {
            var Cadastro = new Cadastro_de_Usuarios();
            Cadastro.Show();
            this.Visible = false;
        }

        private void GerenciarUsuarios_Load(object sender, EventArgs e)
        {
            CarregarUsuarios();
        }

        private void CarregarUsuarios(string filtro = "")
        {
            string sql = @"
                SELECT Nome, Email, FuncaoUsuario AS Cargo FROM Usuario
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

                    dgvUsuarios.Columns.Clear();
                    dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nome", DataPropertyName = "Nome" });
                    dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email" });
                    dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cargo", DataPropertyName = "Cargo" });
                    dgvUsuarios.DataSource = usuariosTable;

                    
                    dgvUsuarios.AutoGenerateColumns = false; 
                    da.Fill(usuariosTable);
                    dgvUsuarios.DataSource = usuariosTable;
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
    }
}
