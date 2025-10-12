using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Gerenciamento_De_Chamados
{
    public partial class ChamadoCriado : Form
    {
        private int chamadoId;
        string connectionString = "Server=fatalsystemsrv1.database.windows.net;Database=DbaFatal-System;User Id=fatalsystem;Password=F1234567890m@;";
        public ChamadoCriado(int idDoChamado)
        {
            InitializeComponent();
            this.Load += ChamadoCriado_Load;
            this.chamadoId = idDoChamado;
        }

        private void gbxChamadoCriado_Paint(object sender, PaintEventArgs e)
        {
            // Pega a cor que você quer (hexadecimal)
            Color corFundo = ColorTranslator.FromHtml("#E3FFE5");

            // Preenche o fundo inteiro do GroupBox
            using (SolidBrush brush = new SolidBrush(corFundo))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            // Desenha o texto do título do GroupBox normalmente
            GroupBox box = sender as GroupBox;
            if (box != null)
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    box.Text,
                    box.Font,
                    new Point(10, 1),  // posição do texto
                    box.ForeColor
                );
            }
        }
        private void ChamadoCriado_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Funcoes.SessaoUsuario.Nome))
                lbl_NomeUser.Text = ($"Bem vindo {Funcoes.SessaoUsuario.Nome}");

            else
                lbl_NomeUser.Text = "Usuário não identificado";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT DataChamado, Titulo, Descricao, Categoria FROM Chamado WHERE IdChamado = @IdChamado";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdChamado", this.chamadoId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime dataCriacao = Convert.ToDateTime(reader["DataChamado"]);
                                string titulo = reader["Titulo"].ToString();
                                string descricao = reader["Descricao"].ToString();
                                string categoria = reader["Categoria"].ToString();

                                StringBuilder resumo = new StringBuilder();
                                resumo.AppendLine($"Chamado criado em: {dataCriacao:dd/MM/yyyy HH:mm:ss}");
                                resumo.AppendLine($"Usuario: {Funcoes.SessaoUsuario.Nome}");
                                resumo.AppendLine("------------------------------------------------");
                                resumo.AppendLine($"Título: {titulo}");
                                resumo.AppendLine($"Categoria: {categoria}");
                                resumo.AppendLine();
                                resumo.AppendLine("---Descrição---");
                                resumo.AppendLine(descricao);

                                txtResumoChamado.Text = resumo.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar detalhes do chamado: " + ex.Message);
            }
        }

        private void gbxTitulo_Paint(object sender, PaintEventArgs e)
        {
            // Pega a cor que você quer (hexadecimal)
            Color corFundo = ColorTranslator.FromHtml("#E3FFE5");

            // Preenche o fundo inteiro do GroupBox
            using (SolidBrush brush = new SolidBrush(corFundo))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            // Desenha o texto do título do GroupBox normalmente
            GroupBox box = sender as GroupBox;
            if (box != null)
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    box.Text,
                    box.Font,
                    new Point(10, 1),  // posição do texto
                    box.ForeColor
                );
            }
        
    }
    }
}
