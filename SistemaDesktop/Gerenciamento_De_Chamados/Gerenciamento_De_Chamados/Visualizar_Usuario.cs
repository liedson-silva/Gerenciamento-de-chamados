using Gerenciamento_De_Chamados.Helpers;
using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados
{

    public partial class Visualizar_Usuario : Form
    {
        private int usuarioId;

        private readonly IUsuarioRepository _usuarioRepository;

        public Visualizar_Usuario()
        {
            InitializeComponent();
            _usuarioRepository = new UsuarioRepository();
        }

        public Visualizar_Usuario(int idDoUsuario) : this()
        {
            this.usuarioId = idDoUsuario;

            this.Load += Visualizar_Usuario_Load;
        }

       
        private async void Visualizar_Usuario_Load(object sender, EventArgs e)
        {
            await CarregarDadosDoUsuarioAsync();
        }
        private async Task CarregarDadosDoUsuarioAsync()
        {
            try
            {
                //Busca o usuário no repositório
                Usuario usuario = await _usuarioRepository.BuscarPorIdAsync(this.usuarioId);

                if (usuario != null)
                {
                    // Preenche os campos do formulário com os dados do objeto
                    txtNome.Text = usuario.Nome;
                    cmbFuncao.SelectedItem = usuario.FuncaoUsuario;
                    cmbSexo.SelectedItem = usuario.Sexo; 
                    txtSetor.Text = usuario.Setor; 
                    txtEmail.Text = usuario.Email;
                    txtLogin.Text = usuario.Login;

                    
                    if (usuario.DataDeNascimento > DateTime.MinValue)
                    {
                        dtpDataNascimento.Value = usuario.DataDeNascimento;
                    }
                    else
                    {
                        dtpDataNascimento.Value = DateTime.Today; // Ou um valor padrão
                    }
                }
                else
                {
                    MessageBox.Show("Usuário não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados do usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void lbl_Inicio_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void PctBox_Logo_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void lbSair_Click(object sender, EventArgs e)
        {
            FormHelper.Sair(this);
        }

        private void lblConta_Click(object sender, EventArgs e)
        {
            var visualizarUsuario = new Visualizar_Usuario(SessaoUsuario.IdUsuario);
            visualizarUsuario.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            var verChamado = new VisualizarChamado();
            this.Hide();
            verChamado.ShowDialog();
            this.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            FormHelper.FAQ(this);
        }

        private void Visualizar_Usuario_Paint(object sender, PaintEventArgs e)
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
                             LinearGradientMode.Vertical);
            g.FillRectangle(gradientePanel, panel1.ClientRectangle);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }

