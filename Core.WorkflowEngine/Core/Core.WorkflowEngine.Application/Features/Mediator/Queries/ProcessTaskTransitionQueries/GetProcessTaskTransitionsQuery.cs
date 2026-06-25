using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries
{
    public class GetProcessTaskTransitionsQuery : IRequest<List<GetProcessTaskTransitionsQueryResult>>
    {
    }
}
