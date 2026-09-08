using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessDefinitionDtos;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices
{
    public interface IProcessDefinitionQueryService : IBaseQueryService<ProcessDefinition, ProcessDefinitionFilterDto>
    {
        public Task<InternalServiceResponse<ProcessDefinition>> GetDataForLastestVersionAsync(Guid processSpecId);
    }
}