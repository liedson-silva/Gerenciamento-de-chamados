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
    public partial class Faq : Form
    {
        public Faq()
        {
            InitializeComponent();
        }

        private void lblSeta1_Click(object sender, EventArgs e)
        {
            
            panelConteudo1.Visible = !panelConteudo1.Visible;

           
            if (panelConteudo1.Visible)
            {
                lblSeta1.Text = "▲";
            }
            else
            {
                lblSeta1.Text = "▼";
            }
        }

        private void Faq_Paint(object sender, PaintEventArgs e)
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

        private void lblSeta2_Click(object sender, EventArgs e)
        {
            panelConteudo2.Visible = !panelConteudo2.Visible;


            if (panelConteudo2.Visible)
            {
                lblSeta2.Text = "▲";
            }
            else
            {
                lblSeta2.Text = "▼";
            }
        }

        private void lblSeta3_Click(object sender, EventArgs e)
        {
            panelConteudo3.Visible = !panelConteudo3.Visible;


            if (panelConteudo3.Visible)
            {
                lblSeta3.Text = "▲";
            }
            else
            {
                lblSeta3.Text = "▼";
            }
        }

        private void lblSeta4_Click(object sender, EventArgs e)
        {
            panelConteudo4.Visible = !panelConteudo4.Visible;


            if (panelConteudo4.Visible)
            {
                lblSeta4.Text = "▲";
            }
            else
            {
                lblSeta4.Text = "▼";
            }
        }

        private void lbSair_Click(object sender, EventArgs e)
        {
            Funcoes.Sair(this);
        }
    }
}
