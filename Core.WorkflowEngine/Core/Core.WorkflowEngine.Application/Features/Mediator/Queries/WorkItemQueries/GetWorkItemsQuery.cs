using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries
{
    public class GetWorkItemsQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetWorkItemsQueryResult>>>
    {
        public Guid InstanceId { get; set; }

        public GetWorkItemsQuery(Guid instanceId)
        {
            InstanceId = instanceId;
        }
    }
}