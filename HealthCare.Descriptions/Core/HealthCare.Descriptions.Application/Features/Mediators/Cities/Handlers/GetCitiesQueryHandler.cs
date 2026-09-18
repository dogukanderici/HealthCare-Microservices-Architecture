using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Helpers;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Cities;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;

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
            var config = new TokenPayloadConfig<City, GetCitiesQueryHandler, GetCitiesQueryResult, int>
            {
                Token = request.Token,

                OrderBy = x => x.Plate,
                ForwardFilter = lastPlate => x => x.Plate > lastPlate,
                BackwardFilter = firstPlate => x => x.Plate < firstPlate,

                CursorSelector = x => x.Plate,
                CreatedAtSelector = x => x.CreatedAt,

                GetTotalCountAsync = async () =>
                {
                    InternalServiceResponse<int> serviceResult = await _cityQueryService.GetDataCountAsync();
                    return serviceResult.Data;
                },

                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetCitiesQueryResult>> serviceResult =
                        await _cityQueryService.GetDatasAsync<GetCitiesQueryResult>(options);

                    return serviceResult;
                }
            };

            return await TokenBasedPaginationHelper.PaginationResultAsync(config);
        }
    }
}