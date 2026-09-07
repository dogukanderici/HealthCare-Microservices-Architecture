using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands
{
    public class CommitWorkItemExecutionCommand : IRequest<InternalHandlerResponse<Guid>>, ITransactionalRequest
    {
        public Guid InstanceId { get; set; }
        public Guid WorkItemId { get; set; }
        public Guid ProcessTaskId { get; set; }
        public Guid ActionId { get; set; }
    }
}