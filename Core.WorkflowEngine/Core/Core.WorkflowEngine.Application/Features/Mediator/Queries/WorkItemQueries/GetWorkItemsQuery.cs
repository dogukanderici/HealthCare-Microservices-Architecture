using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using Core.WorkflowEngine.Application.Features.Wrappers;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries
{
    public class GetWorkItemsQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetWorkItemsQueryResult>>>, ICacheableQuery
    {
        public Guid InstanceId { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey([
            typeof(GetWorkItemsQuery).Name,
            (InstanceId.ToString())
            ]);

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        public GetWorkItemsQuery(Guid instanceId)
        {
            InstanceId = instanceId;
        }
    }
}