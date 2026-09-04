using HealthCare.Descriptions.Application.Common.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces.HandlerServices
{
    public interface IBaseService<T,TResult>
        where T : class
        where TResult : class
    {
        Task<IReadOnlyCollection<TResult>> GetDatasAsync(DBQueryOptions<T>? options = null);
        Task<TResult> GetDataAsync(DBQueryOptions<T>? options = null);
        Task<int> GetDataCountAsync(DBQueryOptions<T>? options = null);
    }
}