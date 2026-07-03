using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Services
{
    public class ProcessDefinitionService : IProcessDefinitionService
    {
        private readonly IRepository<ProcessDefinition> _repository;

        public ProcessDefinitionService(IRepository<ProcessDefinition> respository)
        {
            _repository = respository;
        }

        public async Task<ProcessDefinition> GetDataById(Guid id)
        {
            DBQueryOptions<ProcessDefinition> dbQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => x.Id == id;
            dbQueryOptions.filter = filter;

            return await _repository.GetDataAsync(dbQueryOptions);
        }
        public Task<InternalServiceResponse<Guid>> CreateAsync(ProcessDefinition entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(ProcessDefinition entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}