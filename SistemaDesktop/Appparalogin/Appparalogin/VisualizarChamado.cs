using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Gerenciamento_De_Chamados
{
    public partial class VisualizarChamado : Form
    {
        string connectionString = "Server=fatalsystemsrv1.database.windows.net;Database=DbaFatal-System;User Id=fatalsystem;Password=F1234567890m@;";
        private DataTable chamadosTable = new DataTable();
        public VisualizarChamado()

        {
            InitializeComponent();

            // evento para carregar os dados ao abrir o form
            this.Load += VisualizarChamado_Load;

            // evento para pesquisar
            txtPesquisarChamados.TextChanged += TxtPesquisar_TextChanged;
        }

        private void VisualizarChamado_Load(object sender, EventArgs e)
        {
            CarregarChamados();
            

        }

        private void CarregarChamados(string filtro = "")
        {
            string sql = @"
                            SELECT 
                                    c.IdChamado, 
                                    u.Nome AS Usuario, 
                                    c.Titulo, 
                                    c.PrioridadeChamado AS Prioridade, 
                                    c.Descricao, 
                                    c.DataChamado AS Data, 
                                    c.StatusChamado AS Status, 
                                    c.Categoria 
                                    FROM Chamado c
                                    JOIN Usuario u ON c.FK_IdUsuario = u.IdUsuario
                                    WHERE (@filtro = '' OR c.Titulo LIKE '%' + @filtro + '%'
                                    OR c.PrioridadeChamado LIKE '%' + @filtro + '%'
                                    OR c.Descricao LIKE '%' + @filtro + '%'
                                    OR c.StatusChamado LIKE '%' + @filtro + '%'
                                    OR c.Categoria LIKE '%' + @filtro + '%'
                                    OR u.Nome LIKE '%' + @filtro + '%')
                                    ORDER BY c.DataChamado DESC";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@filtro", filtro ?? string.Empty);
                    chamadosTable.Clear();
                    dgvChamados.Columns.Clear();
                    dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID Chamado", DataPropertyName = "IdChamado" });
                    dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Usuario", DataPropertyName = "Usuario" });
                    dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Título", DataPropertyName = "Titulo" });
                    dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Prioridade", DataPropertyName = "Prioridade" });
                    dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Descrição", DataPropertyName = "Descricao" });
                    dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Data", DataPropertyName = "Data" });
                    dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Status", DataPropertyName = "Status" });
                    dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Categoria", DataPropertyName = "Categoria" });
                    dgvChamados.DataSource = chamadosTable;
                    // Configurações adicionais para o DataGridView
                    dgvChamados.AutoGenerateColumns = false; // Evita a geração automática de colunas
                    da.Fill(chamadosTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar chamados: " + ex.Message);
            }
        }
        // Adicione este método ao seu formulário VisualizarChamado

        private void TxtPesquisar_TextChanged(object sender, EventArgs e)
        {
            CarregarChamados(txtPesquisarChamados.Text);
        }
    }
}
