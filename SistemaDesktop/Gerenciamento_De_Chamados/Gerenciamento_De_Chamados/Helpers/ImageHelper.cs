using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Web;


namespace Gerenciamento_De_Chamados.Helpers
{
    public class ImageHelper 
    {
        public string UltimoNomeArquivo { get; private set; }
        public string UltimoTipoArquivo { get; private set; }

        public byte[] SelecionarArquivoEConverter()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Guarda o nome e tipo
                    this.UltimoNomeArquivo = Path.GetFileName(ofd.FileName);
                    this.UltimoTipoArquivo = MimeMapping.GetMimeMapping(ofd.FileName); 

                    using (Image imagem = Image.FromFile(ofd.FileName))
                    {
                        return ImageToByteArray(imagem);
                    }
                }
            }
            return null;
        }

        public byte[] ImageToByteArray(Image img) {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }
        public Image ByteArrayToImage(byte[] bytes) {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
