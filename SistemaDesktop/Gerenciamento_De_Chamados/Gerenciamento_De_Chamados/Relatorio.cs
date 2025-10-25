using ScottPlot;
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
using System.Windows.Forms.DataVisualization.Charting;


namespace Gerenciamento_De_Chamados
{
    public partial class Relatorio : Form
    {
        public Relatorio()
        {
            InitializeComponent();
            this.Load += Relatorio_Load;
        }

        private void Relatorio_Load(object sender, EventArgs e)
        {

            // Listas dinâmicas para receber os dados do banco
            List<double> valoresDB = new List<double>();
            List<string> labelsDB = new List<string>();


            string connectionString = "Server=fatalsystemsrv1.database.windows.net;Database=DbaFatal-System;User Id=fatalsystem;Password=F1234567890m@;";

            string query = @"
        SELECT StatusChamado, COUNT(IdChamado) 
        FROM Chamado 
        WHERE StatusChamado IN ('Pendente', 'Em Andamento', 'Resolvido') 
        GROUP BY StatusChamado";


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string status = reader.GetString(0);
                                int contagem = reader.GetInt32(1);

                                labelsDB.Add(status);
                                valoresDB.Add(contagem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar ao banco de dados: \n" + ex.Message);
                return; 
            }

            if (valoresDB.Count == 0)
            {
                formPlotVisaoGeral.Plot.Title("Sem dados para exibir");
                formPlotVisaoGeral.Plot.Axes.Frameless();
                formPlotVisaoGeral.Plot.HideGrid();
                formPlotVisaoGeral.Refresh();
                return;
            }

            var piePlot = formPlotVisaoGeral.Plot.Add.Pie(valoresDB.ToArray());


            double total = valoresDB.Sum(); 

            for (int i = 0; i < labelsDB.Count; i++)
            {
                string status = labelsDB[i];
                double porcentagem = (valoresDB[i] / total);

               
                piePlot.Slices[i].Label = $"{status} ({porcentagem:P1})";

               
                if (status.Equals("Pendente", StringComparison.OrdinalIgnoreCase))
                {
                    piePlot.Slices[i].Fill.Color = new ScottPlot.Color(33, 150, 243);  
                }
                else if (status.Equals("Em Andamento", StringComparison.OrdinalIgnoreCase))
                {
                    piePlot.Slices[i].Fill.Color = new ScottPlot.Color(255, 193, 7); 
                }
                else if (status.Equals("Resolvido", StringComparison.OrdinalIgnoreCase)) 
                {
                    piePlot.Slices[i].Fill.Color = new ScottPlot.Color(76, 175, 80); 
                }
                else
                {
                  
                    piePlot.Slices[i].Fill.Color = ScottPlot.Colors.Gray;
                }
            }

          
            piePlot.SliceLabelDistance = 0.6; 

           
            formPlotVisaoGeral.Plot.Title("Visão Overal de Chamados");

            
            formPlotVisaoGeral.Plot.Axes.Frameless();
            formPlotVisaoGeral.Plot.HideGrid();

           
            formPlotVisaoGeral.Refresh();
        }

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

        private void panelEsquerdo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelSidebar_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            System.Drawing.Color corInicioPanel = System.Drawing.Color.White;
            System.Drawing.Color corFimPanel = ColorTranslator.FromHtml("#232325");
            LinearGradientBrush gradientePanel = new LinearGradientBrush(
                     panel1.ClientRectangle,
                    corInicioPanel,
                    corFimPanel,
                    LinearGradientMode.Vertical);
            g.FillRectangle(gradientePanel, panel1.ClientRectangle);
        }
    }
}
