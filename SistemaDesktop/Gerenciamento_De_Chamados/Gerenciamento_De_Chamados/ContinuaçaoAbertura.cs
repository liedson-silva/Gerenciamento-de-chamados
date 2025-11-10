
using Gerenciamento_De_Chamados.Helpers;
using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;
using Gerenciamento_De_Chamados.Services;
using Gerenciamento_De_Chamados.Validacao;
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
        private readonly ImageHelper _imageHelper;
        private readonly IEmailService _emailService;
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IArquivoRepository _arquivoRepository;
        private readonly ChamadoService _chamadoService;


        public ContinuaçaoAbertura(AberturaChamados abertura, ImageHelper imageHelper)
        {
            InitializeComponent();
            aberturaChamados = abertura;

            _imageHelper = imageHelper;
            _chamadoRepository = new ChamadoRepository();
            _arquivoRepository = new ArquivoRepository();
            _emailService = new EmailService();

            _chamadoService = new ChamadoService(
            _chamadoRepository,
            _arquivoRepository,
            _emailService,
            new AIService()
            );

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

            // ValidadorChamado para título e descrição
            if (!ValidadorChamado.IsTituloValido(TituloChamado))
            {
                MessageBox.Show("O título é obrigatório e deve ter mais de 5 caracteres.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Para a execução, não continua
            }

            if (!ValidadorChamado.IsDescricaoValida(DescricaoChamado))
            {
                MessageBox.Show("A descrição é obrigatória e deve ter mais de 10 caracteres.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Para a execução
            }

            if (!ValidadorChamado.IsPessoasAfetadasValido(PessoasAfetadas))
            {
                MessageBox.Show("Por favor, selecione o número de pessoas afetadas.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validação simples para os ComboBox (apenas para saber se algo foi selecionado)
            if (string.IsNullOrWhiteSpace(PessoasAfetadas))
            {
                MessageBox.Show("Por favor, selecione o número de pessoas afetadas.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(ImpedeTrabalho))
            {
                MessageBox.Show("Por favor, informe se o problema impede o trabalho.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(OcorreuAnteriormente))
            {
                MessageBox.Show("Por favor, informe se o problema ocorreu anteriormente.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(CategoriaChamado))
            {
                MessageBox.Show("Por favor, selecione uma categoria.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
         

            int idUsuario = SessaoUsuario.IdUsuario;

            try
            {

                DateTime horaDeBrasilia = DateTime.Now;

                Chamado novoChamado = new Chamado
                {
                    Titulo = TituloChamado,
                    PrioridadeChamado = "Análise", // Valor padrão
                    Descricao = DescricaoChamado,
                    DataChamado = horaDeBrasilia,
                    StatusChamado = "Pendente",
                    Categoria = CategoriaChamado,
                    FK_IdUsuario = SessaoUsuario.IdUsuario,
                    PessoasAfetadas = PessoasAfetadas,
                    ImpedeTrabalho = ImpedeTrabalho,
                    OcorreuAnteriormente = OcorreuAnteriormente
                };


                
                string tipoAnexo = _imageHelper.UltimoTipoArquivo ?? "application/octet-stream";
                string nomeAnexo = _imageHelper.UltimoNomeArquivo ?? "anexo_chamado.png";

                int idChamado = await _chamadoService.CriarNovoChamadoAsync(novoChamado, AnexarArquivo, nomeAnexo, tipoAnexo);


                MessageBox.Show("Chamado aberto com sucesso! Número do chamado: " + idChamado);

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
                lbl_NomeUser.Text = ($" {SessaoUsuario.Nome}");
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
            this.Close();
        }
    }
}