using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskTransitionCommands
{
    public class UpdateProcessTaskTransitionCommand : IRequest<InternalHandlerResponse<DateTimeOffset>>, ITransactionalRequest
    {
        public Guid Id { get; set; }
        public Guid ProcessTaskId { get; set; }
        public Guid NextTaskId { get; set; }
        public Guid ActionId { get; set; }
        public string ConditionExpression { get; set; }
        public bool IsActive { get; set; }
    }
}