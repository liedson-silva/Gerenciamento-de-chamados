using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Models
{
    /// <summary>
    /// Esta classe é um modelo de dados para criar os GRÁFICOS no sistema.
    /// Ela organiza os dados que vêm do banco para o formato que a biblioteca de gráficos entende.
    /// </summary>
    public class ChartDataPoint
    {
        // ====================================================================
        // I. PROPRIEDADES DE GRÁFICO
        // ====================================================================

        /// <summary>
        /// O RÓTULO ou nome do item no gráfico.
        /// (Ex: 'Pendente', 'Resolvido' ou 'Hardware', 'Software').
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// O VALOR numérico usado para desenhar a altura da barra ou o tamanho da fatia do gráfico.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// A contagem inteira de itens para aquela categoria.
        /// (Ex: O número total de chamados 'Pendente').
        /// </summary>
        public int Count { get; set; }
    }
}