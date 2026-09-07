using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskTransitionDtos;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Interfaces.Services
{
    public interface ITaskTransitionService : IBaseService<ProcessTaskTransition>
    {
        Task<InternalServiceResponse<ProcessTaskTransition>> GetDataByIdAsync(Guid id);
        Task<InternalServiceResponse<IReadOnlyCollection<ProcessTaskTransition>>> GetDatasByFilterAsync(TaskTransitionFilterDto taskTransitionFilterDto);
    }
}
