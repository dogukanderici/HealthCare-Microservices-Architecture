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
    public class GetDynamicRolesByFilterQueryHandler : IRequestHandler<GetDynamicRolesByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesByFilterQueryResult>>>
    {
        public Task<InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesByFilterQueryResult>>> Handle(GetDynamicRolesByFilterQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}