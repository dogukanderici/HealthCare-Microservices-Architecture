using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskQueries
{
    public class GetProcessTaskByIdQuery : IRequest<GetProcessTaskByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetProcessTaskByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}