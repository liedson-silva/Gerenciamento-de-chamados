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
    public partial class ContinuaçaoAbertura : Form
    {
        private AberturaChamados aberturaChamados;

        // Recebe a tela de abertura de chamados já preenchida
        public ContinuaçaoAbertura(AberturaChamados abertura)
        {
            InitializeComponent();
            aberturaChamados = abertura;
        }

        private void btnConcluirCH_Click(object sender, EventArgs e)
        {
            string TituloChamado = aberturaChamados.txtTituloChamado.Text.Trim();
            string DescricaoChamado = aberturaChamados.txtDescriçãoCh.Text.Trim();
            string PessoasAfetadas = cBoxPessoasAfeta.Text.Trim();
            string SetorAfetado = cBoxImpedTrab.Text;
            string Historicoacont = cBoxAcontAntes.Text;
            string CategoriaChamado = aberturaChamados.cboxCtgChamado.Text;
            byte[] AnexarArquivo = aberturaChamados.arquivoAnexado;



            string connectionString = "Server=fatalsystemsrv1.database.windows.net;Database=DbaFatal-System;User Id=fatalsystem;Password=F1234567890m@;";

            int idUsuario = Funcoes.SessaoUsuario.IdUsuario;
            string sql = @"INSERT INTO Chamado 
                           (Titulo, PrioridadeChamado, Descricao, DataChamado, StatusChamado, Categoria, FK_IdUsuario) 
                           OUTPUT INSERTED.IdChamado
                           VALUES (@Titulo, @PrioridadeChamado, @Descricao, @DataChamado, @StatusChamado, @Categoria, @FK_IdUsuario)";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();

                    int idChamado;
                    using (SqlCommand cmd = new SqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Titulo", TituloChamado);
                        cmd.Parameters.AddWithValue("@PrioridadeChamado", "análise");
                        cmd.Parameters.AddWithValue("@Descricao", DescricaoChamado);
                        cmd.Parameters.AddWithValue("@DataChamado", DateTime.Now);
                        cmd.Parameters.AddWithValue("@StatusChamado", "Aberto");
                        cmd.Parameters.AddWithValue("@Categoria", CategoriaChamado);
                        cmd.Parameters.AddWithValue("@FK_IdUsuario", idUsuario);

                        idChamado = (int)cmd.ExecuteScalar();
                    }

                    if (AnexarArquivo != null && AnexarArquivo.Length > 0)
                    {
                        string sqlArquivo = @"INSERT INTO Arquivo 
                                              (TipoArquivo, NomeArquivo, Arquivo, FK_IdChamado)
                                              VALUES (@TipoArquivo, @NomeArquivo, @Arquivo, @FK_IdChamado)";
                        using (SqlCommand cmdArq = new SqlCommand(sqlArquivo, conexao))
                        {
                            cmdArq.Parameters.AddWithValue("@TipoArquivo", "imagem/png");
                            cmdArq.Parameters.AddWithValue("@NomeArquivo", "anexo.png");
                            cmdArq.Parameters.AddWithValue("@Arquivo", AnexarArquivo);
                            cmdArq.Parameters.AddWithValue("@FK_IdChamado", idChamado);
                            cmdArq.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Chamado aberto com sucesso! Número do chamado: " + idChamado);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao abrir chamado: " + ex.Message);
                }
            }
            var telaFimChamado = new FimChamado();
            telaFimChamado.Show();
            this.Visible = false;
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
    }
}
