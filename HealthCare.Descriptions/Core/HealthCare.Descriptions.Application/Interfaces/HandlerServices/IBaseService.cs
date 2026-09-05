using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces.HandlerServices
{
    public interface IBaseService<T, TResult>
        where T : class
        where TResult : class
    {
        Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync(DBQueryOptions<T>? options = null);
        Task<InternalServiceResponse<TResult>> GetDataAsync(DBQueryOptions<T>? options = null);
        Task<InternalServiceResponse<int>> GetDataCountAsync(DBQueryOptions<T>? options = null);
    }
}