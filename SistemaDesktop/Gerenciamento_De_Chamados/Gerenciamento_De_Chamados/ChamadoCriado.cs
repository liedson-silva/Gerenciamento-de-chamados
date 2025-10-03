using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados
{
    public partial class ChamadoCriado : Form
    {
        public ChamadoCriado()
        {
            InitializeComponent();
        }

        private void gbxChamadoCriado_Paint(object sender, PaintEventArgs e)
        {
            // Pega a cor que você quer (hexadecimal)
            Color corFundo = ColorTranslator.FromHtml("#E3FFE5");

            // Preenche o fundo inteiro do GroupBox
            using (SolidBrush brush = new SolidBrush(corFundo))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            // Desenha o texto do título do GroupBox normalmente
            GroupBox box = sender as GroupBox;
            if (box != null)
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    box.Text,
                    box.Font,
                    new Point(10, 1),  // posição do texto
                    box.ForeColor
                );
            }
        }

    }
}
