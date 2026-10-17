using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Commands.DynamicRoleCommands
{
    public class DeleteDynamicRoleCommand : IRequest<InternalHandlerResponse<bool>>
    {
        public Guid Id { get; set; }

        public DeleteDynamicRoleCommand(Guid id)
        {
            Id = id;
        }
    }
}