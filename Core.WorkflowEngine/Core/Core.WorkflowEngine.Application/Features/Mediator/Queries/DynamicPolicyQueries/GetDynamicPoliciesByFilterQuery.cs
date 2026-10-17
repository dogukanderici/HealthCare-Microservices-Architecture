using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicPolicyResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.DynamicPolicyQueries
{
    public class GetDynamicPoliciesByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetDynamicPoliciesByFilterQueryResult>>>
    {
        public string? Methode { get; set; }
        public string? Endpoint { get; set; }
        public bool? IsActive { get; set; }

        [JsonConstructor]
        private GetDynamicPoliciesByFilterQuery()
        {

        }

        public static GetDynamicPoliciesByFilterQuery Filter(string? methode, string? endpoint, bool? isActive) =>
            new GetDynamicPoliciesByFilterQuery
            {
                Methode = methode,
                Endpoint = endpoint,
                IsActive = isActive
            };
    }
}