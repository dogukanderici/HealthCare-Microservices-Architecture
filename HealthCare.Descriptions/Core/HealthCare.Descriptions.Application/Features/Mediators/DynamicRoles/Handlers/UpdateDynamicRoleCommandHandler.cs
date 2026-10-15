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
    public class UpdateDynamicRoleCommandHandler : IRequestHandler<UpdateDynamicRoleCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        public Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateDynamicRoleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
