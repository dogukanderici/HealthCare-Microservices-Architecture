using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands
{
    public class UpdateInstanceCommand : IRequest<InternalCommandResponse<DateTimeOffset>>
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public int Number { get; set; }
        public Guid InitiatorWorkItemId { get; set; }
        public int Status { get; set; }
    }
}
