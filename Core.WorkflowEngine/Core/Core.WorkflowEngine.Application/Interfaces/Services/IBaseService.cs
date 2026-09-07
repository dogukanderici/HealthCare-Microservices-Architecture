using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Domain.Abstractions;

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