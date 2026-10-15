using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Queries
{
    public class GetDynamicRolesQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesQueryResult>>>
    {
        public Guid DynamicPolicyId { get; set; }

        public GetDynamicRolesQuery(Guid dynamicPolicyId)
        {
            DynamicPolicyId = dynamicPolicyId;
        }
    }
}