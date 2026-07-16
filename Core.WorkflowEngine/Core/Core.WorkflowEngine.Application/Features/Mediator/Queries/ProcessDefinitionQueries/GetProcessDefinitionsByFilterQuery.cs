using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Wrappers;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries
{
    public class GetProcessDefinitionsByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>>>, ICacheableQuery
    {
        public string? ProcessName { get; set; }
        public bool? IsActive { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(
            [
                typeof(GetProcessDefinitionsByFilterQuery).Name,
                ProcessName,
                (IsActive.HasValue ? "true":"false")
            ]
        );

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        [JsonConstructor]
        private GetProcessDefinitionsByFilterQuery()
        {

        }

        public static GetProcessDefinitionsByFilterQuery Filter(string? processName, bool? isActive) =>
            new GetProcessDefinitionsByFilterQuery
            {
                ProcessName = processName,
                IsActive = isActive
            };
    }
}