using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands
{
    public class UpdateProcessTaskActionCommand : IRequest<InternalHandlerResponse<DateTimeOffset>>, ITransactionalRequest
    {
        public Guid Id { get; set; }
        public Guid ProcessTaskId { get; set; }
        public Guid AssignedUser { get; set; }
        public Guid ActionId { get; set; }
        public string ActionName { get; set; }
        public int ActionType { get; set; }
        public int ExecutionOrder { get; set; }
        public bool IsActive { get; set; }
    }
}