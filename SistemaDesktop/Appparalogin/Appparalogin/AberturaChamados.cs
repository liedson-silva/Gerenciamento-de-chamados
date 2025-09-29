using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Appparalogin
{
    public partial class AberturaChamados : Form
    {
        public AberturaChamados()
        {
            InitializeComponent();
        }
        Funcoes funcoes = new Funcoes();
        public void btnContinuar_Click(object sender, EventArgs e)
        { 

            var continuaçaoabertura = new ContinuaçaoAbertura();
            continuaçaoabertura.Show();
            this.Hide(); // Oculta o form atual
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

        public byte[] arquivoAnexado;
        private void btnAnexArq_Click(object sender, EventArgs e)
        {
            arquivoAnexado = funcoes.SelecionarArquivoEConverter();

            if (arquivoAnexado != null)
            {
               btnAnexArq.Image = funcoes.ByteArrayToImage(arquivoAnexado);
            }
            else
            {
                MessageBox.Show("Nenhum arquivo foi selecionado.");
            }

        }

    }
}

    


