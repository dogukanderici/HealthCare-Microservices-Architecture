using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.DynamicPolicyQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicPolicyResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicPolicyServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.DynamicPolicyHandlers
{
    public class GetDynamicPoliciesQueryHandler : IRequestHandler<GetDynamicPoliciesQuery, InternalHandlerResponse<IReadOnlyCollection<GetDynamicPoliciesQueryResult>>>
    {
        private readonly IDynamicPolicyQueryService _queryService;

        public GetDynamicPoliciesQueryHandler(IDynamicPolicyQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetDynamicPoliciesQueryResult>>> Handle(GetDynamicPoliciesQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<IReadOnlyCollection<GetDynamicPoliciesQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetDynamicPoliciesQueryResult>();

            return serviceResponse.ToHandlerResponse();
        }
    }
}