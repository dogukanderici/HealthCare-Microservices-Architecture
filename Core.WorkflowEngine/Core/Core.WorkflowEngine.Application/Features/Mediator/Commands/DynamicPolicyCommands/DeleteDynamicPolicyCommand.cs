using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.DynamicPolicyCommands
{
    public class DeleteDynamicPolicyCommand : IRequest<InternalHandlerResponse<bool>>
    {
        public Guid Id { get; set; }

        public DeleteDynamicPolicyCommand(Guid id)
        {
            Id = id;
        }
    }
}