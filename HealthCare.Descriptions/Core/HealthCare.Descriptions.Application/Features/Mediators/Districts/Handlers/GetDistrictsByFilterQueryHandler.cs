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
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Districts.Handlers
{
    public class GetDistrictsByFilterQueryHandler : IRequestHandler<GetDistrictsByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetDistrictsByFilterQueryResult>>>
    {
        private readonly IDistrictQueryService _queryService;

        public GetDistrictsByFilterQueryHandler(IDistrictQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetDistrictsByFilterQueryResult>>> Handle(GetDistrictsByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<District> dBQueryOptions = new DBQueryOptions<District>();
            Expression<Func<District, bool>> filter = x => x.Plate == request.Plate;
            dBQueryOptions.filter = filter;

            List<Expression<Func<District, object>>> includes = [
                x=>x.City
                ];
            dBQueryOptions.includes = includes;

            var config = new TokenPayloadConfig<District, GetDistrictsByFilterQueryHandler, GetDistrictsByFilterQueryResult, DateTimeOffset>
            {
                Token = request.Token,

                OrderBy = x => x.DistrictName,
                ForwardFilter = lastDate => x => x.CreatedAt > lastDate,
                BackwardFilter = firstDate => x => x.CreatedAt < firstDate,

                CursorSelector = x => x.CreatedAt,
                CreatedAtSelector = x => x.CreatedAt,

                GetTotalCountAsync = async () =>
                {
                    InternalServiceResponse<int> serviceResult = await _queryService.GetDataCountAsync();

                    return serviceResult.Data;
                },

                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetDistrictsByFilterQueryResult>> serviceResult =
                    await _queryService.GetDatasAsync<GetDistrictsByFilterQueryResult>(dBQueryOptions);

                    return serviceResult;
                }
            };

            return await TokenBasedPaginationHelper.PaginationResultAsync(config, dBQueryOptions);

            //InternalServiceResponse<IReadOnlyCollection<GetDistrictsByFilterQueryResult>> serviceResult = await _queryService.GetDatasAsync<GetDistrictsByFilterQueryResult>(dBQueryOptions);

            //return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}