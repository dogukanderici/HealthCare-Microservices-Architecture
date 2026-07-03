using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands
{
    public class CreateInstanceExecutionCommand : IRequest<InternalHandlerResponse<Guid>>
    {
        public Guid TaskId { get; set; }
        public Guid VersionId { get; set; } = Guid.NewGuid();
    }
}