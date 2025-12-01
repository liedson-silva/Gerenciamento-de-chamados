using Gerenciamento_De_Chamados.Helpers;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;


namespace Gerenciamento_De_Chamados
{
    public partial class AberturaChamados : Form
    {

        private readonly ImageHelper _imageHelper;

        // Este campo é usado para passar os bytes do arquivo entre os métodos
        public byte[] arquivoAnexado;

        public AberturaChamados()
        {
            InitializeComponent();
            this.Load += AberturaChamados_Load;


            _imageHelper = new ImageHelper();
        }

        public void btnContinuar_Click(object sender, EventArgs e)
        {

            var continuaçaoabertura = new ContinuaçaoAbertura(this, _imageHelper);
            continuaçaoabertura.Show();
            this.Hide();
        }

        private void btnAnexArq_Click(object sender, EventArgs e)
        {

            arquivoAnexado = _imageHelper.SelecionarArquivoEConverter(this);

            // Limpa o botão, pois a imagem agora vai para o PictureBox
            btnAnexArq.Image = null;

            // Tenta localizar o PictureBox no formulário
            PictureBox pictureBox = this.Controls.Find("pctAnexoPreview", true).Length > 0 ?
                this.Controls.Find("pctAnexoPreview", true)[0] as PictureBox : null;

            if (arquivoAnexado != null)
            {

                Image imagemAnexada = _imageHelper.ByteArrayToImage(arquivoAnexado);


                if (imagemAnexada != null)
                {

                    if (pictureBox != null)
                    {
                        pictureBox.Image = imagemAnexada;
                        pictureBox.SizeMode = PictureBoxSizeMode.Zoom; 
                        pictureBox.Visible = true;
                    }

                    btnAnexArq.Text = $"Anexado: {_imageHelper.UltimoNomeArquivo}";

                }
                else
                {
                    // Caso o arquivo não seja uma imagem (ex: documento)
                    if (pictureBox != null)
                    {
                        pictureBox.Image = null;
                        pictureBox.Visible = false;
                    }
                    btnAnexArq.Text = "Anexar Arquivo";
                    MessageBox.Show($"Arquivo '{_imageHelper.UltimoNomeArquivo}' anexado. Prévia não disponível.", "Anexo Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // Se o usuário cancelou a seleção (arquivoAnexado é null)
                if (pictureBox != null)
                {
                    pictureBox.Image = null;
                    pictureBox.Visible = false;
                }
                // Garante que o estado interno da classe esteja limpo se o usuário cancelou
                this.arquivoAnexado = null;
                btnAnexArq.Text = "Anexar Arquivo";
                MessageBox.Show("Nenhum arquivo foi selecionado.");
            }
        }

        private void AberturaChamados_Load(object sender, EventArgs e)
        {
            

            // Garante que o PictureBox comece invisível
            if (this.Controls.Find("pctAnexoPreview", true).Length > 0 &&
                this.Controls.Find("pctAnexoPreview", true)[0] is PictureBox pictureBox)
            {
                pictureBox.Visible = false;
            }
        }

        private void lbl_Inicio_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void PctBox_Logo_Click(object sender, EventArgs e)
        {
            FormHelper.BotaoHome(this);
        }

        private void lblMconta_Click(object sender, EventArgs e)
        {
            var visualizarUsuario = new Visualizar_Usuario(SessaoUsuario.IdUsuario);
            visualizarUsuario.Show();
            this.Hide();
        }


        private void lbSair_Click(object sender, EventArgs e)
        {
            FormHelper.Sair(this);
        }


        private void lblFaq_Click_1(object sender, EventArgs e)
        {
            // O ideal seria usar FormHelper.FAQ<AberturaChamados>(this); se a FAQ tiver botão voltar.
            FormHelper.FAQ(this);
        }

        #region Código de Estética (Sem Alterações)
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
        #endregion

        private void AberturaChamados_Paint(object sender, PaintEventArgs e)
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

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {
            var verChamado = new VisualizarChamado();
            this.Hide();
            verChamado.ShowDialog();
            this.Show();
        }
    }
}