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
    public class GetDynamicPolicyByIdQueryHandler : IRequestHandler<GetDynamicPolicyByIdQuery, InternalHandlerResponse<GetDynamicPolicyByIdQueryResult>>
    {
        private readonly IDynamicPolicyQueryService _queryService;

        public GetDynamicPolicyByIdQueryHandler(IDynamicPolicyQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetDynamicPolicyByIdQueryResult>> Handle(GetDynamicPolicyByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetDynamicPolicyByIdQueryResult> serviceResponse =
                await _queryService.GetDataAsync<GetDynamicPolicyByIdQueryResult>(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}