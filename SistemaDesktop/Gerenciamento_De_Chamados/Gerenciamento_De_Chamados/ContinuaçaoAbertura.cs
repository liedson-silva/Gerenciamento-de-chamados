using Gerenciamento_De_Chamados.Helpers;
using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using Gerenciamento_De_Chamados.Services;
using Gerenciamento_De_Chamados.Validacao;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Para possíveis exceções de DB

namespace Gerenciamento_De_Chamados
{
    public partial class ContinuaçaoAbertura : Form
    {
        private AberturaChamados aberturaChamados;
        private readonly ImageHelper _imageHelper;
        private readonly IEmailService _emailService;
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IArquivoRepository _arquivoRepository;
        private readonly ChamadoService _chamadoService;
        private readonly IAIService _aiService;


        public ContinuaçaoAbertura(AberturaChamados abertura, ImageHelper imageHelper)
        {
            InitializeComponent();
            aberturaChamados = abertura;

            // Inicialização das dependências (injetadas ou instanciadas)
            _imageHelper = imageHelper;
            _chamadoRepository = new ChamadoRepository();
            _arquivoRepository = new ArquivoRepository();
            _emailService = new EmailService();
            _aiService = new AIService();

            // Instancia o serviço de chamado com todas as dependências
            _chamadoService = new ChamadoService(
            _chamadoRepository,
            _arquivoRepository,
            _emailService,
            _aiService
            );

            // Assinatura dos eventos para centralização do painel de carregamento
            this.Load += ContinuaçaoAbertura_Load;
            this.Resize += ContinuaçaoAbertura_Resize;
        }


        private async void btnConcluirCH_Click(object sender, EventArgs e)
        {
            // --- VALIDAÇÃO ---
            string TituloChamado = aberturaChamados.txtTituloChamado.Text.Trim();
            string DescricaoChamado = aberturaChamados.txtDescriçãoCh.Text.Trim();
            string PessoasAfetadas = cBoxPessoasAfeta.Text.Trim();
            string ImpedeTrabalho = cBoxImpedTrab.Text;
            string OcorreuAnteriormente = cBoxAcontAntes.Text;
            string CategoriaChamado = aberturaChamados.cboxCtgChamado.Text;

            // Validações OBRIGATÓRIAS
            if (!ValidadorChamado.IsTituloValido(TituloChamado))
            {
                MessageBox.Show("O título é obrigatório e deve ter mais de 5 caracteres.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidadorChamado.IsDescricaoValida(DescricaoChamado))
            {
                MessageBox.Show("A descrição é obrigatória e deve ter mais de 10 caracteres.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(PessoasAfetadas) || string.IsNullOrWhiteSpace(ImpedeTrabalho) || string.IsNullOrWhiteSpace(OcorreuAnteriormente) || string.IsNullOrWhiteSpace(CategoriaChamado))
            {
                MessageBox.Show("Por favor, preencha todos os campos da continuação do chamado.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- INÍCIO DO CARREGAMENTO VISUAL ---
            pnlLoading.Visible = true;
            CentralizarPainelLoading();
            pnlLoading.BringToFront(); // Traz para frente de todos os controles
            btnConcluirCH.Enabled = false; // Desabilita botões para evitar cliques duplicados
            button1.Enabled = false;
            this.Cursor = Cursors.WaitCursor; // Muda o cursor
            Application.DoEvents(); // Garante que a UI atualize imediatamente

            try
            {
                byte[] arquivoBytes = aberturaChamados.arquivoAnexado;
                string nomeAnexo = _imageHelper.UltimoNomeArquivo;
                string tipoAnexo = _imageHelper.UltimoTipoArquivo;

                DateTime horaDeBrasilia = ObterHoraBrasilia();

                Chamado novoChamado = new Chamado
                {
                    Titulo = TituloChamado,
                    Descricao = DescricaoChamado,
                    Categoria = CategoriaChamado,
                    DataChamado = horaDeBrasilia,
                    StatusChamado = "Pendente",
                    FK_IdUsuario = SessaoUsuario.IdUsuario,
                    PessoasAfetadas = PessoasAfetadas,
                    ImpedeTrabalho = ImpedeTrabalho,
                    OcorreuAnteriormente = OcorreuAnteriormente,
                    // Deixa as sugestões da IA vazias/nulas por enquanto
                    PrioridadeSugeridaIA = null,
                    ProblemaSugeridoIA = null,
                    SolucaoSugeridaIA = null
                };

                Usuario usuarioLogado = new Usuario
                {
                    Nome = SessaoUsuario.Nome,
                    Email = SessaoUsuario.Email,
                    IdUsuario = SessaoUsuario.IdUsuario
                };

                // ETAPA 1: Cria o chamado base no DB (RÁPIDO)
                int idChamado = await _chamadoService.CriarChamadoBaseAsync(novoChamado, arquivoBytes, nomeAnexo, tipoAnexo);

                // ETAPA 2: Envia email de confirmação para o USUÁRIO (RÁPIDO/SÍNCRONO)
                await _chamadoService.EnviarConfirmacaoUsuarioAsync(novoChamado, usuarioLogado, idChamado);

                // ETAPA 3: Processamento da IA e notificação da TI (LENTO/BACKGROUND)
                // Usamos Task.Run para que a interface NÃO TRAVE enquanto a IA pensa.
                Task.Run(async () =>
                {
                    await _chamadoService.ProcessarAnaliseEAtualizarAsync(idChamado, novoChamado, usuarioLogado, arquivoBytes, nomeAnexo, tipoAnexo);
                });

                // Mostra a tela de conclusão imediatamente
                var telaFim = new FimChamado(idChamado);
                telaFim.Show();

                aberturaChamados.Close();
                this.Close();
            }
            catch (SqlException sqlex)
            {
                MessageBox.Show("Erro de Banco de Dados: " + sqlex.Message, "Erro SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar chamado ou enviar confirmação: " + ex.Message, "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
             
                if (!this.IsDisposed)
                {
                    pnlLoading.Visible = false;
                    btnConcluirCH.Enabled = true;
                    button1.Enabled = true;
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private DateTime ObterHoraBrasilia()
        {
            try
            {
               
                TimeZoneInfo brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brasilTimeZone);
            }
            catch
            {
               
                return DateTime.Now;
            }
        }


        #region Métodos de Centralização do Painel

        private void ContinuaçaoAbertura_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessaoUsuario.Nome))
                lbl_NomeUser.Text = ($" {SessaoUsuario.Nome}");

           
            CentralizarPainelLoading();
        }

        private void ContinuaçaoAbertura_Resize(object sender, EventArgs e)
        {
            
            CentralizarPainelLoading();
        }

        private void CentralizarPainelLoading()
        {
           
            if (pnlLoading != null && this.ClientSize.Width > 0 && this.ClientSize.Height > 0)
            {
               
                pnlLoading.Left = (this.ClientSize.Width - pnlLoading.Width) / 2;
                pnlLoading.Top = (this.ClientSize.Height - pnlLoading.Height) / 2;
            }
        }

        #endregion

        #region Código de Estética e Navegação

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

        private void lbl_Inicio_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void lbSair_Click(object sender, EventArgs e)
        {
            FormHelper.Sair(this);
        }

        private void ContinuaçaoAbertura_Paint(object sender, PaintEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Botão Voltar: reabre a tela anterior
            aberturaChamados.Show();
            this.Close();
        }

        #endregion
    }
}