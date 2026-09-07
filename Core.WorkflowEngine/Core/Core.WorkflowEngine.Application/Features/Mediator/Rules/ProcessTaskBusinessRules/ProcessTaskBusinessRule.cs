using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskBusinessRules
{
    public class ProcessTaskBusinessRule : IProcessTaskBusinessRule
    {
        private readonly IRepository<ProcessTask> _repository;

        public ProcessTaskBusinessRule(IRepository<ProcessTask> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckExistingDataAsync(DBQueryOptions<ProcessTask> dBQueryOptions)
        {
            int data = await _repository.GetAllDataCountAsync(dBQueryOptions);

            return data == 0;
        }
    }
}