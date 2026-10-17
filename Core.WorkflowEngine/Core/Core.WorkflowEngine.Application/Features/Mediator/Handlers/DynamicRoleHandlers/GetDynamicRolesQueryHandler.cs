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
    public class GetDynamicRolesQueryHandler : IRequestHandler<GetDynamicRolesQuery, InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesQueryResult>>>
    {
        private readonly IDynamicRoleQueryService _queryService;

        public GetDynamicRolesQueryHandler(IDynamicRoleQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesQueryResult>>> Handle(GetDynamicRolesQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<IReadOnlyCollection<GetDynamicRolesQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetDynamicRolesQueryResult>();

            return serviceResponse.ToHandlerResponse();
        }
    }
}