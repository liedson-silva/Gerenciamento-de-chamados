using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Appparalogin
{
    public partial class GerenciarChamado : Form
    {
        public GerenciarChamado()
        {
            InitializeComponent();
        }

        private void btnCriarChamado_Click(object sender, EventArgs e)
        {
            var criarchamado = new AberturaChamados();
            criarchamado.Show();
            this.Hide(); // Oculta o form atual
        }
    }
}
