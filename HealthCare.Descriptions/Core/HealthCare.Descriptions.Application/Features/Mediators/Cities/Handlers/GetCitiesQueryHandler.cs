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
            bool isLastPage = false;
            string pagingToken = "";

            // Son sayfada olup olunmadığını anlamak için N+1 taktiği uygulanır.
            dBQueryOptions.DataTakeNumber = tokenPayload.TakenCount + 1;

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

            // Örneğin 10 veri almak istenirse 11 tane veri getir sorgusu yazılır. Eğer 11 veri dönerse en az bir sayfa daha veri var demektir.
            // Eğer 10 veya daha az dönerse son sayfada olunduğu anlaşılır.
            if (serviceResult.Data.Count > tokenPayload.TakenCount)
            {
                isLastPage = false;
                List<GetCitiesQueryResult> dataList = serviceResult.Data.ToList();
                dataList.RemoveAt(serviceResult.Data.Count - 1);
                serviceResult.Data = dataList;

                tokenPayload.LastData = serviceResult.Data.Last().Plate;
                tokenPayload.LastCreatedAt = serviceResult.Data.Last().CreatedAt;

                pagingToken = EncryptionHelper.EncryptToken(tokenPayload, "12345678abcdefgh87654321ABCDEFGH");
            }
            else
            {
                isLastPage = true;
            }

            if (!isForward)
            {
                serviceResult.Data = serviceResult.Data.Reverse().ToList();
            }

            return serviceResult.ToHandlerResponse(pagingToken, isLastPage);
        }
    }
}