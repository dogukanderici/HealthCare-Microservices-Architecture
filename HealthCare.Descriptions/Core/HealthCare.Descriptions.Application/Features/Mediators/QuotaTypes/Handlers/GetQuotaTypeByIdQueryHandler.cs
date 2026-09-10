using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.QuotaTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Handlers
{
    public class GetQuotaTypeByIdQueryHandler : IRequestHandler<GetQuotaTypeByIdQuery, InternalHandlerResponse<GetQuotaTypeByIdQueryResult>>
    {
        private readonly IQuotaTypeQueryService _queryService;

        public GetQuotaTypeByIdQueryHandler(IQuotaTypeQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetQuotaTypeByIdQueryResult>> Handle(GetQuotaTypeByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetQuotaTypeByIdQueryResult> serviceResult = await _queryService.GetDataAsync<GetQuotaTypeByIdQueryResult>(request.Id);

            return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}