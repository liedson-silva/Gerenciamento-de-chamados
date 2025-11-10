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

        // Repositórios
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IHistoricoRepository _historicoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        // Guarda o chamado original para comparar mudanças
        private Chamado _chamadoCarregado;
        // Guarda a solução original para comparar mudanças
        private string _solucaoCarregada = "";

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
                // Busca e armazena o chamado
                _chamadoCarregado = await _chamadoRepository.BuscarPorIdAsync(_chamadoId);
                if (_chamadoCarregado == null)
                {
                    MessageBox.Show("Chamado não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Busca o usuário criador
                Usuario usuarioCriador = await _usuarioRepository.BuscarPorIdAsync(_chamadoCarregado.FK_IdUsuario);
                string nomeUsuario = usuarioCriador?.Nome ?? "Usuário Desconhecido";

                // Monta o resumo
                StringBuilder resumo = new StringBuilder();
                resumo.AppendLine($"ID do Chamado: {_chamadoCarregado.IdChamado}");
                resumo.AppendLine($"Criado por: {nomeUsuario}");
                resumo.AppendLine($"Data/Hora: {_chamadoCarregado.DataChamado:dd/MM/yyyy 'às' HH:mm:ss}");
                resumo.AppendLine($"Categoria Atual: {_chamadoCarregado.Categoria}");
                resumo.AppendLine($"Prioridade Atual: {_chamadoCarregado.PrioridadeChamado}");
                resumo.AppendLine("---");
                resumo.AppendLine($"Título: {_chamadoCarregado.Titulo}");
                resumo.AppendLine("---");
                resumo.AppendLine($"Descrição do Problema:");
                resumo.AppendLine(_chamadoCarregado.Descricao);

                txtDescricao.Text = resumo.ToString();
                txtDescricao.ReadOnly = true; // Descrição é sempre ReadOnly

                // Preenche os ComboBoxes de Admin
                cboxCategoria.SelectedItem = _chamadoCarregado.Categoria;
                cboxPrioridade.SelectedItem = _chamadoCarregado.PrioridadeChamado;

                //  Verifica Permissão
                bool isAdmin = (SessaoUsuario.FuncaoUsuario?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true) ||
                               (SessaoUsuario.FuncaoUsuario?.Equals("Administrador", StringComparison.OrdinalIgnoreCase) == true);

                // Mostra/Esconde os controles de Admin
                lblPrioridade.Visible = isAdmin;
                cboxPrioridade.Visible = isAdmin;
                lblCategoria.Visible = isAdmin;
                cboxCategoria.Visible = isAdmin;

                // Preenche a solução E define o estado dos controles
                string statusAtual = _chamadoCarregado.StatusChamado;
                this.Tag = statusAtual;

                if (statusAtual == "Resolvido")
                {
                    string solucao = await _historicoRepository.BuscarUltimaSolucaoAsync(_chamadoId);
                    _solucaoCarregada = solucao ?? "Solução não registrada no histórico.";
                    txtSolucao.Text = _solucaoCarregada;

                    if (isAdmin)
                    {
                        // ADMIN PODE EDITAR CHAMADO RESOLVIDO
                        txtSolucao.ReadOnly = false;
                        btnEnviar.Enabled = true;
                        btnEnviar.Text = "Salvar Alterações";
                        cboxCategoria.Enabled = true;
                        cboxPrioridade.Enabled = true;
                    }
                    else
                    {
                        // NÃO-ADMIN VÊ TUDO TRAVADO
                        txtSolucao.ReadOnly = true;
                        btnEnviar.Enabled = false;
                        btnEnviar.Text = "Resolvido";
                        cboxCategoria.Enabled = false;
                        cboxPrioridade.Enabled = false;
                    }
                }
                else // Status é "Pendente" ou "Em Andamento"
                {
                    _solucaoCarregada = _chamadoCarregado.SolucaoSugeridaIA;
                    txtSolucao.Text = _solucaoCarregada;

                    txtSolucao.ReadOnly = false;
                    btnEnviar.Enabled = true;
                    btnEnviar.Text = "Enviar Resposta";

                    // Admin pode editar Categoria/Prioridade
                    cboxCategoria.Enabled = isAdmin;
                    cboxPrioridade.Enabled = isAdmin;
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
            //  Pega todos os valores atuais (editados ou não)
            string solucaoFinal = txtSolucao.Text;
            string novaPrioridade = cboxPrioridade.Text;
            string novaCategoria = cboxCategoria.Text;

            string statusAntigo = this.Tag?.ToString() ?? "Pendente";
            string novoStatus = "Resolvido"; // O status final será sempre "Resolvido"
            DateTime dataAgora = ObterHoraBrasilia();

            // Validação
            if (string.IsNullOrWhiteSpace(solucaoFinal))
            {
                MessageBox.Show("Por favor, descreva a solução aplicada.", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string confirmMsg = (statusAntigo == "Resolvido")
                ? "Deseja salvar as alterações neste chamado já resolvido?"
                : "Deseja aplicar esta solução e marcar o chamado como 'Resolvido'?";

            if (MessageBox.Show(confirmMsg, "Confirmar Alterações", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            bool mudouAlgo = false; // Flag para saber se algo foi alterado

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // Atualiza o Chamado (sempre atualiza tudo)
                        await _chamadoRepository.AtualizarStatusAsync(_chamadoId, novoStatus, novaPrioridade, novaCategoria, conn, trans);

                        // Registra no Histórico (APENAS O QUE MUDOU)

                        // Se o status MUDOU (Pendente -> Resolvido)
                        if (statusAntigo != "Resolvido")
                        {
                            Historico histStatus = new Historico
                            {
                                DataSolucao = dataAgora,
                                Solucao = $"O status foi alterado de '{statusAntigo}' para '{novoStatus}'",
                                FK_IdChamado = _chamadoId,
                                Acao = "Troca de Status"
                            };
                            await _historicoRepository.AdicionarAsync(histStatus, conn, trans);
                            mudouAlgo = true;
                        }

                        // Se a PRIORIDADE mudou (e o admin pode ver os controles)
                        if (_chamadoCarregado.PrioridadeChamado != novaPrioridade && cboxPrioridade.Visible)
                        {
                            Historico histPrioridade = new Historico
                            {
                                DataSolucao = dataAgora.AddSeconds(1),
                                Solucao = $"Prioridade alterada de '{_chamadoCarregado.PrioridadeChamado ?? "N/A"}' para '{novaPrioridade}' pelo Admin.",
                                FK_IdChamado = _chamadoId,
                                Acao = "Nota Interna"
                            };
                            await _historicoRepository.AdicionarAsync(histPrioridade, conn, trans);
                            mudouAlgo = true;
                        }

                        // Se a CATEGORIA mudou (e o admin pode ver os controles)
                        if (_chamadoCarregado.Categoria != novaCategoria && cboxCategoria.Visible)
                        {
                            Historico histCategoria = new Historico
                            {
                                DataSolucao = dataAgora.AddSeconds(2),
                                Solucao = $"Categoria alterada de '{_chamadoCarregado.Categoria}' para '{novaCategoria}' pelo Admin.",
                                FK_IdChamado = _chamadoId,
                                Acao = "Nota Interna"
                            };
                            await _historicoRepository.AdicionarAsync(histCategoria, conn, trans);
                            mudouAlgo = true;
                        }

                        // Se a SOLUÇÃO mudou (ou se é a primeira vez sendo resolvida)
                        if (solucaoFinal != _solucaoCarregada)
                        {
                            string acao = (statusAntigo == "Resolvido") ? "Solução Editada" : "Solução Aplicada";
                            Historico histSolucao = new Historico
                            {
                                DataSolucao = dataAgora.AddSeconds(3),
                                Solucao = solucaoFinal,
                                FK_IdChamado = _chamadoId,
                                Acao = acao
                            };
                            await _historicoRepository.AdicionarAsync(histSolucao, conn, trans);
                            mudouAlgo = true;
                        }

                        trans.Commit();
                        this.Cursor = Cursors.Default;

                        if (mudouAlgo || statusAntigo != "Resolvido")
                            MessageBox.Show("Alterações salvas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Nenhuma alteração detectada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Erro ao salvar as alterações: " + ex.Message, "Erro de Transação", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // (Métodos de pintura e navegação)
        private void panel1_Paint(object sender, PaintEventArgs e) { /* ... seu código de gradiente ... */ }
        private void lbl_Inicio_Click(object sender, EventArgs e) { FormHelper.BotaoHome(this); }
        private void PctBox_Logo_Click(object sender, EventArgs e) { FormHelper.BotaoHome(this); }
        private void lbSair_Click(object sender, EventArgs e) { FormHelper.Sair(this); }

        #endregion

        private void AnaliseChamado_Paint(object sender, PaintEventArgs e)
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

        private void panel1_Paint_1(object sender, PaintEventArgs e)
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
    }
}