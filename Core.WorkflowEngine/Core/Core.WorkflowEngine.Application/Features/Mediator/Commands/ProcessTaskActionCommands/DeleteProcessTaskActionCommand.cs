using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands
{
    public class DeleteProcessTaskActionCommand : IRequest<InternalCommandResponse<bool>>
    {
        public Guid Id { get; set; }

        public DeleteProcessTaskActionCommand(Guid id)
        {
            Id = id;
        }
    }
}