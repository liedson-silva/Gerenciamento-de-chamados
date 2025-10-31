using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gerenciamento_De_Chamados.Helpers;

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
    }
}
