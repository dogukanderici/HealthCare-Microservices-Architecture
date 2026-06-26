using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskActionResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskActionQueries
{
    public class GetProcessTaskActionsByFilterQuery : IRequest<InternalHandlerResponse<List<GetProcessTaskActionsByFilterQueryResult>>>
    {
        public Guid? ProcessTaskId { get; set; }
        public Guid? ActionId { get; set; }
        public string? ActionName { get; set; }
        public int? ActionType { get; set; }
        public bool? IsActive { get; set; }

        private GetProcessTaskActionsByFilterQuery()
        {

        }

        public static GetProcessTaskActionsByFilterQuery Filter(Guid? processTaskId, Guid? actionId, string? actionName, int? actionType, bool? isActive) =>
            new GetProcessTaskActionsByFilterQuery
            {
                ProcessTaskId = processTaskId,
                ActionId = actionId,
                ActionName = actionName,
                ActionType = actionType,
                IsActive = isActive
            };
    }
}