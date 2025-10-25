using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScottPlot;

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
            // 1. Seus dados
            double[] valores = { 4, 7, 2 };
            string[] labelsX = { "Pendentes", "Em Andamento", "Resolvidos" };

            // 2. Adiciona o gráfico de Pizza
            var piePlot = formPlotVisaoGeral.Plot.Add.Pie(valores);

            // 3. Define as cores de cada fatia
            piePlot.Slices[0].Fill.Color = new ScottPlot.Color(239, 83, 80);   // Vermelho
            piePlot.Slices[1].Fill.Color = new ScottPlot.Color(255, 167, 38); // Laranja
            piePlot.Slices[2].Fill.Color = new ScottPlot.Color(102, 187, 106); // Verde

            // 4. CORREÇÃO: Vamos criar os rótulos (Label + Porcentagem) manualmente
            //    Primeiro, calculamos o total
            double total = valores.Sum();

            //    Agora, atribuímos o texto a cada fatia
            for (int i = 0; i < labelsX.Length; i++)
            {
                double porcentagem = (valores[i] / total);

                // A propriedade .Label é a string que aparece na fatia
                piePlot.Slices[i].Label = $"{labelsX[i]} ({porcentagem:P1})"; // ex: "Pendentes (30.8%)"
            }

            // 5. CORREÇÃO: Para mostrar os rótulos, a propriedade é 'ShowSliceLabels'
            //piePlot.SliceLabelStyle.IsVisible = true;
            piePlot.SliceLabelDistance = 0.6; // Ajuste a distância conforme necessário

            // 6. Define o Título
            formPlotVisaoGeral.Plot.Title("Visão Overal de Chamados");

            // 7. CORREÇÃO: O jeito certo de remover os eixos e a moldura
            //    Não existe 'Hide()' ou 'Frame.Visible'.
            //    O método correto é 'Frameless()'.
            formPlotVisaoGeral.Plot.Axes.Frameless(); // Isso remove eixos, ticks e a moldura
            formPlotVisaoGeral.Plot.HideGrid(); // Isso remove o grid de fundo

            // 8. Atualiza o gráfico
            formPlotVisaoGeral.Refresh();
        }
    }
}
