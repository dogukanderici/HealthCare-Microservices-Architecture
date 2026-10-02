using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Domain.Abstractions;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Interfaces.Services
{
    public interface IBaseCommandService<T>
        where T : class, IEntity
    {

        Task<InternalServiceResponse<T>> GetDataForUpdateAsync(Guid id);
        Task<InternalServiceResponse<Guid>> CreateAsync(T entity, CancellationToken cancellationToken);
        Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(T entity);
        Task<InternalServiceResponse<bool>> DeleteAsync(Guid id);
    }
}