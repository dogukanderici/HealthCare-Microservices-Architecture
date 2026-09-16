using HealthCare.Descriptions.Application.Common.Helpers;
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
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
            DBQueryOptions<City> dBQueryOptions = new DBQueryOptions<City>();
            Expression<Func<City, object>> orderBy = x => x.Plate;
            bool isForward = true;

            dBQueryOptions.DataTakeNumber = 10;

            if (request.PaginationToken != null)
            {
                // TODO - Secret Key appsetting'ten alınmalı.
                CursorTokenPayload<int> tokenData = DecryptionHelper.DecryptToken<CursorTokenPayload<int>>(request.PaginationToken, "12345678abcdefgh87654321ABCDEFGH");

                isForward = tokenData.IsForward;

                if (tokenData.IsForward)
                {
                    Expression<Func<City, bool>> filter = x => (
                        (x.Plate > tokenData.LastData)
                    );

                    dBQueryOptions.filter = filter;
                    dBQueryOptions.sortingType = 1;
                }
                else
                {
                    Expression<Func<City, bool>> filter = x => (
                        (x.Plate < tokenData.FirstData)
                    );

                    dBQueryOptions.filter = filter;
                    dBQueryOptions.sortingType = 0;
                }

                dBQueryOptions.DataTakeNumber = tokenData.TakenCount;
            }

            dBQueryOptions.orderBy = orderBy;

            InternalServiceResponse<IReadOnlyCollection<GetCitiesQueryResult>> serviceResult = await _cityQueryService.GetDatasAsync<GetCitiesQueryResult>(dBQueryOptions);

            if (!isForward)
            {
                serviceResult.Data = serviceResult.Data.Reverse().ToList();
            }

            return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}