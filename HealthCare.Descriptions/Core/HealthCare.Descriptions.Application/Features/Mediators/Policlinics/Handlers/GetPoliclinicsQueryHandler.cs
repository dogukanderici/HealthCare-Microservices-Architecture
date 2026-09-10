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
    public class GetPoliclinicsQueryHandler : IRequestHandler<GetPoliclinicsQuery, InternalHandlerResponse<IReadOnlyCollection<GetPoliclinicsQueryResult>>>
    {
        private readonly IPoliclinicQueryService _queryService;

        public GetPoliclinicsQueryHandler(IPoliclinicQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetPoliclinicsQueryResult>>> Handle(GetPoliclinicsQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<IReadOnlyCollection<GetPoliclinicsQueryResult>> serviceResponse = await _queryService.GetDatasAsync<GetPoliclinicsQueryResult>();

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}
