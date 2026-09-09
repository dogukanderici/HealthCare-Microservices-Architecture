using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Cities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Cities.Handlers
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, InternalHandlerResponse<IReadOnlyCollection<GetCitiesQueryResult>>>
    {
        private readonly ICityQueryService _cityQueryService;

        public GetCitiesQueryHandler(ICityQueryService cityQueryService)
        {
            _cityQueryService = cityQueryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetCitiesQueryResult>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<IReadOnlyCollection<GetCitiesQueryResult>> serviceResult = await _cityQueryService.GetDatasAsync<GetCitiesQueryResult>();

            return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}