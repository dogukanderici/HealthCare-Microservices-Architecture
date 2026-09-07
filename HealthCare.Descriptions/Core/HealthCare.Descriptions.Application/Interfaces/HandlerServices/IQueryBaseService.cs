using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces.HandlerServices
{
    public interface IQueryBaseService<T>
        where T : class
    {
        Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<T>? options = null) where TResult : IListResult;
        Task<InternalServiceResponse<TResult>> GetDataAsync<TResult>(Guid id) where TResult : ISingleResult;
        Task<InternalServiceResponse<int>> GetDataCountAsync(DBQueryOptions<T>? options = null);
    }
}