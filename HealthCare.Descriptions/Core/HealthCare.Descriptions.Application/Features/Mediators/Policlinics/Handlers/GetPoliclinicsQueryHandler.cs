using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Helpers;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Policlinics;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Handlers
{
    public class GetPoliclinicsQueryHandler : IRequestHandler<GetPoliclinicsQuery, InternalHandlerResponse<IReadOnlyCollection<GetPoliclinicsQueryResult>>>
    {
        private readonly IPoliclinicQueryService _queryService;
        private readonly ITokenBasedPaginationHelper<Policlinic, GetPoliclinicsQueryHandler, GetPoliclinicsQueryResult, string> _paginationHelper;

        public GetPoliclinicsQueryHandler(IPoliclinicQueryService queryService, ITokenBasedPaginationHelper<Policlinic, GetPoliclinicsQueryHandler, GetPoliclinicsQueryResult, string> paginationHelper)
        {
            _queryService = queryService;
            _paginationHelper = paginationHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetPoliclinicsQueryResult>>> Handle(GetPoliclinicsQuery request, CancellationToken cancellationToken)
        {
            var config = new TokenPayloadConfig<Policlinic, GetPoliclinicsQueryHandler, GetPoliclinicsQueryResult, string>
            {
                Token = request.Token,

                OrderBy = x => x.PoliclinicName,
                ForwardFilter = lastName => x => string.Compare(x.PoliclinicName, lastName) > 0,
                BackwardFilter = firstName => x => string.Compare(x.PoliclinicName, firstName) < 0,

                CursorSelector = x => x.PoliclinicName,
                CreatedAtSelector = X => X.CreatedAt,

                GetTotalCountAsync = async (options) =>
                {
                    InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(options);

                    return serviceResponse.Data;
                },

                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetPoliclinicsQueryResult>> serviceResponse = await _queryService.GetDatasAsync<GetPoliclinicsQueryResult>(options);

                    return serviceResponse;
                }
            };

            return await _paginationHelper.PaginationResultAsync(config);

            //InternalServiceResponse<IReadOnlyCollection<GetPoliclinicsQueryResult>> serviceResponse = await _queryService.GetDatasAsync<GetPoliclinicsQueryResult>();

            //return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}
