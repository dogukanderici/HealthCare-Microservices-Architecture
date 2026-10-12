using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.DynamicPolicyQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicPolicyResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicPolicyServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.DynamicPolicyHandlers
{
    public class GetDynamicPolicyByIdQueryHandler : IRequestHandler<GetDynamicPolicyByIdQuery, InternalHandlerResponse<GetDynamicPolicyByIdQueryResult>>
    {
        private readonly IDynamicPolicyQueryService _queryService;

        public GetDynamicPolicyByIdQueryHandler(IDynamicPolicyQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetDynamicPolicyByIdQueryResult>> Handle(GetDynamicPolicyByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetDynamicPolicyByIdQueryResult> serviceResponse = await _queryService.GetDataByIdAsync<GetDynamicPolicyByIdQueryResult>(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}