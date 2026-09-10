using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Policlinics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Handlers
{
    public class GetPoliclinicByIdQueryHandler : IRequestHandler<GetPoliclinicByIdQuery, InternalHandlerResponse<GetPoliclinicByIdQueryResult>>
    {
        private readonly IPoliclinicQueryService _queryService;

        public GetPoliclinicByIdQueryHandler(IPoliclinicQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetPoliclinicByIdQueryResult>> Handle(GetPoliclinicByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetPoliclinicByIdQueryResult> serviceResponse = await _queryService.GetDataAsync<GetPoliclinicByIdQueryResult>(request.Id);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}
