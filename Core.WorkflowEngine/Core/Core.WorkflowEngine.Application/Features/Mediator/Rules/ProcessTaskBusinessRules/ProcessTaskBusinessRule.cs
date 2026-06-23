using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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