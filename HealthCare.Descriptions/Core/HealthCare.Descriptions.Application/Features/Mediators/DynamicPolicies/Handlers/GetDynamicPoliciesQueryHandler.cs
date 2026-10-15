using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicPolicies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Handlers
{
    public class GetDynamicPoliciesQueryHandler : IRequestHandler<GetDynamicPoliciesQuery, InternalHandlerResponse<IReadOnlyCollection<GetDynamicPoliciesQueryResult>>>
    {
        private readonly IDynamicPolicyQueryService _queryService;

        public GetDynamicPoliciesQueryHandler(IDynamicPolicyQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetDynamicPoliciesQueryResult>>> Handle(GetDynamicPoliciesQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<IReadOnlyCollection<GetDynamicPoliciesQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetDynamicPoliciesQueryResult>();

            return serviceResponse.ToHandlerResponse();
        }
    }
}