using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries
{
    public class GetWorkItemCountQuery : IRequest<int>
    {
    }
}
