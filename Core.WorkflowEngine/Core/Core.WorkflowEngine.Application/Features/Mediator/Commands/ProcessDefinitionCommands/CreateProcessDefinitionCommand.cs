using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands
{
    public class CreateProcessDefinitionCommand : IRequest<InternalHandlerResponse<Guid>>, ITransactionalRequest
    {
        public string ProcessName { get; set; }
        public bool IsActive { get; set; }
    }
}