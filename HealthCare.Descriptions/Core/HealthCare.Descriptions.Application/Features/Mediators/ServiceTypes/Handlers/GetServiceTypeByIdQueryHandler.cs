using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.ServiceType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Handlers
{
    public class GetServiceTypeByIdQueryHandler : IRequestHandler<GetServiceTypeByIdQuery, InternalHandlerResponse<GetServiceTypeByIdQueryResult>>
    {
        private readonly IServiceTypeQueryService _queryService;

        public GetServiceTypeByIdQueryHandler(IServiceTypeQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetServiceTypeByIdQueryResult>> Handle(GetServiceTypeByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetServiceTypeByIdQueryResult> serviceResult = await _queryService.GetDataAsync<GetServiceTypeByIdQueryResult>(request.Id);

            return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}