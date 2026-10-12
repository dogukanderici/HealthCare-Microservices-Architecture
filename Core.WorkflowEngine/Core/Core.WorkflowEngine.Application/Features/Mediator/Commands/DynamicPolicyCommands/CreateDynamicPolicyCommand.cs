using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.DynamicPolicyCommands
{
    public class CreateDynamicPolicyCommand : IRequest<InternalHandlerResponse<Guid>>, ITransactionalRequest
    {
        public string Methode { get; set; }
        public string Endpoint { get; set; }
        public bool IsActive { get; set; }
    }
}