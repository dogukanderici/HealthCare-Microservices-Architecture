using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.ServiceDtos.WorkItemServiceDtos;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices
{
    public interface IWorkItemQueryService : IBaseQueryService<WorkItem, WorkItemFilterDto>
    {
        Task<InternalServiceResponse<IReadOnlyCollection<WorkItem>>> GetWorkItemByFilterAsync(DBQueryOptions<WorkItem> dBQueryOptions);
    }
}