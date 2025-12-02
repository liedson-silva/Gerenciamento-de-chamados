using Gerenciamento_De_Chamados.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Repositories
{
    /// <summary>
    /// Interface para o Repositório de Relatórios.
    /// Define as operações de consulta para geração de relatórios detalhados e dados para gráficos.
    /// </summary>
    public interface IRelatorioRepository
    {
        // ====================================================================
        // I. RELATÓRIO DETALHADO
        // ====================================================================

        /// <summary>
        /// Gera um relatório detalhado dos chamados em um intervalo de datas.
        /// Este método é usado para alimentar o <see cref="FormRelatorioDetalhado"/> para visualização e exportação em PDF.
        /// </summary>
        /// <param name="dataInicio">Data de início do filtro.</param>
        /// <param name="dataFim">Data de fim do filtro.</param>
        /// <returns>Um DataTable contendo os dados detalhados do relatório.</returns>
        Task<DataTable> GerarRelatorioDetalhadoAsync(DateTime dataInicio, DateTime dataFim);


        // ====================================================================
        // II. DADOS PARA GRÁFICOS (ChartDataPoint)
        // ====================================================================

        /// <summary>
        /// Busca dados de contagem de chamados agrupados por Status.
        /// Usado para gerar o gráfico de Visão Geral por Status (Pizza ou Colunas).
        /// </summary>
        /// <returns>Uma lista de objetos <see cref="ChartDataPoint"/> (Rótulo/Valor).</returns>
        Task<List<ChartDataPoint>> GetStatusChartDataAsync();

        /// <summary>
        /// Busca dados de contagem de chamados agrupados por Prioridade.
        /// Usado para gerar o gráfico de Prioridades.
        /// </summary>
        /// <returns>Uma lista de objetos <see cref="ChartDataPoint"/> (Rótulo/Valor).</returns>
        Task<List<ChartDataPoint>> GetPriorityChartDataAsync();
    }
}