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
        private readonly string _filtroInicial;


        private bool _isFiltroPrioridade = false;

        public VisualizarChamado(string filtro = "")
        {
            InitializeComponent();
            this.Load += VisualizarChamado_Load;
            ConfigurarGrade();

            _chamadoRepository = new ChamadoRepository();
            _filtroInicial = filtro;

            if (!string.IsNullOrEmpty(filtro) && (filtro == "Alta" || filtro == "Média" || filtro == "Baixa"))
            {
                _isFiltroPrioridade = true;
                this.Text = $"Visualizando Chamados - Prioridade: {filtro}";
            }
            else if (!string.IsNullOrEmpty(filtro))
            {
                this.Text = $"Visualizando Chamados - Status: {filtro}";
            }
        }

        private async void VisualizarChamado_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessaoUsuario.Nome))
                lbl_NomeUser.Text = ($" {SessaoUsuario.Nome}");
            else
                lbl_NomeUser.Text = "Usuário não identificado";

            await CarregarChamados();

            // Associa o evento de pesquisa apenas após o carregamento inicial
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
                string filtroTexto = txtPesquisarChamados.Text;
                DataTable dt;

                if (_isFiltroPrioridade)
                {

                    dt = await _chamadoRepository.BuscarPorPrioridadeEFiltrarAsync(_filtroInicial, filtroTexto);
                }
                else
                {

                    dt = await _chamadoRepository.BuscarMeusChamadosFiltrados(
                        SessaoUsuario.IdUsuario,
                        _filtroInicial,
                        filtroTexto
                    );
                }

                if (this.IsDisposed) return;

                dgvChamados.DataSource = dt;

                if (dt.Rows.Count == 0 && string.IsNullOrEmpty(filtroTexto) && !string.IsNullOrEmpty(_filtroInicial))
                {
                   
                   MessageBox.Show($"Nenhum chamado encontrado com o filtro '{_filtroInicial}'.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ObjectDisposedException) { }
            catch (Exception ex)
            {
                if (!this.IsDisposed)
                    MessageBox.Show("Erro ao carregar chamados: " + ex.Message);
            }
        }

        private async void TxtPesquisar_TextChanged(object sender, EventArgs e)
        {
            // Pequeno delay para não fazer queries a cada letra digitada instantaneamente
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
                    // Lógica de Redirecionamento baseada no Perfil
                    // Se for Tecnico/Admin, abre a tela de Analise/Resposta para TRABALHAR no chamado
                    if (SessaoUsuario.FuncaoUsuario == "Tecnico" || SessaoUsuario.FuncaoUsuario == "Admin" || SessaoUsuario.FuncaoUsuario == "Administrador")
                    {
                        var telaAnalise = new ChamadoCriado(idChamadoSelecionado);
                        telaAnalise.ShowDialog();
                    }
                    else
                    {
                        // Se for usuário comum, abre apenas visualização dos detalhes
                        var telaDetalhes = new ChamadoCriado(idChamadoSelecionado);
                        telaDetalhes.ShowDialog();
                    }

                    // Recarrega a lista ao voltar, para atualizar status/alterações
                    await CarregarChamados();
                }
            }
        }

        #region Navegação e Estética
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
        #endregion

        private void lblMconta_Click(object sender, EventArgs e)
        {
            var visualizarUsuario = new Visualizar_Usuario(SessaoUsuario.IdUsuario);
            visualizarUsuario.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            var verChamado = new VisualizarChamado();
            this.Hide();
            verChamado.ShowDialog();
            this.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            FormHelper.FAQ(this);
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }
    }
}