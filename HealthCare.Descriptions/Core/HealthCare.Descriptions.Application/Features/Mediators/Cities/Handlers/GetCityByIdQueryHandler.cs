using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Cities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Cities.Handlers
{
    public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, InternalHandlerResponse<GetCityByIdQueryResult>>
    {
        private readonly ICityQueryService _cityQueryService;

        public GetCityByIdQueryHandler(ICityQueryService cityQueryService)
        {
            _cityQueryService = cityQueryService;
        }

        public async Task<InternalHandlerResponse<GetCityByIdQueryResult>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetCityByIdQueryResult> serviceResult = await _cityQueryService.GetDataAsync<GetCityByIdQueryResult>(request.Id);

            return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}