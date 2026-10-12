using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.DynamicRoleQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicRoleResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicRoleServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.DynamicRoleHandlers
{
    public class GetDynamicRolesByFilterQueryHandler : IRequestHandler<GetDynamicRolesByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesByFilterQueryResult>>>
    {
        private readonly IDynamicRoleQueryService _queryService;

        public GetDynamicRolesByFilterQueryHandler(IDynamicRoleQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesByFilterQueryResult>>> Handle(GetDynamicRolesByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<DynamicRole> dBQueryOptions = new DBQueryOptions<DynamicRole>();
            dBQueryOptions.filter = x => (
                (!request.DynamicPolicyId.HasValue || x.DynamicPolicyId == request.DynamicPolicyId) &&
                (!request.AllowedRoleId.HasValue || x.AllowedRoleId == request.AllowedRoleId) &&
                (!request.IsActive.HasValue || x.IsActive == request.IsActive)
            );

            InternalServiceResponse<IReadOnlyCollection<GetDynamicRolesByFilterQueryResult>> serviceResponse =
                await _queryService.GetDatasByFilterAsync<GetDynamicRolesByFilterQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}