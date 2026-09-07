using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands
{
    public class CreateInstanceCommand : IRequest<InternalHandlerResponse<Guid>>
    {
        public Guid ProcessId { get; set; }
        public Guid TaskId { get; set; }
    }
}