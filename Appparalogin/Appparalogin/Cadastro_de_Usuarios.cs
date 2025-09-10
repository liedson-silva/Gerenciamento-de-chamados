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
            // Plano detalhado (pseudocódigo):
            // 1. Verificar se a string de conexão contém "User Id" e "Password" para autenticação SQL Server.
            // 2. Corrigir a string de conexão removendo "Trusted Connection" e adicionando "User Id" e "Password".
            // 3. Testar a conexão para garantir que o erro de usuário não ocorre mais.

            // Substitua a linha da string de conexão por esta versão corrigida:
            SqlConnection conn = new SqlConnection("Data Source=fatalsystemsrv1.database.windows.net;Initial Catalog=DbaFatal-System;User Id=frederico;Password=Fred11376@;Encrypt=True");

            //Cria string de inserção SQL
            string sql = "INSERT INTO CADASTRO(Nome, Email, Telefone, SenhaHash) VALUES(@Nome, @Email, @Telefone, @SenhaHash)";

            try
            {
                //cria um objeto do tipo comando passando como parametro a string sql e conn
                SqlCommand c = new SqlCommand(sql, conn);

                //Insere os dados da textBox no comando sql
                c.Parameters.Add(new SqlParameter("@Nome", this.txtCadastroNome.Text));
                c.Parameters.Add(new SqlParameter("@Email", this.txtCadastroEmail.Text));
                c.Parameters.Add(new SqlParameter("@Telefone", this.txtCadastroTel.Text));
                c.Parameters.Add(new SqlParameter("@SenhaHash", Encoding.UTF8.GetBytes(this.txtCadastroSenha.Text)));

                //Abrimos conexao com banco de dados
                conn.Open();


                //Executa o comando sql no banco de dados
                c.ExecuteNonQuery();


                //Fechamos a conexão
                conn.Close();


                MessageBox.Show("Dados inseridos com sucesso!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro de banco de dados: " + ex.Message);
            }
        }

        private void txtCadastroNome_TextChanged(object sender, EventArgs e)
        {

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
    }
}
