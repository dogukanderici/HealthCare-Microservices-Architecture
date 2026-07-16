using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkflowExecutionResults;
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

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkflowExecutionQueries
{
    public class GetTransitionsByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetTransitionsByFilterQueryResult>>>, ICacheableQuery
    {
        public Guid ProcessTaskId { get; set; }
        public Guid ActionId { get; set; }
        public bool IsActive { get; set; }
        public Guid VersionId { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey([
            typeof(GetTransitionsByFilterQuery).Name,
            (ProcessTaskId.ToString()),
            (ActionId.ToString()),
            (VersionId.ToString()),
            ]);

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        [JsonConstructor]
        private GetTransitionsByFilterQuery()
        {

        }

        public GetTransitionsByFilterQuery Filter(Guid processTaskId, Guid actionId, bool isActive, Guid VersionId) =>
            new GetTransitionsByFilterQuery
            {
                ProcessTaskId = processTaskId,
                ActionId = actionId,
                IsActive = isActive,
                VersionId = VersionId
            };
    }
}