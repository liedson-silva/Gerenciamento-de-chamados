using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using Gerenciamento_De_Chamados.Helpers;
using Gerenciamento_De_Chamados.Services;
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

        // Repositórios e Serviços
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IHistoricoRepository _historicoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmailService _emailService;

        // Guarda estado para comparações
        private Chamado _chamadoCarregado;
        private string _solucaoCarregada = "";

        public AnaliseChamado(int chamadoId)
        {
            InitializeComponent();
            _chamadoId = chamadoId;

            _chamadoRepository = new ChamadoRepository();
            _historicoRepository = new HistoricoRepository();
            _usuarioRepository = new UsuarioRepository();
            _emailService = new EmailService();

            this.Load += AnaliseChamado_Load;

            // Opcional: Adicionar o evento Click ao botão Enviar
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);

        }

        private async void AnaliseChamado_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessaoUsuario.Nome))
                lbl_NomeUser.Text = ($"Olá, {SessaoUsuario.Nome}");

            await CarregarDadosChamadoAsync();
        }

        private async Task CarregarDadosChamadoAsync()
        {
            try
            {
                // 1. Busca o chamado
                _chamadoCarregado = await _chamadoRepository.BuscarPorIdAsync(_chamadoId);
                if (_chamadoCarregado == null)
                {
                    MessageBox.Show("Chamado não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // 2. Busca o usuário criador (para exibir o nome)
                Usuario usuarioCriador = await _usuarioRepository.BuscarPorIdAsync(_chamadoCarregado.FK_IdUsuario);
                string nomeUsuario = usuarioCriador?.Nome ?? "Usuário Desconhecido";

                // 3. Monta o resumo visual (ReadOnly)
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
                txtDescricao.ReadOnly = true;

                // Posiciona o cursor no início do texto para não ficar rolado lá embaixo
                txtDescricao.SelectionStart = 0;
                txtDescricao.ScrollToCaret();

                // 4. Preenche os ComboBoxes (para edição)
                cboxCategoria.Text = _chamadoCarregado.Categoria;
                cboxPrioridade.Text = _chamadoCarregado.PrioridadeChamado;

                // 5. Verifica Permissão de Admin
                bool isAdmin = (SessaoUsuario.FuncaoUsuario?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true) ||
                               (SessaoUsuario.FuncaoUsuario?.Equals("Administrador", StringComparison.OrdinalIgnoreCase) == true);

                // Mostra/Esconde controles exclusivos de Admin
                lblPrioridade.Visible = isAdmin;
                cboxPrioridade.Visible = isAdmin;
                lblCategoria.Visible = isAdmin;
                cboxCategoria.Visible = isAdmin;

                // 6. Configura campos baseado no Status
                string statusAtual = _chamadoCarregado.StatusChamado;
                this.Tag = statusAtual; // Guarda o status original na Tag do form

                if (statusAtual == "Resolvido")
                {
                    string solucao = await _historicoRepository.BuscarUltimaSolucaoAsync(_chamadoId);
                    _solucaoCarregada = solucao ?? "Solução não registrada no histórico.";
                    txtSolucao.Text = _solucaoCarregada;

                    if (isAdmin)
                    {
                        // Admin pode reabrir ou editar chamado resolvido
                        txtSolucao.ReadOnly = false;
                        btnEnviar.Enabled = true;
                        btnEnviar.Text = "Salvar Alterações";
                        cboxCategoria.Enabled = true;
                        cboxPrioridade.Enabled = true;
                    }
                    else
                    {
                        // Técnico comum apenas visualiza
                        txtSolucao.ReadOnly = true;
                        btnEnviar.Enabled = false;
                        btnEnviar.Text = "Resolvido";
                        cboxCategoria.Enabled = false;
                        cboxPrioridade.Enabled = false;
                    }
                }
                else // Pendente ou Em Andamento
                {
                    // Sugere a solução da IA se o campo estiver vazio
                    _solucaoCarregada = _chamadoCarregado.SolucaoSugeridaIA;
                    txtSolucao.Text = _solucaoCarregada;

                    txtSolucao.ReadOnly = false;
                    btnEnviar.Enabled = true;
                    btnEnviar.Text = "Enviar Resposta";

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
            // Captura valores da UI
            string solucaoFinal = txtSolucao.Text;
            string novaPrioridade = cboxPrioridade.Text;
            string novaCategoria = cboxCategoria.Text;

            string statusAntigo = this.Tag?.ToString() ?? "Pendente";
            string novoStatus = "Resolvido";
            DateTime dataAgora = ObterHoraBrasilia();

            // Validação simples
            if (string.IsNullOrWhiteSpace(solucaoFinal))
            {
                MessageBox.Show("Por favor, descreva a solução aplicada.", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string confirmMsg = (statusAntigo == "Resolvido")
                ? "Deseja salvar as alterações neste chamado já resolvido?"
                : "Deseja aplicar esta solução e marcar o chamado como 'Resolvido'? O usuário será notificado.";

            if (MessageBox.Show(confirmMsg, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            bool mudouAlgo = false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Atualiza tabela Chamado (Status, Categoria, Prioridade)
                        await _chamadoRepository.AtualizarStatusAsync(_chamadoId, novoStatus, novaPrioridade, novaCategoria, conn, trans);

                        // 2. Registra Histórico (Status)
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

                        // 3. Registra Histórico (Prioridade - se mudou e é visível)
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

                        // 4. Registra Histórico (Categoria - se mudou e é visível)
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

                        // 5. Registra Histórico (Solução Aplicada)
                        // Registra se a solução mudou OU se é a primeira vez que está sendo resolvido
                        if (solucaoFinal != _solucaoCarregada || statusAntigo != "Resolvido")
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

                        // CONFIRMA NO BANCO
                        trans.Commit();

                        // --- ENVIO DE E-MAIL ---
                        try
                        {
                            // Busca o usuário dono para pegar o e-mail
                            Usuario usuarioDono = await _usuarioRepository.BuscarPorIdAsync(_chamadoCarregado.FK_IdUsuario);

                            if (usuarioDono != null)
                            {
                                await _emailService.EnviarEmailResolucaoUsuarioAsync(_chamadoCarregado, usuarioDono, solucaoFinal);
                            }
                        }
                        catch (Exception exEmail)
                        {
                            Console.WriteLine($"Erro ao enviar email de resolução: {exEmail.Message}");
                            // Não faz rollback, pois o chamado já foi salvo. Apenas loga.
                        }
                        // -----------------------

                        this.Cursor = Cursors.Default;

                        if (mudouAlgo || statusAntigo != "Resolvido")
                            MessageBox.Show("Alterações salvas e usuário notificado!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Nenhuma alteração detectada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #region Métodos Auxiliares (UI e Data)

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoVoltar<Responder_Chamado>(this);
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

        private void AnaliseChamado_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color corInicio = Color.White;
            Color corFim = ColorTranslator.FromHtml("#232325");
            using (LinearGradientBrush gradiente = new LinearGradientBrush(this.ClientRectangle, corInicio, corFim, LinearGradientMode.Horizontal))
            {
                g.FillRectangle(gradiente, this.ClientRectangle);
            }
        }

        // Outros eventos de UI
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
        
        private void PctBox_Logo_Click(object sender, EventArgs e) { FormHelper.BotaoHome(this); }
        private void lbSair_Click(object sender, EventArgs e) { FormHelper.Sair(this); }

        #endregion

        private void lblMconta_Click(object sender, EventArgs e)
        {
            var visualizarUsuario = new Visualizar_Usuario(SessaoUsuario.IdUsuario);
            visualizarUsuario.Show();
            this.Close();
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

        private void lbl_Inicio_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }
    }
}