using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskActionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskActionQueries
{
    public class GetProcessTaskActionsQuery : IRequest<List<GetProcessTaskActionsQueryResult>>
    {
    }
}
