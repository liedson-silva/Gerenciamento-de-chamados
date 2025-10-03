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
    public partial class Home: Form
    {

        public Home()
        {
            InitializeComponent();
        }

        private void btnGerChamado_Click(object sender, EventArgs e)
        {
            var gerchamado = new GerenciarChamado();
            gerchamado.Show();
            this.Hide(); // Oculta o form atual
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var gerusuario = new GerenciarUsuarios();
            gerusuario.Show(); 
            this.Hide(); 
        }
    }
}
