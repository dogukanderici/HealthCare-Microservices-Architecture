using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.DynamicRoleCommands
{
    public class CreateDynamicRoleCommand : IRequest<InternalHandlerResponse<Guid>>, ITransactionalRequest
    {
        public Guid DynamicPolicyId { get; set; }
        public Guid AllowedRoleId { get; set; }
        public bool IsActive { get; set; }
    }
}