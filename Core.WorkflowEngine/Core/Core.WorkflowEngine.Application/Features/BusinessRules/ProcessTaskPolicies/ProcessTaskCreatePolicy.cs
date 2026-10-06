using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskPolicies
{
    public class ProcessTaskCreatePolicy : IProcessTaskCreatePolicy
    {
        private readonly IRepository<ProcessTask> _repository;

        public ProcessTaskCreatePolicy(IRepository<ProcessTask> repository)
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