using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gerenciamento_De_Chamados.Models;      
using Gerenciamento_De_Chamados.Repositories; 
using Gerenciamento_De_Chamados.Services;
using Gerenciamento_De_Chamados.Helpers;

namespace Gerenciamento_De_Chamados
{
    public partial class Responder_Chamado : Form
    {
        

       
        private readonly IChamadoRepository _chamadoRepository;
        private DataTable chamadosTable = new DataTable();

        public Responder_Chamado()
        {
            InitializeComponent();

            
            _chamadoRepository = new ChamadoRepository();

            ConfigurarGrade();
            this.Load += Responder_Chamado_Load;
        }

        private void Responder_Chamado_Load(object sender, EventArgs e)
        {
            CarregarChamados();
        }

        private void ConfigurarGrade()
        {
            // Seu código de ConfigurarGrade() permanece o mesmo
            dgvResponder.RowTemplate.Height = 35;
            dgvResponder.ColumnHeadersHeight = 35;
            dgvResponder.AutoGenerateColumns = false;
            dgvResponder.Columns.Clear();
            dgvResponder.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdChamado", DataPropertyName = "IdChamado", HeaderText = "ID", Width = 60 });
            dgvResponder.Columns.Add(new DataGridViewTextBoxColumn { Name = "Titulo", DataPropertyName = "Titulo", HeaderText = "Titulo", Width = 250 });
            dgvResponder.Columns.Add(new DataGridViewTextBoxColumn { Name = "Descricao", DataPropertyName = "Descricao", HeaderText = "Descricao", Width = 450 });
            dgvResponder.Columns.Add(new DataGridViewTextBoxColumn { Name = "Prioridade", DataPropertyName = "Prioridade", HeaderText = "Prioridade", Width = 100 });
            dgvResponder.Columns.Add(new DataGridViewTextBoxColumn { Name = "data", DataPropertyName = "data", HeaderText = "data", Width = 100 });
            dgvResponder.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", DataPropertyName = "Status", HeaderText = "Status", Width = 120 });
            dgvResponder.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdUsuario", DataPropertyName = "IdUsuario", HeaderText = "Usuario", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

       
        private void CarregarChamados(string filtro = "")
        {
            
            try
            {
                // Chama o repositório
                chamadosTable = _chamadoRepository.BuscarTodosFiltrados(filtro);
                dgvResponder.DataSource = chamadosTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar chamados: " + ex.Message);
            }
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

        private void Responder_Chamado_Paint(object sender, PaintEventArgs e)
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

    
        private async void dgvResponder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                DataGridViewRow row = dgvResponder.Rows[e.RowIndex];
                object idValue = row.Cells["IdChamado"].Value;

                if (idValue != null && int.TryParse(idValue.ToString(), out int idChamadoSelecionado))
                {
                    //Buscar dados do chamado usando o repositório
                    Chamado chamado = await _chamadoRepository.BuscarPorIdAsync(idChamadoSelecionado);

                    if (chamado == null)
                    {
                        MessageBox.Show("Chamado não encontrado no banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Verificar se a IA precisa ser executada
                    if (string.IsNullOrEmpty(chamado.PrioridadeSugeridaIA) || chamado.PrioridadeSugeridaIA == "Pendente" || chamado.PrioridadeSugeridaIA == "Análise Pendente")
                    {
                        MessageBox.Show("Este chamado ainda não foi triado. Iniciando análise da IA... Isso pode levar alguns segundos.", "Análise da IA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            // Buscar soluções anteriores (agora pelo repositório)
                            List<string> solucoesAnteriores = await _chamadoRepository.BuscarSolucoesAnterioresAsync(chamado.Categoria);

                            AIService aiService = new AIService();
                            var (novoProblema, novaPrioridade, novaSolucao) = await aiService.AnalisarChamado(
                                chamado.Titulo,
                                chamado.PessoasAfetadas,
                                chamado.OcorreuAnteriormente,
                                chamado.ImpedeTrabalho,
                                chamado.Descricao,
                                chamado.Categoria,
                                solucoesAnteriores
                            );

                            if (novaPrioridade == "Não identificado" || novaPrioridade.Contains("Erro"))
                            {
                                MessageBox.Show($"A IA não conseguiu analisar o chamado. Detalhes: {novaSolucao}", "Erro na IA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                // Atualizar o chamado no BD com os dados da IA, via repositório
                                await _chamadoRepository.AtualizarSugestoesIAAsync(idChamadoSelecionado, novaPrioridade, novoProblema, novaSolucao);

                                // (O código original não atualizava o DataGridView, então mantemos esse comportamento)
                            }
                        }
                        catch (Exception aiEx)
                        {
                            MessageBox.Show($"Erro ao executar a análise da IA: {aiEx.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    // Abrir a tela de Análise 
                    var analisechamado = new AnaliseChamado(idChamadoSelecionado);
                    analisechamado.ShowDialog();

                    // Recarrega os chamados após fechar a tela de análise
                    CarregarChamados();
                }
                else
                {
                    MessageBox.Show("Não foi possível obter o ID do chamado selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao processar o chamado: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
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

        private void btnVisualizarCh_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            FormHelper.FAQ(this);
        }

        private void bt_Criar_Click(object sender, EventArgs e)
        {
            var criarchamado = new AberturaChamados();
            criarchamado.Show();
            this.Hide();
        }
    }
}