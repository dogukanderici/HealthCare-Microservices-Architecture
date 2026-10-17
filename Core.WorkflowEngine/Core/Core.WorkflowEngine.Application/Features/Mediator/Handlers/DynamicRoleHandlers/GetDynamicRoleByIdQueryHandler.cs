using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.DynamicRoleQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicRoleResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicRoleServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.DynamicRoleHandlers
{
    public class GetDynamicRoleByIdQueryHandler : IRequestHandler<GetDynamicRoleByIdQuery, InternalHandlerResponse<GetDynamicRolesByFilterQueryResult>>
    {
        private readonly IDynamicRoleQueryService _queryService;

        public GetDynamicRoleByIdQueryHandler(IDynamicRoleQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetDynamicRolesByFilterQueryResult>> Handle(GetDynamicRoleByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetDynamicRolesByFilterQueryResult> serviceResponse =
                await _queryService.GetDataByIdAsync<GetDynamicRolesByFilterQueryResult>(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}