using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.DynamicPolicyQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicPolicyResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicPolicyServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.DynamicPolicyHandlers
{
    public class GetDynamicPoliciesByFilterQueryHandler : IRequestHandler<GetDynamicPoliciesByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetDynamicPoliciesByFilterQueryResult>>>
    {
        private readonly IDynamicPolicyQueryService _queryService;

        public GetDynamicPoliciesByFilterQueryHandler(IDynamicPolicyQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetDynamicPoliciesByFilterQueryResult>>> Handle(GetDynamicPoliciesByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<DynamicPolicy> dBQueryOptions = new DBQueryOptions<DynamicPolicy>();
            dBQueryOptions.filter = x => (
                (string.IsNullOrEmpty(request.Methode) || x.Methode == request.Methode) &&
                (string.IsNullOrEmpty(request.Endpoint) || x.Endpoint == request.Endpoint) &&
                (!request.IsActive.HasValue || x.IsActive == request.IsActive)
            );

            InternalServiceResponse<IReadOnlyCollection<GetDynamicPoliciesByFilterQueryResult>> serviceResponse =
                await _queryService.GetDatasByFilterAsync<GetDynamicPoliciesByFilterQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}