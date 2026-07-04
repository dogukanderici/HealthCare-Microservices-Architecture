using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands
{
    public class CreateProcessTaskActionCommand : IRequest<InternalHandlerResponse<Guid>>, ITransactionalRequest
    {
        public Guid ProcessTaskId { get; set; }
        public Guid AssignedUser { get; set; }
        public string ActionName { get; set; }
        public int ActionType { get; set; }
        public int ExecutionOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
