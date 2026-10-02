using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices
{
    public interface IWorkItemQueryService : IBaseQueryService<WorkItem>
    {
        Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetWorkItemByFilterAsync<TResult>(DBQueryOptions<WorkItem> options);
    }
}