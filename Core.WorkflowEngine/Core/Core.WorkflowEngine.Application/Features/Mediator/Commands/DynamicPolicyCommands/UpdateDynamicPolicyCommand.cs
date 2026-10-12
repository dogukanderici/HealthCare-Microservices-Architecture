using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.DynamicPolicyCommands
{
    public class UpdateDynamicPolicyCommand : IRequest<InternalHandlerResponse<DateTimeOffset>>
    {
        public Guid Id { get; set; }
        public string Methode { get; set; }
        public string Endpoint { get; set; }
        public bool IsActive { get; set; }
    }
}