using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.DynamicRoleQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicRoleResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicRoleServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            DBQueryOptions<DynamicRole> dbQueryOption = new DBQueryOptions<DynamicRole>();
            dbQueryOption.filter = x => x.DynamicPolicyId == request.DynamicPolicyId;

            List<Expression<Func<DynamicRole, object>>> includes = new List<Expression<Func<DynamicRole, object>>>
            {
                x=>x.DynamicPolicy,
                y=>y.SyncedRole
            };

            dbQueryOption.includes = includes;

            InternalServiceResponse<IReadOnlyCollection<GetDynamicRolesQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetDynamicRolesQueryResult>(dbQueryOption);

            return serviceResponse.ToHandlerResponse();
        }
    }
}