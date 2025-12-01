using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gerenciamento_De_Chamados.Helpers;
using Gerenciamento_De_Chamados.Models;
using Gerenciamento_De_Chamados.Repositories;

namespace Gerenciamento_De_Chamados
{
    public partial class FimChamado : Form
    {
        // Variável para receber o ID do chamado
        private int chamadoId;

        // Repositório para buscar os dados
        private readonly IChamadoRepository _chamadoRepository;

        public FimChamado(int idDoChamado)
        {
            InitializeComponent();
            this.chamadoId = idDoChamado;

            // Inicializa o repositório
            _chamadoRepository = new ChamadoRepository();

            this.Load += FimChamado_Load;


        }

        private async void FimChamado_Load(object sender, EventArgs e)
        {
            
         
            await CarregarResumoChamado();
        }

        private async Task CarregarResumoChamado()
        {
            try
            {
                // Busca os detalhes do chamado pelo ID
                Chamado chamado = await _chamadoRepository.BuscarPorIdAsync(this.chamadoId);

                if (chamado != null)
                {

                    string resumo = "✅ CHAMADO REGISTRADO COM SUCESSO!" + Environment.NewLine + Environment.NewLine;
                    resumo += $"🆔 ID do Chamado: {chamado.IdChamado}" + Environment.NewLine;
                    resumo += $"📅 Data e Hora: {chamado.DataChamado:dd/MM/yyyy 'às' HH:mm}" + Environment.NewLine;

                    resumo += Environment.NewLine + "---------------------------------------------------" + Environment.NewLine;
                    resumo += "Nossa equipe técnica já foi notificada. Você receberá atualizações sobre o andamento desta solicitação no seu e-mail.";

                    txtDescricao.Text = resumo;

                
                    txtDescricao.Select(0, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar informações do chamado: " + ex.Message);
            }
        }

        private void btnVerChamado_Click(object sender, EventArgs e)
        {
            var telaDetalhes = new ChamadoCriado(this.chamadoId);
            telaDetalhes.ShowDialog();
            
        }

        private void btn_PaginaInicial_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        #region Estética e Navegação
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

        private void FimChamado_Paint(object sender, PaintEventArgs e)
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

        private void lbl_Inicio_Click(object sender, EventArgs e) { FormHelper.BotaoHome(this); }
        private void PctBox_Logo_Click(object sender, EventArgs e) { FormHelper.BotaoHome(this); }
        private void lbSair_Click(object sender, EventArgs e) { FormHelper.Sair(this); }
        #endregion

        private void lblMconta_Click(object sender, EventArgs e)
        {
            var visualizarUsuario = new Visualizar_Usuario(SessaoUsuario.IdUsuario);
            visualizarUsuario.Show();
            this.Hide();
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
    }
}