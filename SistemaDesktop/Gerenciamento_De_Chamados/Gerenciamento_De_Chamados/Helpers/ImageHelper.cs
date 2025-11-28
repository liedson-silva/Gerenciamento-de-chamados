using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Windows.Forms;


namespace Gerenciamento_De_Chamados.Helpers
{
    public class ImageHelper
    {
        public string UltimoNomeArquivo { get; private set; }
        public string UltimoTipoArquivo { get; private set; }

        // CORREÇÃO: Recebe IWin32Window (o formulário pai) para evitar que a tela minimize.
        public byte[] SelecionarArquivoEConverter(IWin32Window owner)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                // Deixa claro que ele suporta mais tipos, mas prioriza imagens
                ofd.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.bmp|Todos os Arquivos|*.*";

                // Passa o formulário pai para o ShowDialog()
                if (ofd.ShowDialog(owner) == DialogResult.OK)
                {
                    // Guarda o nome e tipo
                    this.UltimoNomeArquivo = Path.GetFileName(ofd.FileName);
                    // Adicionei uma verificação para evitar erro de referência nula
                    this.UltimoTipoArquivo = MimeMapping.GetMimeMapping(this.UltimoNomeArquivo) ?? "application/octet-stream";

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
                        // podemos simplesmente ler os bytes puros para anexar
                        try
                        {
                            return File.ReadAllBytes(ofd.FileName);
                        }
                        catch (Exception)
                        {
                            // Falha na leitura, retorna nulo
                            return null;
                        }
                    }
                }
            }
            return null;
        }

        public byte[] ImageToByteArray(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }
        public Image ByteArrayToImage(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                // Este método é mais seguro para tentar ler imagens
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