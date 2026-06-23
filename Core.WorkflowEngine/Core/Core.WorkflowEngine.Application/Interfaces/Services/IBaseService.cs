using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces.Services
{
    public interface IBaseService<T>
        where T : class, IEntity
    {

        Task<InternalServiceResponse<Guid>> CreateAsync(T entity, CancellationToken cancellationToken);
        Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<InternalServiceResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}