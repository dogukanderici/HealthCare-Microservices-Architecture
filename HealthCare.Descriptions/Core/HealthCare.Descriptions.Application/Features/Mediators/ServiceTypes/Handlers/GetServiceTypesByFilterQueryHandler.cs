using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Helpers;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.ServiceType;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Handlers
{
    public class GetServiceTypesByFilterQueryHandler : IRequestHandler<GetServiceTypesByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetServiceTypesByFilterQueryResult>>>
    {
        private readonly IServiceTypeQueryService _queryService;
        private readonly ITokenBasedPaginationHelper<ServicingType, GetServiceTypesByFilterQueryHandler, GetServiceTypesByFilterQueryResult, string> _paginationHelper;

        public GetServiceTypesByFilterQueryHandler(IServiceTypeQueryService queryService, ITokenBasedPaginationHelper<ServicingType, GetServiceTypesByFilterQueryHandler, GetServiceTypesByFilterQueryResult, string> paginationHelper)
        {
            _queryService = queryService;
            _paginationHelper = paginationHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetServiceTypesByFilterQueryResult>>> Handle(GetServiceTypesByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ServicingType> dBQueryOptions = new DBQueryOptions<ServicingType>();
            Expression<Func<ServicingType, bool>> filter = x => (
                (string.IsNullOrEmpty(request.ServiceCode) || x.ServiceCode == request.ServiceCode) &&
                (string.IsNullOrEmpty(request.ServiceName) || x.ServiceName == request.ServiceName)
            );
            dBQueryOptions.filter = filter;

            var config = new TokenPayloadConfig<ServicingType, GetServiceTypesByFilterQueryHandler, GetServiceTypesByFilterQueryResult, string>
            {
                Token = request.Token,

                OrderBy = x => x.ServiceName,
                ForwardFilter = lastName => x => string.Compare(x.ServiceName, lastName) > 0,
                BackwardFilter = firstName => x => string.Compare(x.ServiceName, firstName) < 0,

                CursorSelector = x => x.ServiceName,
                CreatedAtSelector = x => x.CreatedAt,

                GetTotalCountAsync = async () =>
                {
                    InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync();

                    return serviceResponse.Data;
                },

                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetServiceTypesByFilterQueryResult>> serviceResponse =
                        await _queryService.GetDatasAsync<GetServiceTypesByFilterQueryResult>(options);

                    return serviceResponse;
                }
            };

            return await _paginationHelper.PaginationResultAsync(config, dBQueryOptions);

            //InternalServiceResponse<IReadOnlyCollection<GetServiceTypesByFilterQueryResult>> serviceResult = await _queryService.GetDatasAsync<GetServiceTypesByFilterQueryResult>(dBQueryOptions);

            //return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}