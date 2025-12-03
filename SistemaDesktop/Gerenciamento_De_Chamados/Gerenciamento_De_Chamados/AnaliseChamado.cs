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
    /// <summary>
    /// **FORMULÁRIO: Análise e Resolução de Chamados**
    /// Esta tela permite ao Técnico (ou Admin) visualizar o chamado completo,
    /// aplicar a solução final, e opcionalmente, alterar a prioridade e categoria.
    /// </summary>
    public partial class AnaliseChamado : Form
    {
        private int _chamadoId;
        
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

       
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IHistoricoRepository _historicoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmailService _emailService; 

        
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
        }

        private async void AnaliseChamado_Load(object sender, EventArgs e)
        {
           

            await CarregarDadosChamadoAsync();
        }

        /// <summary>
        /// Busca o chamado no banco de dados e preenche todos os campos do formulário.
        /// Aplica a lógica de permissões (Admin vs. Técnico) e de status (Resolvido vs. Pendente).
        /// </summary>
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

                // 3. Monta o resumo visual (ReadOnly) no txtDescricao
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

                // 4. Preenche os ComboBoxes (para edição ou visualização)
                cboxCategoria.Text = _chamadoCarregado.Categoria;
                cboxPrioridade.Text = _chamadoCarregado.PrioridadeChamado;

                // 5. Verifica Permissão de Admin para determinar o que pode ser editado
                bool isAdmin = (SessaoUsuario.FuncaoUsuario?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true) ||
                               (SessaoUsuario.FuncaoUsuario?.Equals("Administrador", StringComparison.OrdinalIgnoreCase) == true);

                // Mostra/Esconde controles exclusivos de Admin (Categoria e Prioridade)
                lblPrioridade.Visible = isAdmin;
                cboxPrioridade.Visible = isAdmin;
                lblCategoria.Visible = isAdmin;
                cboxCategoria.Visible = isAdmin;

                // 6. Configura campos baseado no Status
                string statusAtual = _chamadoCarregado.StatusChamado;
                this.Tag = statusAtual; 

                if (statusAtual == "Resolvido")
                {
                    // Se já estiver resolvido, carrega a solução final registrada
                    string solucao = await _historicoRepository.BuscarUltimaSolucaoAsync(_chamadoId);
                    _solucaoCarregada = solucao ?? "Solução não registrada no histórico.";
                    txtSolucao.Text = _solucaoCarregada;

                    if (isAdmin)
                    {
                        // Admin pode editar prioridade/categoria/solução de um chamado resolvido
                        txtSolucao.ReadOnly = false;
                        btnEnviar.Enabled = true;
                        btnEnviar.Text = "Salvar Alterações";
                        cboxCategoria.Enabled = true;
                        cboxPrioridade.Enabled = true;
                    }
                    else
                    {
                        // Técnico comum apenas visualiza chamados resolvidos
                        txtSolucao.ReadOnly = true;
                        btnEnviar.Enabled = false;
                        btnEnviar.Text = "Resolvido";
                        cboxCategoria.Enabled = false;
                        cboxPrioridade.Enabled = false;
                    }
                }
                else // Pendente ou Em Andamento
                {
                    // Sugere a solução da IA (salva no Chamado) como ponto de partida
                    _solucaoCarregada = _chamadoCarregado.SolucaoSugeridaIA;
                    txtSolucao.Text = _solucaoCarregada;

                    txtSolucao.ReadOnly = false;
                    btnEnviar.Enabled = true;
                    btnEnviar.Text = "Enviar Resposta"; // Ao enviar, marca como 'Resolvido'

                    // Edição de Categoria/Prioridade liberada apenas para Admin
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

        /// <summary>
        /// **AÇÃO CRÍTICA: RESOLUÇÃO DO CHAMADO**
        /// Este método executa uma transação atômica que:
        /// 1. Atualiza o status do Chamado para 'Resolvido' (e Categoria/Prioridade, se alteradas).
        /// 2. Registra múltiplas ações no Histórico (Status, Solução, Alterações Admin).
        /// 3. Envia o e-mail de notificação para o usuário.
        /// </summary>
        private async void btnEnviar_Click(object sender, EventArgs e)
        {

            string solucaoFinal = txtSolucao.Text;
            string novaPrioridade = cboxPrioridade.Text;
            string novaCategoria = cboxCategoria.Text;

            // Variáveis de controle
            string statusAntigo = this.Tag?.ToString() ?? "Pendente";
            string novoStatus = "Resolvido";
            DateTime dataAgora = ObterHoraBrasilia();
            bool mudouAlgo = false; // Flag para determinar se houve qualquer mudança válida no chamado

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

            
            // Garante que todas as operações (Chamado e Histórico) sejam salvas ou nenhuma seja.
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Atualiza tabela Chamado (Status, Categoria, Prioridade)
                        await _chamadoRepository.AtualizarStatusAsync(_chamadoId, novoStatus, novaPrioridade, novaCategoria, conn, trans);
                        mudouAlgo = true;

                        // 2. Registra Histórico (Status) - Somente se o status for realmente alterado (i.e., de Pendente/Andamento para Resolvido)
                        if (statusAntigo != "Resolvido")
                        {
                            Historico histStatus = new Historico
                            {
                                DataSolucao = dataAgora,
                                Solucao = $"O status foi alterado de '{statusAntigo}' para '{novoStatus}' por {SessaoUsuario.Nome}.",
                                FK_IdChamado = _chamadoId,
                                Acao = "Troca de Status"
                            };
                            await _historicoRepository.AdicionarAsync(histStatus, conn, trans);
                            mudouAlgo = true;
                        }

                        // 3. Registra Histórico (Prioridade) - Se mudou E se o campo é visível (apenas Admin)
                        if (_chamadoCarregado.PrioridadeChamado != novaPrioridade && cboxPrioridade.Visible)
                        {
                            Historico histPrioridade = new Historico
                            {
                                // Adiciona segundos para garantir ordem cronológica no histórico, mesmo que salvo na mesma hora
                                DataSolucao = dataAgora.AddSeconds(1),
                                Solucao = $"Prioridade alterada de '{_chamadoCarregado.PrioridadeChamado ?? "N/A"}' para '{novaPrioridade}' pelo Admin.",
                                FK_IdChamado = _chamadoId,
                                Acao = "Nota Interna"
                            };
                            await _historicoRepository.AdicionarAsync(histPrioridade, conn, trans);
                            mudouAlgo = true;
                        }

                        // 4. Registra Histórico (Categoria) - Se mudou E se o campo é visível (apenas Admin)
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


                        // ** FIM DA TRANSAÇÃO: Confirma todas as mudanças **
                        trans.Commit();

                        // --- ENVIO DE E-MAIL (FORA DA TRANSAÇÃO) ---
                        // O envio é feito após o commit. Se o email falhar, o BD não é afetado.
                        try
                        {
                            // Busca o usuário dono para pegar o e-mail
                            Usuario usuarioDono = await _usuarioRepository.BuscarPorIdAsync(_chamadoCarregado.FK_IdUsuario);

                            if (usuarioDono != null)
                            {
                                // Usa o serviço de e-mail injetado para notificar o usuário da resolução
                                await _emailService.EnviarEmailResolucaoUsuarioAsync(_chamadoCarregado, usuarioDono, solucaoFinal);
                            }
                        }
                        catch (Exception exEmail)
                        {
                            Console.WriteLine($"Erro ao enviar email de resolução: {exEmail.Message}");
                            // Não faz rollback, pois o chamado já foi salvo. Apenas loga o erro.
                            MessageBox.Show("Chamado salvo, mas houve falha no envio do e-mail de notificação. Detalhes: " + exEmail.Message, "Aviso de Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        // ** FALHA NA TRANSAÇÃO: Desfaz todas as operações **
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
            // Retorna à tela anterior (Responder_Chamado)
            FormHelper.BotaoVoltar<Responder_Chamado>(this);
        }

        /// <summary>
        /// Obtém a data e hora atual no fuso horário de Brasília.
        /// (Relevante para a sua localização em São José dos Campos).
        /// </summary>
        private DateTime ObterHoraBrasilia()
        {
            try
            {
                // Fuso horário padrão do Brasil (Horário de Verão pode afetar, mas é um bom padrão)
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