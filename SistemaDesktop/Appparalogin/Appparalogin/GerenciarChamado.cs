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
    public partial class GerenciarChamado : Form
    {
        public GerenciarChamado()
        {
            InitializeComponent();
        }

        private void btnCriarChamado_Click(object sender, EventArgs e)
        {
            var aberturaChamadosForm = new AberturaChamados();
            aberturaChamadosForm.Show();
            this.Hide();
        }

        private void btnVisualizarCh_Click(object sender, EventArgs e)
        {
            try
            {
                var visualizarChamados = new VisualizarChamado();
                visualizarChamados.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir janela: " + ex.Message);
            }
        }
    }
}
