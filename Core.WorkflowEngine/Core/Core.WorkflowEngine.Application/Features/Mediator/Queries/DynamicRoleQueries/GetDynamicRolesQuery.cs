using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicRoleResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.DynamicRoleQueries
{
    public class GetDynamicRolesQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesQueryResult>>>
    {
        public Guid DynamicPolicyId { get; set; }

        public GetDynamicRolesQuery(Guid dynamicPolicyId)
        {
            DynamicPolicyId = dynamicPolicyId;
        }
    }
}