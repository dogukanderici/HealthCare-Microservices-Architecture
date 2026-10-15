using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Queries
{
    public class GetDynamicPoliciesByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetDynamicPoliciesByFilterQueryResult>>>
    {
        public string? Methode { get; set; }
        public string? Endpoint { get; set; }
        public bool? IsAvailable { get; set; }

        [JsonConstructor]
        private GetDynamicPoliciesByFilterQuery()
        {

        }

        public static GetDynamicPoliciesByFilterQuery Filter(string? methode, string? endpoint, bool? isActive) =>
            new GetDynamicPoliciesByFilterQuery
            {
                Methode = methode,
                Endpoint = endpoint,
                IsAvailable = isActive
            };
    }
}