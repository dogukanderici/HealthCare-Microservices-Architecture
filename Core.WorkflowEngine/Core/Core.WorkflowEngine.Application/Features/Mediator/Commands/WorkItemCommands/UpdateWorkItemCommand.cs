using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands
{
    public class UpdateWorkItemCommand : IRequest<InternalHandlerResponse<DateTimeOffset>>, ITransactionalRequest
    {
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        public Guid StepId { get; set; }
        public Guid AssignedUserId { get; set; }
        public Guid AssignedRoleId { get; set; }
        public Guid SelectedAction { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
        public Guid CompletedBy { get; set; }
        public int Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
