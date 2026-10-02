using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskDtos;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService
{
    public interface IProcessTaskQueryService : IBaseQueryService<ProcessTask>
    {
        Task<InternalServiceResponse<TResult>> GetDataByProcessIdAsync<TResult>(Guid processId);
    }
}