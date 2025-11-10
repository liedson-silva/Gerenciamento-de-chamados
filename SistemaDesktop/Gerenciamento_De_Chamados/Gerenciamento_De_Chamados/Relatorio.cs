using Gerenciamento_De_Chamados.Helpers;
using Gerenciamento_De_Chamados.Models; 
using Gerenciamento_De_Chamados.Repositories; 
using ScottPlot;
using ScottPlot.Interactivity;
using ScottPlot.Interactivity.UserActionResponses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq; 
using System.Threading.Tasks; 
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados
{
    public partial class Relatorio : Form
    {
        
        private readonly IRelatorioRepository _relatorioRepository;

        public Relatorio()
        {
            InitializeComponent();

           
            _relatorioRepository = new RelatorioRepository();

            this.Load += Relatorio_Load;
        }

       
        private async void Relatorio_Load(object sender, EventArgs e)
        {
            //Trava o primeiro gráfico (Visão Geral)
            formPlotVisaoGeral.UserInputProcessor.IsEnabled = true;
            formPlotVisaoGeral.UserInputProcessor.UserActionResponses.Clear(); // Remove as ações (Dar zoom, arrastar)

            // Trava o segundo gráfico (Prioridades)
            fpPrioridades.UserInputProcessor.IsEnabled = true;
            fpPrioridades.UserInputProcessor.UserActionResponses.Clear(); // Remove TODAS as ações do grafico 2

            var menuButton = StandardMouseButtons.Right;
            var menuResponse = new SingleClickContextMenu(menuButton);
            formPlotVisaoGeral.UserInputProcessor.UserActionResponses.Add(menuResponse);
            fpPrioridades.UserInputProcessor.UserActionResponses.Add(new SingleClickContextMenu(menuButton)); 

            try
            {
                await CarregarGraficoStatusAsync();
                await CarregarGraficoPrioridadeAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados dos relatórios: \n" + ex.Message);
            }
        }

     
        private async Task CarregarGraficoStatusAsync()
        {
            // Busca os dados do Repositório
            List<ChartDataPoint> statusData = await _relatorioRepository.GetStatusChartDataAsync();

            // Prepara os dados para o gráfico
            double[] valores = statusData.Select(d => d.Value).ToArray();
            string[] labels = statusData.Select(d => d.Label).ToArray();
            double total = valores.Sum();

            lblTotalChamados.Text = total.ToString();

            // Atualiza os KCards (de forma segura)
            var pendente = statusData.FirstOrDefault(d => d.Label.Equals("Pendente", StringComparison.OrdinalIgnoreCase));
            var emAndamento = statusData.FirstOrDefault(d => d.Label.Equals("Em Andamento", StringComparison.OrdinalIgnoreCase));
            var resolvido = statusData.FirstOrDefault(d => d.Label.Equals("Resolvido", StringComparison.OrdinalIgnoreCase));

            lblTotalPendentes.Text = pendente?.Value.ToString() ?? "0";
            lblTotalResolvidos.Text = resolvido?.Value.ToString() ?? "0";

            // Configura as Legendas
            // Define cores padrão
            var corPendente = new ScottPlot.Color(239, 83, 80);
            var corAndamento = new ScottPlot.Color(126, 87, 194);
            var corResolvido = new ScottPlot.Color(102, 187, 106);

            pnPendente.BackColor = System.Drawing.Color.FromArgb(corPendente.R, corPendente.G, corPendente.B);
            pnEmAndamento.BackColor = System.Drawing.Color.FromArgb(corAndamento.R, corAndamento.G, corAndamento.B);
            pnResolvido.BackColor = System.Drawing.Color.FromArgb(corResolvido.R, corResolvido.G, corResolvido.B);

            lblPendente.Text = $"Pendente ({pendente?.Value / total ?? 0:P1})";
            lblEmAndamento.Text = $"Em Andamento ({emAndamento?.Value / total ?? 0:P1})";
            lblResolvido.Text = $"Resolvido ({resolvido?.Value / total ?? 0:P1})";

            // Monta o Gráfico
            if (valores.Length == 0)
            {
                formPlotVisaoGeral.Plot.Title("Sem dados para exibir");
                formPlotVisaoGeral.Plot.Axes.Frameless();
                formPlotVisaoGeral.Plot.HideGrid();
                formPlotVisaoGeral.Refresh();
                return;
            }

            var piePlot = formPlotVisaoGeral.Plot.Add.Pie(valores);
            piePlot.DonutFraction = 0.6;
            piePlot.LineWidth = 0;

            // Atribui as cores na ordem correta
            piePlot.Slices = new List<ScottPlot.PieSlice>();
            for (int i = 0; i < labels.Length; i++)
            {
                var slice = new ScottPlot.PieSlice { Value = valores[i], Label = "" }; // Label em branco
                if (labels[i].Equals("Pendente", StringComparison.OrdinalIgnoreCase))
                    slice.Fill.Color = corPendente;
                else if (labels[i].Equals("Em Andamento", StringComparison.OrdinalIgnoreCase))
                    slice.Fill.Color = corAndamento;
                else if (labels[i].Equals("Resolvido", StringComparison.OrdinalIgnoreCase))
                    slice.Fill.Color = corResolvido;

                piePlot.Slices.Add(slice);
            }

            //formPlotVisaoGeral.Plot.Axes.Frameless();
            formPlotVisaoGeral.Plot.HideGrid();
            formPlotVisaoGeral.Plot.Axes.SetLimits(-1.5, 1.5, -1.5, 1.5);
            formPlotVisaoGeral.Plot.Layout.Frameless();
            formPlotVisaoGeral.Refresh();
        }

      
        private async Task CarregarGraficoPrioridadeAsync()
        {
            // Busca os dados do Repositório
            List<ChartDataPoint> priorityData = await _relatorioRepository.GetPriorityChartDataAsync();

            // Prepara os dados
            double[] valores = priorityData.Select(d => d.Value).ToArray();
            string[] labels = priorityData.Select(d => d.Label).ToArray();
            double total = valores.Sum();

            // Define Cores e Legendas
            var corAlta = new ScottPlot.Color(239, 83, 80);
            var corMedia = new ScottPlot.Color(33, 150, 243);
            var corBaixa = new ScottPlot.Color(102, 187, 106);

            var alta = priorityData.FirstOrDefault(d => d.Label.Equals("Alta", StringComparison.OrdinalIgnoreCase));
            var media = priorityData.FirstOrDefault(d => d.Label.Equals("Média", StringComparison.OrdinalIgnoreCase));
            var baixa = priorityData.FirstOrDefault(d => d.Label.Equals("Baixa", StringComparison.OrdinalIgnoreCase));

            pnAlta.BackColor = System.Drawing.Color.FromArgb(corAlta.R, corAlta.G, corAlta.B);
            pnMedia.BackColor = System.Drawing.Color.FromArgb(corMedia.R, corMedia.G, corMedia.B);
            pnBaixa.BackColor = System.Drawing.Color.FromArgb(corBaixa.R, corBaixa.G, corBaixa.B);

            lblAlta.Text = $"Alta ({alta?.Value / total ?? 0:P1})";
            lblMedia.Text = $"Média ({media?.Value / total ?? 0:P1})";
            lblBaixa.Text = $"Baixa ({baixa?.Value / total ?? 0:P1})";

            // Monta o Gráfico
            if (valores.Length == 0)
            {
                fpPrioridades.Plot.Title("Sem dados de prioridade");
                fpPrioridades.Plot.Axes.Frameless();
                fpPrioridades.Plot.HideGrid();
                fpPrioridades.Refresh();
                return;
            }

            var piePlotPrioridade = fpPrioridades.Plot.Add.Pie(valores);
            piePlotPrioridade.DonutFraction = 0.6;
            piePlotPrioridade.LineWidth = 0;

            // Atribui as cores na ordem correta
            piePlotPrioridade.Slices = new List<ScottPlot.PieSlice>();
            for (int i = 0; i < labels.Length; i++)
            {
                var slice = new ScottPlot.PieSlice { Value = valores[i], Label = "" };
                if (labels[i].Equals("Alta", StringComparison.OrdinalIgnoreCase))
                    slice.Fill.Color = corAlta;
                else if (labels[i].Equals("Média", StringComparison.OrdinalIgnoreCase))
                    slice.Fill.Color = corMedia;
                else if (labels[i].Equals("Baixa", StringComparison.OrdinalIgnoreCase))
                    slice.Fill.Color = corBaixa;

                piePlotPrioridade.Slices.Add(slice);
            }

            fpPrioridades.Plot.Axes.SetLimits(-1.5, 1.5, -1.5, 1.5);
            fpPrioridades.Plot.Layout.Frameless();
            fpPrioridades.Plot.HideGrid();
            fpPrioridades.Plot.Benchmark.IsVisible = false;
            fpPrioridades.Refresh();
        }


        #region Métodos de UI e Navegação (Sem Alterações)
        private void panelPrincipal_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            System.Drawing.Color corInicio = System.Drawing.Color.White;
            System.Drawing.Color corFim = ColorTranslator.FromHtml("#232325");

            using (LinearGradientBrush gradiente = new LinearGradientBrush(
                   this.ClientRectangle, corInicio, corFim, LinearGradientMode.Horizontal))
            {
                g.FillRectangle(gradiente, this.ClientRectangle);
            }
        }

        private void panelSidebar_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            System.Drawing.Color corInicioPanel = System.Drawing.Color.White;
            System.Drawing.Color corFimPanel = ColorTranslator.FromHtml("#232325");

            LinearGradientBrush gradientePanel = new LinearGradientBrush(
                         panelSidebar.ClientRectangle,
                         corInicioPanel,
                         corFimPanel,
                         System.Drawing.Drawing2D.LinearGradientMode.Vertical);

            g.FillRectangle(gradientePanel, panelSidebar.ClientRectangle);
        }

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            DateTime dataInicio = dtpDe.Value;
            DateTime dataFim = dtpAte.Value;

            if (dataFim < dataInicio)
            {
                MessageBox.Show("A data 'Até' não pode ser anterior à data 'De'.");
                return;
            }
            
            FormRelatorioDetalhado frmDetalhe = new FormRelatorioDetalhado(dataInicio, dataFim);
            frmDetalhe.Show();
        }

        private void lbl_Inicio_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void PctBox_Inicio_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void lbSair_Click(object sender, EventArgs e)
        {
            FormHelper.Sair(this);
        }
        #endregion

        private void label3_Click(object sender, EventArgs e)
        {
            FormHelper.FAQ(this);
        }
    }
}