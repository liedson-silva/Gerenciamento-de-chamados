using Gerenciamento_De_Chamados.Helpers;
using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados
{
    public partial class HomeAdmin : Form
    {
        private readonly IChamadoRepository _chamadoRepository;
        public HomeAdmin()
        {
            InitializeComponent();
            _chamadoRepository = new ChamadoRepository();
            this.Load += HomeUsuario_Load;
        }
        private async void HomeUsuario_Load(object sender, EventArgs e)
        {
            plotStatus.UserInputProcessor.IsEnabled = true;
            plotStatus.UserInputProcessor.UserActionResponses.Clear();

            plotCategoria.UserInputProcessor.IsEnabled = true;
            plotCategoria.UserInputProcessor.UserActionResponses.Clear();

            if (!string.IsNullOrEmpty(SessaoUsuario.Nome))
                lbl_NomeUser.Text = $"Olá, {SessaoUsuario.Nome} ({SessaoUsuario.FuncaoUsuario})";
            else
                lbl_NomeUser.Text = "Usuário não identificado";

            try
            {
                await CarregarGraficosAsync();
                await CarregarGraficoCategoriasAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar gráficos: " + ex.Message);
            }
        }


        private async Task CarregarGraficosAsync()
        {
            List<ChartDataPoint> statusData = await _chamadoRepository.ContarPorStatusGeralAsync();

            double[] valores = statusData.Select(d => d.Value).ToArray();
            string[] labels = statusData.Select(d => d.Label).ToArray();
            double total = valores.Sum();
            if (total == 0) total = 1; // Evita divisão por zero se não houver dados

            if (valores.Length == 0)
            {
                plotStatus.Plot.Title("Você ainda não abriu chamados");
                plotStatus.Plot.Axes.Frameless();
                plotStatus.Plot.HideGrid();
                plotStatus.Refresh();
                return;
            }

            var piePlot = plotStatus.Plot.Add.Pie(valores);
            piePlot.DonutFraction = 0;
            piePlot.LineWidth = 0;

            piePlot.Slices = new List<ScottPlot.PieSlice>();
            for (int i = 0; i < labels.Length; i++)
            {
                var slice = new ScottPlot.PieSlice { Value = valores[i], Label = "" }; // Label vazio
                slice.Fill.Color = GetColorForStatus(labels[i]);
                piePlot.Slices.Add(slice);
            }


            try
            {
                var pendente = statusData.FirstOrDefault(d => d.Label.Equals("Pendente", StringComparison.OrdinalIgnoreCase));
                var emAndamento = statusData.FirstOrDefault(d => d.Label.Equals("Em Andamento", StringComparison.OrdinalIgnoreCase));
                var solucionado = statusData.FirstOrDefault(d => d.Label.Equals("Resolvido", StringComparison.OrdinalIgnoreCase));

                var corPendente = GetColorForStatus("Pendente");
                var corEmAndamento = GetColorForStatus("Em Andamento");
                var corSolucionado = GetColorForStatus("Resolvido");

                // Atualiza a legenda para Pendente
                pnPendente.BackColor = System.Drawing.Color.FromArgb(corPendente.R, corPendente.G, corPendente.B);
                lblPendente.Text = $"Pendente ({pendente?.Value / total ?? 0:P1})";

                // Atualiza a legenda para Em Andamento
                pnEmAndamento.BackColor = System.Drawing.Color.FromArgb(corEmAndamento.R, corEmAndamento.G, corEmAndamento.B);
                lblEmAndamento.Text = $"Em Andamento ({emAndamento?.Value / total ?? 0:P1})";

                // Atualiza a legenda para Resolvido
                pnResolvido.BackColor = System.Drawing.Color.FromArgb(corSolucionado.R, corSolucionado.G, corSolucionado.B);
                lblResolvido.Text = $"Solucionado ({solucionado?.Value / total ?? 0:P1})";
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Erro: Os controles de legenda (ex: 'pnPendente', 'lblPendente') não foram encontrados no formulário. Verifique os nomes no Designer.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao criar legendas customizadas: " + ex.Message);
            }


            plotStatus.Plot.Title("Meus Chamados por Status", size: 14);
            plotStatus.Plot.Legend.IsVisible = false; // Esconde a legenda padrão
            plotStatus.Plot.Axes.SetLimits(-1.5, 1.5, -1.5, 1.5);
            plotStatus.Plot.HideGrid();
            plotStatus.Plot.Axes.Left.IsVisible = false;
            plotStatus.Plot.Axes.Top.IsVisible = false;
            plotStatus.Plot.Axes.Right.IsVisible = false;
            plotStatus.Plot.Axes.Bottom.IsVisible = false;
            plotStatus.Plot.Grid.IsVisible = false;
            plotStatus.Refresh();
        }

        private async Task CarregarGraficoCategoriasAsync()
        {
            List<ChartDataPoint> categoriaData = await _chamadoRepository.ContarPorCategoriaGeralAsync();

            double[] valores = categoriaData.Select(d => d.Value).ToArray();
            string[] labels = categoriaData.Select(d => d.Label).ToArray();

            if (valores.Length == 0)
            {
                plotCategoria.Plot.Title("Sem dados por categoria");
                plotCategoria.Plot.Axes.Frameless();
                plotCategoria.Plot.HideGrid();
                plotCategoria.Refresh();
                return;
            }


            double valorMaximo = valores.Max() == 0 ? 1 : valores.Max();

            var bars = new List<ScottPlot.Bar>();
            for (int i = 0; i < labels.Length; i++)
            {

                bars.Add(new ScottPlot.Bar
                {
                    Position = i,
                    Value = valores[i],
                    FillColor = GetColorForCategoria(labels[i])
                });
            }


            plotCategoria.Plot.Add.Bars(bars);


            plotCategoria.Plot.Layout.Frameless();

            plotCategoria.Plot.Axes.Bottom.FrameLineStyle.IsVisible = false;


            double[] posicoes = Enumerable.Range(0, labels.Length).Select(i => (double)i).ToArray();
            plotCategoria.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(posicoes, labels);
            plotCategoria.Plot.Axes.Bottom.TickLabelStyle.Rotation = 45;
            plotCategoria.Plot.Axes.Bottom.MajorTickStyle.Length = 0; // Esconde os "tracinhos"
                                                                      // ---

            try
            {
                // ... (seu código de legenda 'setLegend' continua igual) ...
                Action<Panel, System.Windows.Forms.Label, string, List<ChartDataPoint>> setLegend =
                (panel, label, categoriaNome, data) =>
                {
                    var cor = GetColorForCategoria(categoriaNome);
                    panel.BackColor = System.Drawing.Color.FromArgb(cor.R, cor.G, cor.B);
                    var item = data.FirstOrDefault(d => d.Label.Equals(categoriaNome, StringComparison.OrdinalIgnoreCase));
                    label.Text = $"{categoriaNome} ({item?.Value ?? 0})";
                };

                setLegend(pnHardware, lblHardware, "Hardware", categoriaData);
                setLegend(pnSoftware, lblSoftware, "Software", categoriaData);
                setLegend(pnSeguranca, lblSeguranca, "Segurança", categoriaData);
                setLegend(pnRede, lblRede, "Rede", categoriaData);
                setLegend(pnServicos, lblServicos, "Serviços", categoriaData);
                setLegend(pnInfra, lblInfra, "Infraestrutura", categoriaData);
                setLegend(pnComunica, lblComunica, "Comunicação", categoriaData);
                setLegend(pnIncidentes, lblIncidentes, "Incidentes", categoriaData);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Erro: Os controles de legenda de Categoria (ex: 'pnHardware', 'lblHardware') não foram encontrados no formulário. Verifique os nomes no Designer.");
            }

            plotCategoria.Plot.Title("Meus Chamados por Categoria", size: 14);

            // --- CORREÇÃO 3: Ajustar Limites X e Y ---
            plotCategoria.Plot.Axes.SetLimitsY(0, valorMaximo * 1.2);
            // Define o limite X (para dar 0.5 de espaço em cada lado da primeira e última barra)
            plotCategoria.Plot.Axes.SetLimitsX(-0.5, labels.Length - 0.5);
            // ---

            // Esconde os outros eixos e a grade
            plotCategoria.Plot.Axes.Left.IsVisible = false;
            plotCategoria.Plot.Axes.Top.IsVisible = false;
            plotCategoria.Plot.Axes.Right.IsVisible = false;
            plotCategoria.Plot.Grid.IsVisible = false;

            plotCategoria.Refresh();
        }


        private ScottPlot.Color GetColorForStatus(string status)
        {
            switch (status.Trim().ToLower())
            {
                case "pendente":
                    return new ScottPlot.Color(239, 83, 80); // Vermelho
                case "em andamento":
                    return new ScottPlot.Color(126, 87, 194); // Roxo
                case "solucionado":
                case "resolvido":
                    return new ScottPlot.Color(102, 187, 106); // Verde
                case "aguardando confirmação":
                    return new ScottPlot.Color(158, 158, 158); // Cinza
                default:
                    return new ScottPlot.Color(33, 150, 243); // Azul Padrão
            }
        }
        private ScottPlot.Color GetColorForCategoria(string categoria)
        {
            switch (categoria.Trim().ToLower())
            {
                case "hardware": return new ScottPlot.Color(255, 152, 0); // Laranja
                case "software": return new ScottPlot.Color(33, 150, 243); // Azul
                case "segurança": return new ScottPlot.Color(239, 83, 80);  // Vermelho
                case "rede": return new ScottPlot.Color(102, 187, 106); // Verde
                case "serviços": return new ScottPlot.Color(126, 87, 194); // Roxo
                case "infraestrutura": return new ScottPlot.Color(0, 150, 136); // Teal
                case "comunicação": return new ScottPlot.Color(255, 193, 7); // Amber
                case "incidentes": return new ScottPlot.Color(96, 125, 139); // Azul Acinzentado
                default: return new ScottPlot.Color(158, 158, 158); // Cinza
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
                    LinearGradientMode.Vertical); // Exemplo com gradiente horizontal
            g.FillRectangle(gradientePanel, panel1.ClientRectangle);
        }

        private void HomeAdmin_Paint(object sender, PaintEventArgs e)
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

        private void btn_Relatorio_Click(object sender, EventArgs e)
        {
            var telaRealatorio = new Relatorio();
            telaRealatorio.Show();
            this.Hide();
        }

        private void btn_Usuario_Click(object sender, EventArgs e)
        {
            var telaUsuario = new GerenciarUsuarios();
            telaUsuario.Show();
            this.Hide();

        }

        private void btn_Chamado_Click(object sender, EventArgs e)
        {
            
            var telaChamado = new Responder_Chamado();
            telaChamado.Show();
            this.Hide();
            
            
        }

        private void lbSair_Click(object sender, EventArgs e)
        {
            FormHelper.Sair(this, timerSessao);
        }

        

        private void timerSessao_Tick(object sender, EventArgs e)
        {
            // "Valida o Token" a cada minuto
            if (!SessaoUsuario.SessaoEstaValida())
            {

                timerSessao.Stop();

                MessageBox.Show("Sua sessão expirou por segurança. Por favor, faça o login novamente.",
                                "Sessão Expirada",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                // "Revoga" o token (garante que está limpo)
                SessaoUsuario.EncerrarSessao();

                // Envia para o Login
                var loginForm = new Login();
                loginForm.Show();
                this.Close();
            }
        }

        private void lblMconta_Click(object sender, EventArgs e)
        {
            var visualizarUsuario = new Visualizar_Usuario(SessaoUsuario.IdUsuario);
            visualizarUsuario.ShowDialog();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            var verChamado = new VisualizarChamado();
            verChamado.ShowDialog();
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            var faq = new Faq();
            faq.ShowDialog();
        }
    }
}
