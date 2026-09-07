using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands
{
    public class CreateInstanceExecutionCommand : IRequest<InternalHandlerResponse<Guid>>, ITransactionalRequest
    {
        public Guid ProcessId { get; set; }
        //public Guid TaskId { get; set; }
    }
}