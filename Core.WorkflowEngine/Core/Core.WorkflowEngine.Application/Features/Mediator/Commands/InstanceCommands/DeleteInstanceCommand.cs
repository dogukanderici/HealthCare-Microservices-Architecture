using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands
{
    public class DeleteInstanceCommand : IRequest<InternalCommandResponse<bool>>, ITransactionalRequest
    {
        public Guid Id { get; set; }

        public DeleteInstanceCommand(Guid id)
        {
            Id = id;
        }
    }
}
