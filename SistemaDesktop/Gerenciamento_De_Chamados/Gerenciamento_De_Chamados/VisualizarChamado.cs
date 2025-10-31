using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gerenciamento_De_Chamados.Repositories;
using Gerenciamento_De_Chamados.Helpers;

namespace Gerenciamento_De_Chamados
{
    public partial class VisualizarChamado : Form
    {
        private IChamadoRepository _chamadoRepository;
        private DataTable chamadosTable = new DataTable();
        public VisualizarChamado()

        {
            InitializeComponent();

            // evento para carregar os dados ao abrir o form
            this.Load += VisualizarChamado_Load;

            ConfigurarGrade();

            _chamadoRepository = new ChamadoRepository();

            // evento para pesquisar
            txtPesquisarChamados.TextChanged += TxtPesquisar_TextChanged;
        }

        private void VisualizarChamado_Load(object sender, EventArgs e)
        {
            CarregarChamados();

            if (!string.IsNullOrEmpty(SessaoUsuario.Nome))
                lbl_NomeUser.Text = ($"Bem vindo {SessaoUsuario.Nome}");
            else
                lbl_NomeUser.Text = "Usuário não identificado";


        }
        private void ConfigurarGrade()
        {

            dgvChamados.RowTemplate.Height = 30;

            dgvChamados.ColumnHeadersHeight = 30;

            dgvChamados.AutoGenerateColumns = false;

            dgvChamados.Columns.Clear();


            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdChamado",
                DataPropertyName = "IdChamado",
                HeaderText = "ID",
                Width = 60
            });
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Titulo",
                DataPropertyName = "Titulo",
                HeaderText = "Titulo",
                Width = 200
            });
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Prioridade",
                DataPropertyName = "Prioridade",
                HeaderText = "Prioridade",
                Width = 200
            });
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descricao",
                DataPropertyName = "Descricao",
                HeaderText = "Descricao",
                Width = 300
            });
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "data",
                DataPropertyName = "data",
                HeaderText = "data",
                Width = 100
            });
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 120
            });
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Categoria",
                DataPropertyName = "Categoria",
                HeaderText = "Categoria",
                Width = 120
            });
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdUsuario",
                DataPropertyName = "IdUsuario",
                HeaderText = "IdUsuario",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void CarregarChamados(string filtro = "")
        {
            try
            {
                // A única linha necessária
                chamadosTable = _chamadoRepository.BuscarTodosFiltrados(filtro);
                dgvChamados.DataSource = chamadosTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar chamados: " + ex.Message);
            }
        }
        

        private void TxtPesquisar_TextChanged(object sender, EventArgs e)
        {
            CarregarChamados(txtPesquisarChamados.Text);
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
                    LinearGradientMode.Vertical); // Exemplo com gradiente horizontal
            g.FillRectangle(gradientePanel, panel1.ClientRectangle);

        }

        private void dgvChamados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o clique foi em uma linha de dados válida (e não no cabeçalho da coluna)
            if (e.RowIndex >= 0)
            {
                // Pega a linha inteira que recebeu o duplo-clique
                DataGridViewRow row = dgvChamados.Rows[e.RowIndex];

                // Pega o valor da célula que contém o ID. 
                object idValue = row.Cells["IdChamado"].Value;
                int idChamadoSelecionado;

                // Tenta converter o ID para um número inteiro de forma segura
                if (idValue != null && int.TryParse(idValue.ToString(), out idChamadoSelecionado))
                {
                    // Se a conversão funcionou, cria e abre a tela de detalhes, passando o ID
                    var telaDetalhes = new ChamadoCriado(idChamadoSelecionado);
                    telaDetalhes.ShowDialog();

                    CarregarChamados();
                }
            }
        }

        private void lblInicio_Click(object sender, EventArgs e)
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
    }
}
