using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService
{
    public interface IProcessTaskCommandService : IBaseCommandService<ProcessTask>
    {
    }
}