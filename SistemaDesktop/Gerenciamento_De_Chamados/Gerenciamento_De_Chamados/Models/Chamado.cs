using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Models
{
    public class Chamado
    {
        public int IdChamado { get; set; }
        public string Titulo { get; set; }
        public string PrioridadeChamado { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public DateTime DataChamado { get; set; }
        public string StatusChamado { get; set; }
        public int FK_IdUsuario { get; set; }
        public string PessoasAfetadas { get; set; }
        public string ImpedeTrabalho { get; set; }
        public string OcorreuAnteriormente { get; set; }

        public string PrioridadeSugeridaIA { get; set; }
        public string ProblemaSugeridoIA { get; set; }
        public string SolucaoSugeridaIA { get; set; }
    }
}
