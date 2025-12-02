using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Models
{
    /// <summary>
    /// Esta classe representa a tabela de 'Histórico' no banco de dados.
    /// Ela é essencial para sabermos quem fez o quê, e quando, em cada chamado (Auditoria).
    /// </summary>
    public class Historico
    {
        // ====================================================================
        // I. CHAVES E IDENTIFICAÇÃO
        // ====================================================================

        /// <summary>
        /// ID Único do registro de histórico (chave primária).
        /// </summary>
        public int IdHistorico { get; set; }

        /// <summary>
        /// Chave estrangeira que liga este registro ao chamado correto.
        /// </summary>
        public int FK_IdChamado { get; set; }


        // ====================================================================
        // II. DADOS DO REGISTRO
        // ====================================================================

        /// <summary>
        /// A data e hora exata em que esta ação (solução/atualização) foi realizada.
        /// </summary>
        public DateTime DataSolucao { get; set; }

        /// <summary>
        /// A descrição da solução aplicada pelo técnico.
        /// Este é o campo que os futuros técnicos vão consultar para resolver problemas parecidos.
        /// </summary>
        public string Solucao { get; set; }

        /// <summary>
        /// O tipo de ação registrada.
        /// (Ex: 'Solução Aplicada', 'Chamado Aberto', 'Status Atualizado').
        /// </summary>
        public string Acao { get; set; }
    }
}