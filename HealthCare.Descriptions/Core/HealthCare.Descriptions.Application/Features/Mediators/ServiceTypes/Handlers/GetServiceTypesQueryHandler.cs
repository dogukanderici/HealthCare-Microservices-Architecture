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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Handlers
{
    public class GetServiceTypesQueryHandler : IRequestHandler<GetServiceTypesQuery, InternalHandlerResponse<IReadOnlyCollection<GetServiceTypesQueryResult>>>
    {
        private readonly IServiceTypeQueryService _queryService;
        private readonly ITokenBasedPaginationHelper<ServicingType, GetServiceTypesQueryHandler, GetServiceTypesQueryResult, string> _paginationHelper;

        public GetServiceTypesQueryHandler(IServiceTypeQueryService queryService, ITokenBasedPaginationHelper<ServicingType, GetServiceTypesQueryHandler, GetServiceTypesQueryResult, string> paginationHelper)
        {
            _queryService = queryService;
            _paginationHelper = paginationHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetServiceTypesQueryResult>>> Handle(GetServiceTypesQuery request, CancellationToken cancellationToken)
        {
            var config = new TokenPayloadConfig<ServicingType, GetServiceTypesQueryHandler, GetServiceTypesQueryResult, string>
            {
                Token = request.Token,

                OrderBy = x => x.ServiceName,
                ForwardFilter = lastName => x => string.Compare(x.ServiceName, lastName) > 0,
                BackwardFilter = firstName => x => string.Compare(x.ServiceName, firstName) < 0,

                CursorSelector = x=>x.ServiceName,
                CreatedAtSelector = x=>x.CreatedAt,

                GetTotalCountAsync = async () =>
                {
                    InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync();

                    return serviceResponse.Data;
                },

                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetServiceTypesQueryResult>> serviceResponse =
                        await _queryService.GetDatasAsync<GetServiceTypesQueryResult>(options);

                    return serviceResponse;
                }
            };

            return await _paginationHelper.PaginationResultAsync(config);

            //InternalServiceResponse<IReadOnlyCollection<GetServiceTypesQueryResult>> serviceResult = await _queryService.GetDatasAsync<GetServiceTypesQueryResult>();

            //return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}