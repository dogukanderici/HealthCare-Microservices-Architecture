using Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskDtos;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService
{
    public interface IProcessTaskQueryService /*: IBaseQueryService<ProcessTask, ProcessTaskFilterDto>*/
    {
        Task<ProcessTask> GetDataByProcessIdAsync(Guid processId);
    }
}