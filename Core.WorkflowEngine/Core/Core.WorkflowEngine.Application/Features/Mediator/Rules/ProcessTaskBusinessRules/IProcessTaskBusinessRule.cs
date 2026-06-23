using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskBusinessRules
{
    public interface IProcessTaskBusinessRule
    {
        Task<bool> CheckExistingDataAsync(DBQueryOptions<ProcessTask> dBQueryOptions);
    }
}
