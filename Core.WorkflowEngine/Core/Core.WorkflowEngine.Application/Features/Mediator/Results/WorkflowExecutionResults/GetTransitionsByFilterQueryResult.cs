using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Results.WorkflowExecutionResults
{
    public record GetTransitionsByFilterQueryResult(

        Guid ProcessTaskId,
        Guid NextTaskId,
        Guid ActionId,
        bool IsActive
     );
}