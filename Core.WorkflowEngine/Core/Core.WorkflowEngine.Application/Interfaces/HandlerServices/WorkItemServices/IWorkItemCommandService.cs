using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices
{
    public interface IWorkItemCommandService : IBaseCommandService<WorkItem>
    {
    }
}