using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using Gerenciamento_De_Chamados.Helpers; 
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text; 
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciamento_De_Chamados
{
    public partial class AnaliseChamado : Form
    {
        private int _chamadoId;
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

       
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IHistoricoRepository _historicoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public AnaliseChamado(int chamadoId)
        {
            InitializeComponent();
            _chamadoId = chamadoId;

          
            _chamadoRepository = new ChamadoRepository();
            _historicoRepository = new HistoricoRepository();
            _usuarioRepository = new UsuarioRepository();

          
            this.Load += AnaliseChamado_Load;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
        }

        private async void AnaliseChamado_Load(object sender, EventArgs e)
        {
            await CarregarDadosChamadoAsync();
           
        }

 
        private async Task CarregarDadosChamadoAsync()
        {
            try
            {
                // Busca o chamado principal
                Chamado chamado = await _chamadoRepository.BuscarPorIdAsync(_chamadoId);
                if (chamado == null)
                {
                    MessageBox.Show("Chamado não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Busca o usuário que criou o chamado
                Usuario usuarioCriador = await _usuarioRepository.BuscarPorIdAsync(chamado.FK_IdUsuario);
                string nomeUsuario = usuarioCriador?.Nome ?? "Usuário Desconhecido";

                // Monta o resumo para 'txtDescricao' (conforme solicitado)
                StringBuilder resumo = new StringBuilder();
                resumo.AppendLine($"ID do Chamado: {chamado.IdChamado}");
                resumo.AppendLine($"Criado por: {nomeUsuario}");
                resumo.AppendLine($"Data/Hora: {chamado.DataChamado:dd/MM/yyyy 'às' HH:mm:ss}");
                resumo.AppendLine($"Categoria: {chamado.Categoria}");
                resumo.AppendLine("---");
                resumo.AppendLine($"Título: {chamado.Titulo}");
                resumo.AppendLine("---");
                resumo.AppendLine($"Descrição do Problema:");
                resumo.AppendLine(chamado.Descricao);
                resumo.AppendLine("---");
                resumo.AppendLine($"Prioridade Sugerida (IA): {chamado.PrioridadeSugeridaIA}"); 

                txtDescricao.Text = resumo.ToString();

                // Preenche a solução e define o estado dos controles
                string statusAtual = chamado.StatusChamado;
                this.Tag = statusAtual; 

                if (statusAtual == "Resolvido")
                {
                    // Se já estiver resolvido, busca a solução final que foi aplicada
                    string solucao = await _historicoRepository.BuscarUltimaSolucaoAsync(_chamadoId);
                    txtSolucao.Text = solucao ?? "Solução não registrada no histórico.";

                    // Trava os campos
                    txtDescricao.ReadOnly = true;
                    txtSolucao.ReadOnly = true;
                    btnEnviar.Enabled = false; // Desabilita o botão "Enviar"
                    btnEnviar.Text = "Resolvido";
                }
                else
                {
                    // Se está "Pendente", pré-preenche com a sugestão da IA (conforme solicitado)
                    txtSolucao.Text = chamado.SolucaoSugeridaIA;

                    
                    txtDescricao.ReadOnly = true; 
                    txtSolucao.ReadOnly = false; 
                    btnEnviar.Enabled = true; 
                    btnEnviar.Text = "Enviar Resposta";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados do chamado: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

   
        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            string solucaoFinal = txtSolucao.Text;
            string novoStatus = "Resolvido";
            DateTime dataAgora = ObterHoraBrasilia();

            if (string.IsNullOrWhiteSpace(solucaoFinal))
            {
                MessageBox.Show("Por favor, descreva a solução aplicada antes de resolver o chamado.", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Deseja aplicar esta solução e marcar o chamado como 'Resolvido'?", "Confirmar Resolução", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //Atualiza o Status do Chamado
                        await _chamadoRepository.AtualizarStatusAsync(_chamadoId, novoStatus, conn, trans);

                        // Insere Histórico 
                        string statusAntigo = this.Tag?.ToString() ?? "Pendente";
                        Historico histStatus = new Historico
                        {
                            DataSolucao = dataAgora,
                            Solucao = $"O status foi alterado de '{statusAntigo}' para '{novoStatus}'",
                            FK_IdChamado = _chamadoId,
                            Acao = "Troca de Status"
                        };
                        await _historicoRepository.AdicionarAsync(histStatus, conn, trans);

                        // Insere Histórico 
                        Historico histSolucao = new Historico
                        {
                            DataSolucao = dataAgora,
                            Solucao = solucaoFinal,
                            FK_IdChamado = _chamadoId,
                            Acao = "Solução Aplicada"
                        };
                        await _historicoRepository.AdicionarAsync(histSolucao, conn, trans);

                        trans.Commit();

                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Chamado resolvido e registrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Erro ao salvar a resolução: " + ex.Message, "Erro de Transação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #region Métodos Auxiliares e de UI 

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DateTime ObterHoraBrasilia()
        {
            try
            {
                TimeZoneInfo brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brasilTimeZone);
            }
            catch { return DateTime.Now; }
        }

     

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null) return;
            Graphics g = e.Graphics;
            Color corInicioPanel = Color.White;
            Color corFimPanel = ColorTranslator.FromHtml("#232325");
            LinearGradientBrush gradientePanel = new LinearGradientBrush(
                   panel.ClientRectangle,
                   corInicioPanel,
                   corFimPanel,
                   LinearGradientMode.Vertical);
            g.FillRectangle(gradientePanel, panel.ClientRectangle);
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
        #endregion
    }
}