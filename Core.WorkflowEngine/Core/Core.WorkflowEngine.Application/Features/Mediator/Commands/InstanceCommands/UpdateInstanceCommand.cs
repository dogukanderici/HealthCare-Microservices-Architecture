using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands
{
    public class UpdateInstanceCommand : IRequest<InternalHandlerResponse<DateTimeOffset>>, ITransactionalRequest
    {
        public Guid Id { get; set; }
        public Guid ProcessId { get; set; }
        public Guid TaskId { get; set; }
        public int Number { get; set; }
        public Guid InitiatorWorkItemId { get; set; }
        public int Status { get; set; }
    }
}
