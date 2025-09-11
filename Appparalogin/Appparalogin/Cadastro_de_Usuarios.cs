using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Appparalogin
{
    public partial class Cadastro_de_Usuarios : Form
    {
        public Cadastro_de_Usuarios()
        {
            InitializeComponent();
        }

        private void btnCadastroAdd_Click(object sender, EventArgs e)
        {

            string usuario = txtCadastroNome.Text.Trim();
            string email = txtCadastroEmail.Text.Trim();
            string senhaDigitada = txtCadastroSenha.Text.Trim();
            string hashSenha = SenhaHelper.GerarHashSenha(senhaDigitada);

            string connectionString = "Server=fatalsystemsrv1.database.windows.net;Database=DbaFatal-System;User Id=frederico;Password=Fred11376@;";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    string sql = "INSERT INTO Usuario (Login, Senha, Email) VALUES (@login, @senha, @email)";
                    using (SqlCommand cmd = new SqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@login", usuario);
                        cmd.Parameters.AddWithValue("@senha", hashSenha);
                        cmd.Parameters.AddWithValue("@email", email);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Usuário cadastrado com sucesso!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao cadastrar: " + ex.Message);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblCadastroSenha_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblCadastroTel_Click(object sender, EventArgs e)
        {

        }

        private void txtCadastroEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
