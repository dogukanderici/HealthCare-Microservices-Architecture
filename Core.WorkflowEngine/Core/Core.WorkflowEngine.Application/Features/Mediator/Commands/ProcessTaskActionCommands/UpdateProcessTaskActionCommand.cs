using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands
{
    public class UpdateProcessTaskActionCommand : IRequest<InternalCommandResponse<DateTimeOffset>>
    {
        public Guid Id { get; set; }
        public Guid ProcessTaskId { get; set; }
        public Guid ActionId { get; set; }
        public string ActionName { get; set; }
        public int ActionType { get; set; }
        public int ExecutionOrder { get; set; }
        public bool IsActive { get; set; }
    }
}