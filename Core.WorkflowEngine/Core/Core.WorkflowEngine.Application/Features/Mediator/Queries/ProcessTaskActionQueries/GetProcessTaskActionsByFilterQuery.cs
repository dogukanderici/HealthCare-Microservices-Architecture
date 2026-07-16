using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskActionResults;
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

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskActionQueries
{
    public class GetProcessTaskActionsByFilterQuery : IRequest<InternalHandlerResponse<List<GetProcessTaskActionsByFilterQueryResult>>>, ICacheableQuery
    {
        public Guid? ProcessTaskId { get; set; }
        public Guid? ActionId { get; set; }
        public string? ActionName { get; set; }
        public int? ActionType { get; set; }
        public bool? IsActive { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(
            [
                typeof(GetProcessTaskActionsByFilterQuery).Name,
                (ProcessTaskId.HasValue ? ProcessTaskId.ToString() : Guid.Empty.ToString())
            ]
            );

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        [JsonConstructor]
        private GetProcessTaskActionsByFilterQuery()
        {

        }

        public static GetProcessTaskActionsByFilterQuery Filter(Guid? processTaskId, Guid? actionId, string? actionName, int? actionType, bool? isActive) =>
            new GetProcessTaskActionsByFilterQuery
            {
                ProcessTaskId = processTaskId,
                ActionId = actionId,
                ActionName = actionName,
                ActionType = actionType,
                IsActive = isActive
            };
    }
}