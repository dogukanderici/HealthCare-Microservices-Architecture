using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskCommands
{
    public class CreateProcessTaskCommand : IRequest<InternalHandlerResponse<Guid>>, ITransactionalRequest
    {
        public Guid ProcessId { get; set; }
        public Guid AssignedUser { get; set; }
        public string StepName { get; set; }
        public bool IsStartStep { get; set; }
        public bool IsActive { get; set; }
        public int VersionNumber { get; set; }
    }
}
