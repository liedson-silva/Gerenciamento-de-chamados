using Gerenciamento_De_Chamados.Helpers;
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
        public HomeAdmin()
        {
            InitializeComponent();
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
            this.Close();
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
            FormHelper.Sair(this);
        }

        private void HomeAdmin_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessaoUsuario.Nome) && !string.IsNullOrEmpty(SessaoUsuario.FuncaoUsuario))
            {
                Home_Tecnico.Text = $"Olá, {SessaoUsuario.Nome} ({SessaoUsuario.FuncaoUsuario})";
            }
            else
            {
                Home_Tecnico.Text = "Usuário não identificado";
            }
        }
    }
}
