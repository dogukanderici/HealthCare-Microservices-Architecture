using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries
{
    public class GetProcessTaskTransitionByIdQuery : IRequest<InternalHandlerResponse<GetProcessTaskTransitionByIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetProcessTaskTransitionByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
