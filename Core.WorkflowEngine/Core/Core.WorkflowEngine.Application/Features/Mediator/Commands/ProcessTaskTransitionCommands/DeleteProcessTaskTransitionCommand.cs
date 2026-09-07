using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskTransitionCommands
{
    public class DeleteProcessTaskTransitionCommand : IRequest<InternalHandlerResponse<bool>>, ITransactionalRequest
    {
        public Guid Id { get; set; }

        public DeleteProcessTaskTransitionCommand(Guid id)
        {
            Id = id;
        }
    }
}