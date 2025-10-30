using System;

namespace Gerenciamento_De_Chamados.Models
{
    public class Historico
    {
        public int IdHistorico { get; set; }
        public DateTime DataSolucao { get; set; }
        public string Solucao { get; set; }
        public int FK_IdChamado { get; set; }
        public string Acao { get; set; }
    }
}