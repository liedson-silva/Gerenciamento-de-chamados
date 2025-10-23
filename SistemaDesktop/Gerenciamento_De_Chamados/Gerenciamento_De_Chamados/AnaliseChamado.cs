using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing; 
using System.Drawing.Drawing2D; 
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados
{
    public partial class AnaliseChamado : Form
    {
        private int _chamadoId;
        string connectionString = "Server=fatalsystemsrv1.database.windows.net;Database=DbaFatal-System;User Id=fatalsystem;Password=F1234567890m@;"; // Sua string de conexão


        public AnaliseChamado(int chamadoId)
        {
            InitializeComponent();
            _chamadoId = chamadoId; 
            this.Load += AnaliseChamado_Load; 
        }

        // Método que será chamado quando o formulário carregar
        private void AnaliseChamado_Load(object sender, EventArgs e)
        {
            CarregarDadosChamado();
            ConfigurarBotoes(); 
        }

        private void CarregarDadosChamado()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    
                    string sql = @"SELECT Titulo, Descricao, Categoria, StatusChamado,
                                          PrioridadeSugeridaIA, ProblemaSugeridoIA, SolucaoSugeridaIA
                                   FROM Chamado
                                   WHERE IdChamado = @IdChamado";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdChamado", _chamadoId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtIdentificacaoProb.Text = reader["ProblemaSugeridoIA"] != DBNull.Value ? reader["ProblemaSugeridoIA"].ToString() : "N/A";
                                txtSolProp.Text = reader["SolucaoSugeridaIA"] != DBNull.Value ? reader["SolucaoSugeridaIA"].ToString() : "N/A";

                                string prioridadeScore = reader["PrioridadeSugeridaIA"] != DBNull.Value ? reader["PrioridadeSugeridaIA"].ToString() : "Baixa"; // Padrão Baixa se nulo
                                // Tenta selecionar o item na ComboBox
                                int index = cboxPrioridProp.FindStringExact(prioridadeScore);
                                if (index != -1)
                                {
                                    cboxPrioridProp.SelectedIndex = index;
                                }
                                else
                                {
                                    cboxPrioridProp.Text = prioridadeScore; 
                                }

                                
                                string statusAtual = reader["StatusChamado"].ToString();
                                this.Tag = statusAtual; 
                            }
                            else
                            {
                                MessageBox.Show("Chamado não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar dados do chamado: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void ConfigurarBotoes()
        {
            string statusAtual = this.Tag as string;

            // Assumindo que os botões se chamam btnSalvarAnalise e btnResolverChamado
            if (statusAtual == "Pendente")
            {
                // Habilita salvar análise, desabilita resolver
                // btnSalvarAnalise.Enabled = true;
                // btnResolverChamado.Enabled = false;
                // Adicione também um TextBox grande chamado txtSolucaoFinal para a solução
                // txtSolucaoFinal.Enabled = false;
            }
            else if (statusAtual == "Em Andamento")
            {
                // Desabilita salvar análise inicial, habilita resolver
                // btnSalvarAnalise.Enabled = false;
                // btnResolverChamado.Enabled = true;
                // txtSolucaoFinal.Enabled = true;
            }
            else // Resolvido ou outro status
            {
                // Desabilita ambos, talvez coloque os campos como ReadOnly
                // btnSalvarAnalise.Enabled = false;
                // btnResolverChamado.Enabled = false;
                // txtIdentificacaoProb.ReadOnly = true;
                // txtSolProp.ReadOnly = true;
                // cboxPrioridProp.Enabled = false;
                // txtSolucaoFinal.ReadOnly = true;
            }
            // Adicione um botão btnCancelar
            // btnCancelar.Click += (s, ev) => this.Close();
        }

        // --- MÉTODOS DOS BOTÕES (Ainda não implementados) ---

        // private void btnSalvarAnalise_Click(object sender, EventArgs e)
        // {
        // Lógica para UPDATE Chamado SET Status='Em Andamento', PrioridadeChamado = cboxPrioridProp.Text
        // Lógica para INSERT INTO Historico (FK_IdChamado, DataSolucao, Solucao, TipoAcao) VALUES (...)
        // Fechar ou atualizar a tela
        // }

        // private void btnResolverChamado_Click(object sender, EventArgs e)
        // {
        // string solucaoFinal = txtSolucaoFinal.Text; // Pega a solução do TextBox apropriado
        // Lógica para UPDATE Chamado SET Status='Resolvido'
        // Lógica para INSERT INTO Historico (FK_IdChamado, DataSolucao, Solucao, TipoAcao) VALUES (..., solucaoFinal, 'Resolução Final')
        // Fechar a tela
        // }

        // Adicione o código de pintura do painel se você tiver um panel1 como nas outras telas
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color corInicioPanel = Color.White;
            Color corFimPanel = ColorTranslator.FromHtml("#232325");
            LinearGradientBrush gradientePanel = new LinearGradientBrush(
                     ClientRectangle, // Use ClientRectangle se for o fundo do Form, ou panel1.ClientRectangle se for um painel
                    corInicioPanel,
                    corFimPanel,
                    LinearGradientMode.Vertical);
            g.FillRectangle(gradientePanel, ClientRectangle); // Idem
        }

    }
}