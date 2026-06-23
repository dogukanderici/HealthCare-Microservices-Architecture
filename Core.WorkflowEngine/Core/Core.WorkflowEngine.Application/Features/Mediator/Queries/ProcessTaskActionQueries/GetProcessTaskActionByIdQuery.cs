using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskActionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskActionQueries
{
    public class GetProcessTaskActionByIdQuery : IRequest<GetProcessTaskActionByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetProcessTaskActionByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}