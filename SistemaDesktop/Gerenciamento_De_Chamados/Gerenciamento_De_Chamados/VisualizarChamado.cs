using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gerenciamento_De_Chamados.Repositories;
using Gerenciamento_De_Chamados.Helpers;
using Gerenciamento_De_Chamados.Models;

namespace Gerenciamento_De_Chamados
{
    public partial class VisualizarChamado : Form
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly string _filtroStatusInicial;
        private bool _isLoading = true;

        public VisualizarChamado(string filtroStatus = "")
        {
            InitializeComponent();
            this.Load += VisualizarChamado_Load;
            ConfigurarGrade();

            _chamadoRepository = new ChamadoRepository();
            _filtroStatusInicial = filtroStatus;

        }

        private async void VisualizarChamado_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessaoUsuario.Nome))
                lbl_NomeUser.Text = ($" {SessaoUsuario.Nome}");
            else
                lbl_NomeUser.Text = "Usuário não identificado";


            await CarregarChamados();
            _isLoading = false;

            txtPesquisarChamados.TextChanged += TxtPesquisar_TextChanged;
        }

        private void ConfigurarGrade()
        {
            dgvChamados.RowTemplate.Height = 30;
            dgvChamados.ColumnHeadersHeight = 30;
            dgvChamados.AutoGenerateColumns = false;
            dgvChamados.Columns.Clear();

            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdChamado", DataPropertyName = "IdChamado", HeaderText = "ID", Width = 60 });
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Titulo", DataPropertyName = "Titulo", HeaderText = "Titulo", Width = 200 });
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Prioridade", DataPropertyName = "PrioridadeChamado", HeaderText = "Prioridade", Width = 100 });
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Descricao", DataPropertyName = "Descricao", HeaderText = "Descricao", Width = 300 });
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Data", DataPropertyName = "DataChamado", HeaderText = "Data", Width = 100 });
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", DataPropertyName = "StatusChamado", HeaderText = "Status", Width = 120 });
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Categoria", DataPropertyName = "Categoria", HeaderText = "Categoria", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private async Task CarregarChamados()
        {
            try
            {
                string status = "";
                string pesquisa = "";

                if (_isLoading && !string.IsNullOrEmpty(_filtroStatusInicial))
                {
                    status = _filtroStatusInicial;
                    pesquisa = "";
                }
              
                else if (!_isLoading)
                {
                    status = "";
                    pesquisa = txtPesquisarChamados.Text;
                }
             

                DataTable dt = await _chamadoRepository.BuscarMeusChamadosFiltrados(
                    SessaoUsuario.IdUsuario,
                    status,
                    pesquisa
                );

                // --- CORREÇÃO 4: Verificar se a Form foi fechada antes de atualizar a UI ---
                if (this.IsDisposed) return;

                dgvChamados.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    // Se foi a carga inicial (vinda do Home) que não achou nada
                    if (_isLoading && !string.IsNullOrEmpty(_filtroStatusInicial))
                    {
                        MessageBox.Show($"Não há chamados com o status '{_filtroStatusInicial}'.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Fecha a tela com segurança DEPOIS que tudo terminar
                        this.BeginInvoke(new Action(() => this.Close()));
                    }
                    // Se foi uma pesquisa manual do usuário que não achou nada
                    else if (!_isLoading)
                    {
                        string msgPesquisa = string.IsNullOrEmpty(pesquisa) ? "disponíveis" : $"para a pesquisa '{pesquisa}'";
                        MessageBox.Show($"Não há chamados {msgPesquisa} para exibir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                
            }
            catch (Exception ex)
            {
                if (!this.IsDisposed)
                {
                    MessageBox.Show("Erro ao carregar chamados: " + ex.Message);
                }
            }
        }

        private async void TxtPesquisar_TextChanged(object sender, EventArgs e)
        {


            await Task.Delay(300); 
            await CarregarChamados();
        }

        private async void dgvChamados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvChamados.Rows[e.RowIndex];
                object idValue = row.Cells["IdChamado"].Value;

                if (idValue != null && int.TryParse(idValue.ToString(), out int idChamadoSelecionado))
                {
                    var telaDetalhes = new ChamadoCriado(idChamadoSelecionado);
                    telaDetalhes.ShowDialog();

                    await CarregarChamados();
                    
                }
            }
        }

        // (Restante do seu código: lblInicio_Click, PctBox_Logo_Click, etc.)
        private void lblInicio_Click(object sender, EventArgs e) { FormHelper.BotaoHome(this); }
        private void PctBox_Logo_Click(object sender, EventArgs e) { FormHelper.BotaoHome(this); }
        private void lbSair_Click(object sender, EventArgs e) { FormHelper.Sair(this); }
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

        private void VisualizarChamado_Paint(object sender, PaintEventArgs e)
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