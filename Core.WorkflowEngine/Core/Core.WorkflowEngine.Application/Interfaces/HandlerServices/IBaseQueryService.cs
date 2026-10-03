using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Domain.Abstractions;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices
{
    public interface IBaseQueryService<T>
        where T : class, IEntity
    {
        public Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<T>? options = null);
        public Task<InternalServiceResponse<TResult>> GetDataByIdAsync<TResult>(Guid id);
        public Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasByFilterAsync<TResult>(DBQueryOptions<T> options);
        public Task<InternalServiceResponse<int>> GetDataCount(DBQueryOptions<T> options);
    }
}