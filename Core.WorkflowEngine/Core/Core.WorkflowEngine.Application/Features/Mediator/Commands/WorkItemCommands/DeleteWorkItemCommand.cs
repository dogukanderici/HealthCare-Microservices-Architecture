using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands
{
    public class DeleteWorkItemCommand : IRequest<InternalHandlerResponse<bool>>, ITransactionalRequest
    {
        public Guid Id { get; set; }

        public DeleteWorkItemCommand(Guid id)
        {
            Id = id;
        }

    }
}
