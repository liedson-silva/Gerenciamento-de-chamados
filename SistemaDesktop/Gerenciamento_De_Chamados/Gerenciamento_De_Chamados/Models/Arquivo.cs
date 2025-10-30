using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Models
{
   public class Arquivo
    {
        public int IdArquivo { get; set; }
        public string TipoArquivo { get; set; }
        public string NomeArquivo { get; set; }
        public byte[] ArquivoBytes { get; set; } 
        public int FK_IdChamado { get; set; }
    }
}
