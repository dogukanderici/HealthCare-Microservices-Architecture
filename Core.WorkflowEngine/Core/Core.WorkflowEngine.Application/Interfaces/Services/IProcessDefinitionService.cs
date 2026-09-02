using Core.WorkflowEngine.Application.ServiceDtos.ProcessDefinitionDtos;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces.Services
{
    public interface IProcessDefinitionService : IBaseService<ProcessDefinition>
    {
        public Task<InternalServiceResponse<IReadOnlyCollection<ProcessDefinition>>> GetDatasAsync();
        public Task<InternalServiceResponse<ProcessDefinition>> GetDataByIdAsync(Guid id);
        public Task<InternalServiceResponse<IReadOnlyCollection<ProcessDefinition>>> GetDatasByFilterAsync(ProcessDefinitionFilterDto filterDto);
        public Task<InternalServiceResponse<ProcessDefinition>> GetDataForLastestVersionAsync(Guid processSpecId);
        public Task<InternalServiceResponse<int>> GetDataCount(ProcessDefinitionFilterDto filterDto);

    }
}
