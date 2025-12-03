using Gerenciamento_De_Chamados.Helpers;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;


namespace Gerenciamento_De_Chamados
{
    /// <summary>
    /// **FORMULÁRIO: ABERTURA DE CHAMADOS (Fase 1)**
    /// Este formulário coleta os dados iniciais do chamado (Título, Descrição, Categoria)
    /// e permite ao usuário anexar um arquivo/screenshot.
    /// </summary>
    public partial class AberturaChamados : Form
    {

        // Propriedade para lidar com a conversão de arquivos e imagens.
        private readonly ImageHelper _imageHelper;

        /// <summary>
        /// Campo público que armazena os bytes do arquivo anexado. 
        /// Isso permite que a tela ContinuaçãoAbertura acesse os dados do anexo
        /// sem quebrar a referência ao objeto.
        /// </summary>
        public byte[] arquivoAnexado;

        public AberturaChamados()
        {
            InitializeComponent();
            this.Load += AberturaChamados_Load;

            // Instancia o Helper que fará a conversão entre Arquivo <-> Bytes <-> Imagem
            _imageHelper = new ImageHelper();
        }

        /// <summary>
        /// Ação do botão "Continuar".
        /// Passa o controle para a segunda tela de abertura (ContinuaçaoAbertura).
        /// </summary>
        public void btnContinuar_Click(object sender, EventArgs e)
        {
            // O objeto 'this' (AberturaChamados) e o _imageHelper são passados para a próxima tela
            // para que ela possa acessar os dados (Título, Descrição) e o arquivo anexado (arquivoAnexado).
            var continuaçaoabertura = new ContinuaçaoAbertura(this, _imageHelper);
            continuaçaoabertura.Show();
            this.Hide();
        }

        /// <summary>
        /// Ação do botão "Anexar Arquivo". Responsável por abrir a caixa de diálogo de arquivos,
        /// converter o arquivo para bytes e exibir o preview.
        /// </summary>
        private void btnAnexArq_Click(object sender, EventArgs e)
        {
            // 1. Chama o Helper para abrir a caixa de diálogo e converter o arquivo selecionado
            arquivoAnexado = _imageHelper.SelecionarArquivoEConverter(this);

            
            PictureBox pictureBox = this.Controls.Find("pctAnexoPreview", true).Length > 0 ?
                this.Controls.Find("pctAnexoPreview", true)[0] as PictureBox : null;

            if (arquivoAnexado != null)
            {
              
                Image imagemAnexada = _imageHelper.ByteArrayToImage(arquivoAnexado);


                if (imagemAnexada != null)
                {
                    // Se for imagem: exibe o preview no PictureBox
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
               
                    if (pictureBox != null)
                    {
                        pictureBox.Image = null; 
                        pictureBox.Visible = false;
                    }
                    btnAnexArq.Text = "Arquivo Anexado"; 
                    MessageBox.Show($"Arquivo '{_imageHelper.UltimoNomeArquivo}' anexado. Prévia não disponível.", "Anexo Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
              
                if (pictureBox != null)
                {
                    pictureBox.Image = null;
                    pictureBox.Visible = false;
                }
               
                this.arquivoAnexado = null;
                btnAnexArq.Text = "Anexar Arquivo";
                MessageBox.Show("Nenhum arquivo foi selecionado.");
            }
        }

        /// <summary>
        /// Método de carregamento do formulário. Usado para inicialização visual.
        /// </summary>
        private void AberturaChamados_Load(object sender, EventArgs e)
        {
            // Garante que o PictureBox comece invisível, só aparecendo quando um anexo for carregado.
            if (this.Controls.Find("pctAnexoPreview", true).Length > 0 &&
                this.Controls.Find("pctAnexoPreview", true)[0] is PictureBox pictureBox)
            {
                pictureBox.Visible = false;
            }
            // Adiciona aqui a exibição do nome do usuário logado na label lbl_NomeUser, se houver
        }

        // ====================================================================================
        // MÉTODOS DE NAVEGAÇÃO
        // Todos usam o FormHelper para garantir o fluxo de tela e logoff.
        // ====================================================================================

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
            // Navegação para a tela de visualização de chamados.
            var verChamado = new VisualizarChamado();
            this.Hide();
            verChamado.ShowDialog();
            this.Show();
        }
    }
}