using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Handlers
{
    public class GetDynamicPoliciesByFilterQueryHandler : IRequestHandler<GetDynamicPoliciesByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetDynamicPoliciesByFilterQueryResult>>>
    {
        public Task<InternalHandlerResponse<IReadOnlyCollection<GetDynamicPoliciesByFilterQueryResult>>> Handle(GetDynamicPoliciesByFilterQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
