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
    public partial class HomeUsuario : Form
    {
        public HomeUsuario()
        {
            InitializeComponent();
            this.Load += HomeUsuario_Load;
        }
        private void HomeUsuario_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Funcoes.SessaoUsuario.Nome))
                lbl_NomeUser.Text = ($"Bem vindo {Funcoes.SessaoUsuario.Nome}");
            else
                lbl_NomeUser.Text = "Usuário não identificado";
        }

        private void btn_AbrirChamado_Click(object sender, EventArgs e)
        {
            var aberturaChamadosForm = new AberturaChamados();
            aberturaChamadosForm.Show();
            this.Hide();
        }
    }
}
