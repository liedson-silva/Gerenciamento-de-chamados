using System;
using System.Data;
using System.Threading.Tasks;

namespace Gerenciamento_De_Chamados.Repositories
{
    public interface IRelatorioRepository
    {
        
        Task<DataTable> GerarRelatorioDetalhadoAsync(DateTime dataInicio, DateTime dataFim);
    }
}