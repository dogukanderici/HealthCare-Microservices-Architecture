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
            CursorTokenPayload<int, GetCitiesQueryHandler> tokenPayload = new CursorTokenPayload<int, GetCitiesQueryHandler>();
            bool isForward = true;

            dBQueryOptions.DataTakeNumber = 10;

            if (request.Token != null)
            {
                // TODO - Secret Key appsetting'ten alınmalı.
                CursorTokenPayload<int, GetCitiesQueryHandler> tokenData =
                    DecryptionHelper.DecryptToken<CursorTokenPayload<int, GetCitiesQueryHandler>>(request.Token, "12345678abcdefgh87654321ABCDEFGH");

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
            else
            {
                // Paging Token yoksa toplam veriyi bir kez olmak üzere bulur.
                InternalServiceResponse<int> totalCount = await _cityQueryService.GetDataCountAsync();
                tokenPayload.TotalCount = totalCount.Data;
            }

            dBQueryOptions.orderBy = orderBy;
            dBQueryOptions.sortingType = 0;

            InternalServiceResponse<IReadOnlyCollection<GetCitiesQueryResult>> serviceResult =
                await _cityQueryService.GetDatasAsync<GetCitiesQueryResult>(dBQueryOptions);

            tokenPayload.IsForward = isForward;
            tokenPayload.FirstData = serviceResult.Data.First().Plate;
            tokenPayload.LastData = serviceResult.Data.Last().Plate;
            tokenPayload.LastCreatedAt = serviceResult.Data.Last().CreatedAt;

            string pagingToken = EncryptionHelper.EncryptToken(tokenPayload, "12345678abcdefgh87654321ABCDEFGH");

            if (!isForward)
            {
                serviceResult.Data = serviceResult.Data.Reverse().ToList();
            }

            return serviceResult.ToHandlerResponse(pagingToken, true);
        }
    }
}