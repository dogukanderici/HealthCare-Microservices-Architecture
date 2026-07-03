using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessDefinitionBusinessRules
{
    public interface IProcessDefinitionBusinessRule
    {
        Task<bool> ExistingProcessDefinitionDataAsync(Guid id);
    }
}
