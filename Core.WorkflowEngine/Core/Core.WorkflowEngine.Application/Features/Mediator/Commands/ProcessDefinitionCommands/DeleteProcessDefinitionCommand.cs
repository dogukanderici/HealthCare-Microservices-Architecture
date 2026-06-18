using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands
{
    public class DeleteProcessDefinitionCommand : IRequest<InternalCommandResponse<bool>>
    {
        public Guid Id { get; set; }

        public DeleteProcessDefinitionCommand(Guid id)
        {
            Id = id;
        }
    }
}
