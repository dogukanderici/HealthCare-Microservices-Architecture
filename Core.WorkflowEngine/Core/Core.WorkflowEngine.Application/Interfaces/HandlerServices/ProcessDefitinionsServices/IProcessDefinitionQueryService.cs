using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices
{
    public interface IProcessDefinitionQueryService : IBaseQueryService<ProcessDefinition>
    {
        public Task<InternalServiceResponse<TResult>> GetDataForLastestVersionAsync<TResult>(Guid processSpecId);
    }
}