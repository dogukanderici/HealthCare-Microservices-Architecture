using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using Core.WorkflowEngine.Application.Features.Wrappers;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries
{
    public class GetWorkItemByIdQuery : IRequest<InternalHandlerResponse<GetWorkItemByIdQueryResult>>, ICacheableQuery
    {
        public Guid WorkItemId { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey([
            typeof(GetWorkItemsQuery).Name,
            (WorkItemId.ToString())
            ]);

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        public GetWorkItemByIdQuery(Guid workItemId)
        {
            WorkItemId = workItemId;
        }
    }
}