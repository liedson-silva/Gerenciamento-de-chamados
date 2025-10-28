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
    public partial class HomeTecnico : Form
    {
        public HomeTecnico()
        {
            InitializeComponent();
            this.Load += HomeTecnico_Load;
        }



        private void btnResponder_chamado_Click(object sender, EventArgs e)
        {
            var reschamado = new Responder_Chamado();
            reschamado.Show();
            this.Hide();
        }

        private void HomeTecnico_Paint(object sender, PaintEventArgs e)
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

        private void btn_AbrirChamado_Click(object sender, EventArgs e)
        {
            var abrir = new AberturaChamados();
            abrir.Show();
            this.Hide();
        }
        private void HomeTecnico_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Funcoes.SessaoUsuario.Nome) && !string.IsNullOrEmpty(Funcoes.SessaoUsuario.FuncaoUsuario))
            {
                Home_Tecnico.Text = $"Olá, {Funcoes.SessaoUsuario.Nome} ({Funcoes.SessaoUsuario.FuncaoUsuario})";
            }
            else
            {
                Home_Tecnico.Text = "Usuário não identificado";
            }
        }

        private void lbSair_Click(object sender, EventArgs e)
        {

        }
    }
}
