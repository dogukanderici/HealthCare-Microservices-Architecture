using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries
{
    public class GetInstancesQuery : IRequest<List<GetInstancesQueryResult>>
    {
    }
}
