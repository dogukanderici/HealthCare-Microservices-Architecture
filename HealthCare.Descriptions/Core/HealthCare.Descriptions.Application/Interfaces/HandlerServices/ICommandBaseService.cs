using HealthCare.Descriptions.Application.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces.HandlerServices
{
    public interface ICommandBaseService<T>
    {
        Task<InternalServiceResponse<T>> GetDataForUpdateAsync(Guid id);
        Task<InternalServiceResponse<Guid>> CreateAsync(T entity);
        Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(T entity);
        Task<InternalServiceResponse<bool>> RemoveAsync(Guid id);
    }
}
