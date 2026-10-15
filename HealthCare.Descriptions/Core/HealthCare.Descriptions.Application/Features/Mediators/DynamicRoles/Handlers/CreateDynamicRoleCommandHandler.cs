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
    public class CreateDynamicRoleCommandHandler : IRequestHandler<CreateDynamicRoleCommand, InternalHandlerResponse<Guid>>
    {
        public Task<InternalHandlerResponse<Guid>> Handle(CreateDynamicRoleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
