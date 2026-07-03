using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands
{
    public class CommitWorkItemExecutionCommand : IRequest<InternalHandlerResponse<Guid>>
    {
        public Guid InstanceId { get; set; }
        public Guid WorkItemId { get; set; }
        public Guid AssignedUserId { get; set; }
        public Guid ProcessTaskId { get; set; }
        public Guid ActionId { get; set; }
        public bool IsActive { get; set; }
        public Guid VersionId { get; set; }
    }
}