using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskQueries
{
    public class GetProcessTasksQuery : IRequest<InternalHandlerResponse<List<GetProcessTasksQueryResult>>>
    {
        public Guid ProcessId { get; set; }

        public GetProcessTasksQuery(Guid processId)
        {
            ProcessId = processId;
        }
    }
}
