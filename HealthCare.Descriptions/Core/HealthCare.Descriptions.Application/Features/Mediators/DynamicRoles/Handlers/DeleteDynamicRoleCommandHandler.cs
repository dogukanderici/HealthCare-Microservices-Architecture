using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Handlers
{
    public class DeleteDynamicRoleCommandHandler : IRequestHandler<DeleteDynamicRoleCommand, InternalHandlerResponse<bool>>
    {
        public Task<InternalHandlerResponse<bool>> Handle(DeleteDynamicRoleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
