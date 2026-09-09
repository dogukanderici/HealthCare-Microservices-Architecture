using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Cities;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Cities.Handlers
{
    public class GetCitiesByFilterQueryHandler : IRequestHandler<GetCitiesByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetCitiesByFilterQueryResult>>>
    {
        private readonly ICityQueryService _cityQueryService;

        public GetCitiesByFilterQueryHandler(ICityQueryService cityQueryService)
        {
            _cityQueryService = cityQueryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetCitiesByFilterQueryResult>>> Handle(GetCitiesByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<City> dBQueryOptions = new DBQueryOptions<City>();
            Expression<Func<City, bool>> filter = x => x.Plate == request.Plate;
            dBQueryOptions.filter = filter;

            InternalServiceResponse<IReadOnlyCollection<GetCitiesByFilterQueryResult>> serviceResult = await _cityQueryService.GetDatasAsync<GetCitiesByFilterQueryResult>(dBQueryOptions);

            return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}