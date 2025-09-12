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
            string CadastroRg = txtCadastroRG.Text.Trim();
            string CadastroCPF = txtCadastroCpf.Text.Trim();
            string CadastroNome = txtCadastroNome.Text.Trim();
            string CadastroUsuario = txtCadastroLogin.Text.Trim();
            string CadastroEmail = txtCadastroEmail.Text.Trim();
            string CadastroSenhaDigitada = txtCadastroSenha.Text.Trim();
            string CadastroFuncaoUsuario = txtCadastroFuncao.Text.Trim();
            string CadastroSexo = comboBoxCadastroSexo.Text;
            string CadastroSetor = cBoxCadSetor.Text;
            DateTime CadastroDataDeNascimento = dtpCadDN.Value;
            string hashSenha = SenhaHelper.GerarHashSenha(CadastroSenhaDigitada);

            string connectionString = "Server=fatalsystemsrv1.database.windows.net;Database=DbaFatal-System;User Id=fatalsystem;Password=F1234567890m@;";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    string sql = "INSERT INTO Usuario (Login, Nome, CPF, RG, FuncaoUsuario, Sexo, Setor, DataDeNascimento, Senha, Email)" +
                        " VALUES (@login, @Nome, @CPF, @RG, @FuncaoUsuario, @Sexo, @Setor, @DataDeNascimento, @senha, @email)";
                    using (SqlCommand cmd = new SqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Nome", CadastroNome);
                        cmd.Parameters.AddWithValue("@CPF", CadastroCPF);
                        cmd.Parameters.AddWithValue("@RG", CadastroRg);
                        cmd.Parameters.AddWithValue("@FuncaoUsuario", CadastroFuncaoUsuario);
                        cmd.Parameters.AddWithValue("@Sexo", CadastroSexo);
                        cmd.Parameters.AddWithValue("@Setor", CadastroSetor);
                        cmd.Parameters.AddWithValue("@DataDeNascimento", CadastroDataDeNascimento);
                        cmd.Parameters.AddWithValue("@login", CadastroUsuario);
                        cmd.Parameters.AddWithValue("@senha", hashSenha);
                        cmd.Parameters.AddWithValue("@email", CadastroEmail);

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




    }
}