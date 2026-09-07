using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.ServiceDtos.WorkItemServiceDtos;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Interfaces.Services
{
    public interface IWorkItemService : IBaseService<WorkItem>
    {
        Task<InternalServiceResponse<WorkItem>> GetWorkItemByIdAsync(Guid id);
        Task<InternalServiceResponse<IReadOnlyCollection<WorkItem>>> GetWorkItemByFilterAsync(WorkItemFilterDto workItemFilterDto);
        Task<InternalServiceResponse<IReadOnlyCollection<WorkItem>>> GetWorkItemByFilterAsync(DBQueryOptions<WorkItem> dBQueryOptions);
    }
}
