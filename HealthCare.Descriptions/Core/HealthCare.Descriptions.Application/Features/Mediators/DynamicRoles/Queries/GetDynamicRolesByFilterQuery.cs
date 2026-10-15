using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Queries
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

        public static GetDynamicRolesByFilterQuery Filter(Guid? dynamicPolicyId, Guid? allowedRolewId, string? methode, string? endpoint, bool? isActive) =>
            new GetDynamicRolesByFilterQuery
            {
                DynamicPolicyId = dynamicPolicyId,
                AllowedRoleId = allowedRolewId,
                Methode = methode,
                Endpoint = endpoint,
                IsActive = isActive
            };
    }
}