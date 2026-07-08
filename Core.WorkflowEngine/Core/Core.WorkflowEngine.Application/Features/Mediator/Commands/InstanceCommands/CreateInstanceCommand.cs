using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands
{
    public class CreateInstanceCommand : IRequest<InternalHandlerResponse<Guid>>
    {
        public Guid ProcessId { get; set; }
        public Guid TaskId { get; set; }
    }
}