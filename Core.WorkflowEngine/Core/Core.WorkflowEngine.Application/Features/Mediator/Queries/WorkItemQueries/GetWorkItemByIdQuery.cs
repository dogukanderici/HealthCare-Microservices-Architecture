using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries
{
    public class GetWorkItemByIdQuery : IRequest<GetWorkItemByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetWorkItemByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
