using Gerenciamento_De_Chamados.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Repositories
{
    public interface IRelatorioRepository
    {
        
        Task<DataTable> GerarRelatorioDetalhadoAsync(DateTime dataInicio, DateTime dataFim);
        Task<List<ChartDataPoint>> GetStatusChartDataAsync();
        Task<List<ChartDataPoint>> GetPriorityChartDataAsync();
    }
}