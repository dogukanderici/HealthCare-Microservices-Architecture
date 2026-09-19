using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.Districts.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.Districts.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Helpers;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Districts;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Districts.Handlers
{
    public class GetDistrictsQueryHandler : IRequestHandler<GetDistrictsQuery, InternalHandlerResponse<IReadOnlyCollection<GetDistrictsQueryResult>>>
    {
        private readonly IDistrictQueryService _queryService;

        public GetDistrictsQueryHandler(IDistrictQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetDistrictsQueryResult>>> Handle(GetDistrictsQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<District> dBQueryOptions = new DBQueryOptions<District>();
            List<Expression<Func<District, object>>> includes = [
                x=>x.City
                ];
            dBQueryOptions.includes = includes;

            var config = new TokenPayloadConfig<District, GetDistrictsQueryHandler, GetDistrictsQueryResult, string>
            {
                Token = request.Token,

                OrderBy = x => x.DistrictName,
                ForwardFilter = lastName => x => string.Compare(x.DistrictName, lastName) > 0,
                BackwardFilter = firstName => x => string.Compare(x.DistrictName, firstName) < 0,

                CursorSelector = x => x.DistrictName,
                CreatedAtSelector = x => x.CreatedAt,

                GetTotalCountAsync = async () =>
                {
                    InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync();

                    return serviceResponse.Data;
                },

                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetDistrictsQueryResult>> serviceResult =
                        await _queryService.GetDatasAsync<GetDistrictsQueryResult>(options);

                    return serviceResult;
                }
            };

            return await TokenBasedPaginationHelper.PaginationResultAsync(config, dBQueryOptions);


            //InternalServiceResponse<IReadOnlyCollection<GetDistrictsQueryResult>> serviceResult = await _queryService.GetDatasAsync<GetDistrictsQueryResult>(dBQueryOptions);

            //return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}