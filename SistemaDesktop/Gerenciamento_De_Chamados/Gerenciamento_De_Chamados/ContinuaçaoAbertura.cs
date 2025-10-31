
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
using Gerenciamento_De_Chamados.Helpers;
using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using Gerenciamento_De_Chamados.Services;

namespace Gerenciamento_De_Chamados
{
    public partial class ContinuaçaoAbertura : Form
    {
        private AberturaChamados aberturaChamados;
        private readonly ImageHelper _imageHelper;
        private readonly IEmailService _emailService;
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IArquivoRepository _arquivoRepository;


        public ContinuaçaoAbertura(AberturaChamados abertura, ImageHelper imageHelper)
        {
            InitializeComponent();
            aberturaChamados = abertura;

            _imageHelper = imageHelper;
            _chamadoRepository = new ChamadoRepository();
            _arquivoRepository = new ArquivoRepository();
            _emailService = new EmailService();
            this.Load += ContinuaçaoAbertura_Load;
        }


        private async void btnConcluirCH_Click(object sender, EventArgs e)
        {
            string TituloChamado = aberturaChamados.txtTituloChamado.Text.Trim();
            string DescricaoChamado = aberturaChamados.txtDescriçãoCh.Text.Trim();
            string PessoasAfetadas = cBoxPessoasAfeta.Text.Trim();
            string ImpedeTrabalho = cBoxImpedTrab.Text;
            string OcorreuAnteriormente = cBoxAcontAntes.Text;
            string CategoriaChamado = aberturaChamados.cboxCtgChamado.Text;
            byte[] AnexarArquivo = aberturaChamados.arquivoAnexado;

            string status = "Pendente";
            string problemaIA = "Pendente";
            string solucaoIA = "Pendente";
            string prioridadeIA = "Analise";
            try
            {
                //Busca soluções anteriores usando o REPOSITÓRIO
                List<string> solucoesAnteriores = await _chamadoRepository.BuscarSolucoesAnterioresAsync(CategoriaChamado);

                AIService aiService = new AIService();
                var (problema, prioridade, solucao) = await aiService.AnalisarChamado(TituloChamado, PessoasAfetadas,
                    OcorreuAnteriormente, ImpedeTrabalho, DescricaoChamado, CategoriaChamado, solucoesAnteriores);

                problemaIA = problema;
                solucaoIA = solucao;
                prioridadeIA = prioridade;
            }
            catch (Exception aiEx)
            {
                MessageBox.Show($"Erro ao analisar chamado com IA: {aiEx.Message}. O chamado será criado sem sugestão.", "Aviso IA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

         

            int idUsuario = SessaoUsuario.IdUsuario;

            try
            {
              
                TimeZoneInfo brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                DateTime horaDeBrasilia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brasilTimeZone);

                Chamado novoChamado = new Chamado
                {
                    Titulo = TituloChamado,
                    PrioridadeChamado = "Análise", 
                    Descricao = DescricaoChamado,
                    DataChamado = horaDeBrasilia,
                    StatusChamado = status,
                    Categoria = CategoriaChamado,
                    FK_IdUsuario = idUsuario,
                    PessoasAfetadas = PessoasAfetadas,
                    ImpedeTrabalho = ImpedeTrabalho,
                    OcorreuAnteriormente = OcorreuAnteriormente,
                    PrioridadeSugeridaIA = prioridadeIA,
                    ProblemaSugeridoIA = problemaIA,
                    SolucaoSugeridaIA = solucaoIA
                };

                
                int idChamado = await _chamadoRepository.AdicionarAsync(novoChamado);

                if (idChamado == -1) 
                {
                    MessageBox.Show("Não foi possível criar o chamado. Verifique o log de erros.");
                    return;
                }


                string nomeAnexo = "anexo_chamado.png"; 
                if (AnexarArquivo != null && AnexarArquivo.Length > 0)
                {
                   
                    nomeAnexo = _imageHelper.UltimoNomeArquivo ?? nomeAnexo;
                    string tipoAnexo = _imageHelper.UltimoTipoArquivo ?? "application/octet-stream";

                    Arquivo novoArquivo = new Arquivo
                    {
                        TipoArquivo = tipoAnexo,
                        NomeArquivo = nomeAnexo,
                        ArquivoBytes = AnexarArquivo,
                        FK_IdChamado = idChamado
                    };
                    await _arquivoRepository.AdicionarAsync(novoArquivo);
                }

                MessageBox.Show("Chamado aberto com sucesso! Número do chamado: " + idChamado);

                // Envia o e-mail
                await _emailService.EnviarEmailChamadoAsync(
                    TituloChamado, DescricaoChamado, CategoriaChamado, idChamado,
                    prioridadeIA, status, PessoasAfetadas, ImpedeTrabalho,
                    OcorreuAnteriormente, problemaIA, solucaoIA,
                    AnexarArquivo, nomeAnexo
                );

                var telaFim = new FimChamado(idChamado);
                telaFim.ShowDialog();

                aberturaChamados.Close();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar chamado no banco: " + ex.Message);
            }
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
                    LinearGradientMode.Vertical);
            g.FillRectangle(gradientePanel, panel1.ClientRectangle);
        }
        private void ContinuaçaoAbertura_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessaoUsuario.Nome))
                lbl_NomeUser.Text = ($"Bem vindo {SessaoUsuario.Nome}");
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
    }
}