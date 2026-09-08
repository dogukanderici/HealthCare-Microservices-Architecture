using Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskTransitionDtos;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices
{
    public interface ITaskTransitionQueryService : IBaseQueryService<ProcessTaskTransition, TaskTransitionFilterDto>
    {
    }
}