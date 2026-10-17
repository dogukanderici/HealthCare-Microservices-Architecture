using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicRoleResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.DynamicRoleQueries
{
    public class GetDynamicRolesByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetDynamicRolesByFilterQueryResult>>>
    {
        public Guid? DynamicPolicyId { get; set; }
        public Guid? AllowedRoleId { get; set; }
        public string? Methode { get; set; }
        public string? Endpoint { get; set; }
        public bool? IsActive { get; set; }

        [JsonConstructor]
        private GetDynamicRolesByFilterQuery()
        {

        }

        public static GetDynamicRolesByFilterQuery Filter(Guid? dynamicRoleId, Guid? allowedRoleId, string? methode, string? endpoint, bool? isActive) =>
            new GetDynamicRolesByFilterQuery
            {
                DynamicPolicyId = dynamicRoleId,
                AllowedRoleId = allowedRoleId,
                Methode = methode,
                Endpoint = endpoint,
                IsActive = isActive
            };
    }
}