using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.QuotaTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Handlers
{
    public class GetQuotaTypesQueryHandler : IRequestHandler<GetQuotaTypesQuery, InternalHandlerResponse<IReadOnlyCollection<GetQuotaTypesQueryResult>>>
    {
        private readonly IQuotaTypeQueryService _queryService;

        public GetQuotaTypesQueryHandler(IQuotaTypeQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetQuotaTypesQueryResult>>> Handle(GetQuotaTypesQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<IReadOnlyCollection<GetQuotaTypesQueryResult>> serviceResult = await _queryService.GetDatasAsync<GetQuotaTypesQueryResult>();

            return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}