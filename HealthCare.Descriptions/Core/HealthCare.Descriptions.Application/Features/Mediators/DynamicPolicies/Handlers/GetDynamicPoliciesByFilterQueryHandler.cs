using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicPolicies;
using HealthCare.Descriptions.Domain.Entities;
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
        private readonly IDynamicPolicyQueryService _queryService;

        public GetDynamicPoliciesByFilterQueryHandler(IDynamicPolicyQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetDynamicPoliciesByFilterQueryResult>>> Handle(GetDynamicPoliciesByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<DynamicPolicy> dBQueryOptions = new DBQueryOptions<DynamicPolicy>();
            dBQueryOptions.filter = x => (
                (string.IsNullOrEmpty(request.Methode) || x.Methode == request.Methode) &&
                (string.IsNullOrEmpty(request.Endpoint) || x.Endpoint == request.Endpoint) &&
                (!request.IsAvailable.HasValue || x.IsAvailable == request.IsAvailable)
            );

            InternalServiceResponse<IReadOnlyCollection<GetDynamicPoliciesByFilterQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetDynamicPoliciesByFilterQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}