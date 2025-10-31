using Gerenciamento_De_Chamados.Models; 
using Gerenciamento_De_Chamados.Repositories;
using Gerenciamento_De_Chamados.Helpers;
using System;
using System.Configuration; 
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Gerenciamento_De_Chamados
{
    public partial class AnaliseChamado : Form
    {
        private int _chamadoId;
        
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IHistoricoRepository _historicoRepository;

        public AnaliseChamado(int chamadoId)
        {
            InitializeComponent();
            _chamadoId = chamadoId;

            
            _chamadoRepository = new ChamadoRepository();
            _historicoRepository = new HistoricoRepository();

            
            this.Load += AnaliseChamado_Load;

           


           
            this.btnResponderCH.Click += new System.EventHandler(this.btnResponderCH_Click);
            this.btnCancelar2.Click += new System.EventHandler(this.btnCancelar_Click);
        }

        private async void AnaliseChamado_Load(object sender, EventArgs e)
        {
            await CarregarDadosChamadoAsync(); // MUDADO PARA ASYNC
     
        }

    
        private async Task CarregarDadosChamadoAsync()
        {
            try
            {
                // Busca o chamado principal
                Chamado chamado = await _chamadoRepository.BuscarPorIdAsync(_chamadoId);

                if (chamado == null)
                {
                    MessageBox.Show("Chamado não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Preenche os campos de análise

                // Guarda o status atual
                string statusAtual = chamado.StatusChamado;
                this.Tag = statusAtual;

                // Se estiver resolvido, busca a última solução
                if (statusAtual == "Resolvido")
                {
                    string solucao = await _historicoRepository.BuscarUltimaSolucaoAsync(_chamadoId);
                    if (solucao != null)
                    {
                        txtSolucaoFinal.Text = solucao;
                    }
                    else
                    {
                        txtSolucaoFinal.Text = "Solução não registrada no histórico.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados do chamado: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }





        private async void btnSalvarAnalise_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Deseja salvar esta análise e mover o chamado para 'Em Andamento'?", "Confirmar Análise", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
        }

            // Inicia a conexão e a transação AQUI, no formulário

        
        private async void btnResponderCH_Click(object sender, EventArgs e)
        {
            string solucaoFinal = txtSolucaoFinal.Text;
            string novoStatus = "Resolvido";
            DateTime dataAgora = ObterHoraBrasilia();

            if (string.IsNullOrWhiteSpace(solucaoFinal))
            {
                MessageBox.Show("Por favor, descreva a solução aplicada antes de resolver o chamado.", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Deseja aplicar esta solução e marcar o chamado como 'Resolvido'?", "Confirmar Resolução", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // Chama o repositório de Chamado
                        await _chamadoRepository.AtualizarStatusAsync(_chamadoId, novoStatus, conn, trans);

                        // Prepara o Histórico (Troca de Status)
                        Historico histStatus = new Historico
                        {
                            DataSolucao = dataAgora,
                            Solucao = "O status foi alterado para Resolvido",
                            FK_IdChamado = _chamadoId,
                            Acao = "Troca de Status"
                        };
                        await _historicoRepository.AdicionarAsync(histStatus, conn, trans);

                        // Prepara o Histórico (Solução Aplicada)
                        Historico histSolucao = new Historico
                        {
                            DataSolucao = dataAgora,
                            Solucao = solucaoFinal,
                            FK_IdChamado = _chamadoId,
                            Acao = "Solução Aplicada"
                        };
                        await _historicoRepository.AdicionarAsync(histSolucao, conn, trans);

                        // Se tudo deu certo, COMITA
                        trans.Commit();

                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Chamado resolvido e registrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close(); // Fecha o formulário
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Erro ao salvar a resolução: " + ex.Message, "Erro de Transação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #region Métodos Auxiliares e de UI 

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DateTime ObterHoraBrasilia()
        {
            try
            {
                TimeZoneInfo brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brasilTimeZone);
            }
            catch { return DateTime.Now; }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null) return;
            Graphics g = e.Graphics;
            Color corInicioPanel = Color.White;
            Color corFimPanel = ColorTranslator.FromHtml("#232325");
            LinearGradientBrush gradientePanel = new LinearGradientBrush(
                   panel.ClientRectangle,
                   corInicioPanel,
                   corFimPanel,
                   LinearGradientMode.Vertical);
            g.FillRectangle(gradientePanel, panel.ClientRectangle);
        }

        private void btnCancelar2_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}