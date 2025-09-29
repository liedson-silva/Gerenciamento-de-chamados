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
            if (!Funcoes.SessaoUsuario.UsuarioIdentificado())
            {
                MessageBox.Show(
                    "Usuário não identificado! Faça login novamente.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            AbrirTelaAberturaChamado();
        }

        /// <summary>
        /// Abre o formulário de abertura de chamados e oculta o formulário atual.
        /// </summary>
        private void AbrirTelaAberturaChamado()
        {
            var aberturaChamadosForm = new AberturaChamados();
            aberturaChamadosForm.Show();
            this.Hide();
        }
    }
}
