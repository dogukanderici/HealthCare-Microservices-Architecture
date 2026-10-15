using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Handlers
{
    public class GetDynamicRolesQueryHandler : IRequestHandler<GetDynamicRolesQuery, InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesQueryResult>>>
    {
        public Task<InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesQueryResult>>> Handle(GetDynamicRolesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
