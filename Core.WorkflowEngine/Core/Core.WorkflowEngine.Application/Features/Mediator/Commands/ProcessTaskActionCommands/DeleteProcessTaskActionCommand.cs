using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands
{
    public class DeleteProcessTaskActionCommand : IRequest<InternalHandlerResponse<bool>>, ITransactionalRequest
    {
        public Guid Id { get; set; }

        public DeleteProcessTaskActionCommand(Guid id)
        {
            Id = id;
        }
    }
}