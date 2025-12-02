using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Windows.Forms;


namespace Gerenciamento_De_Chamados.Helpers
{
    /// <summary>
    /// Esta é minha "Caixa de Ferramentas" para lidar com anexos e imagens no sistema.
    /// Ela cuida de abrir a janela para selecionar o arquivo e converter ele em bytes
    /// para que possamos salvar no banco de dados.
    /// </summary>
    public class ImageHelper
    {
        // ====================================================================
        // I. PROPRIEDADES (O QUE A CAIXA DE FERRAMENTAS GUARDA)
        // ====================================================================

        /// <summary>
        /// Guarda o nome original do último arquivo que foi selecionado pelo usuário.
        /// (Ex: "documento_com_erro.png")
        /// </summary>
        public string UltimoNomeArquivo { get; private set; }

        /// <summary>
        /// Guarda o tipo do último arquivo (MIME Type), para saber se é PNG, PDF, etc.
        /// (Ex: "image/png" ou "application/pdf")
        /// </summary>
        public string UltimoTipoArquivo { get; private set; }


        // ====================================================================
        // II. FUNÇÃO PRINCIPAL (ABRIR E CONVERTER)
        // ====================================================================

        /// <summary>
        /// Abre a janela do computador para o usuário escolher um arquivo (imagem ou outro).
        /// Depois, ela converte esse arquivo em um monte de "números" (bytes) para salvar no banco.
        /// </summary>
        /// <param name="owner">O formulário pai que chamou esta função para não minimizar a tela.</param>
        /// <returns>O arquivo convertido em bytes (byte[]) ou nulo se o usuário cancelar.</returns>
        public byte[] SelecionarArquivoEConverter(IWin32Window owner)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                // Deixa claro que ele suporta mais tipos, mas prioriza imagens
                ofd.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.bmp|Todos os Arquivos|*.*";

               
                if (ofd.ShowDialog(owner) == DialogResult.OK)
                {
                    // Guarda o nome e tipo
                    this.UltimoNomeArquivo = Path.GetFileName(ofd.FileName);
                  
                    this.UltimoTipoArquivo = System.Web.MimeMapping.GetMimeMapping(this.UltimoNomeArquivo) ?? "application/octet-stream";

                    // Tenta criar a imagem a partir do arquivo
                    try
                    {
                        // Se for uma imagem real, converte e retorna
                        using (Image imagem = Image.FromFile(ofd.FileName))
                        {
                            return ImageToByteArray(imagem);
                        }
                    }
                    catch (OutOfMemoryException)
                    {
                        // Se não for uma imagem válida (ou for outro tipo de arquivo, como PDF/DOC),
                        // Lê os bytes puros para anexar
                        try
                        {
                            return File.ReadAllBytes(ofd.FileName);
                        }
                        catch (Exception)
                        {
                            
                            return null;
                        }
                    }
                }
            }
            return null;
        }

        // ====================================================================
        // III. MÉTODOS AUXILIARES (CONVERSÃO DE FORMATOS)
        // ====================================================================

        /// <summary>
        /// Pega uma imagem visível na tela (Image) e transforma em "números" (byte[])
        /// para que possa ser salva no banco de dados.
        /// </summary>
        public byte[] ImageToByteArray(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Salva a imagem no MemoryStream usando o formato original dela.
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Faz o caminho inverso: pega os "números" (byte[]) do banco de dados
        /// e transforma de volta em uma imagem que o sistema pode mostrar na tela (Image).
        /// Se não for uma imagem válida, retorna nulo.
        /// </summary>
        public Image ByteArrayToImage(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                
                // Se o formato não for de imagem, ele retornará nulo
                try
                {
                    return Image.FromStream(ms);
                }
                catch (ArgumentException)
                {
                    return null;
                }
            }
        }
    }
}