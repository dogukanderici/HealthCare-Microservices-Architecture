using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries
{
    public class GetProcessTaskTransitionsByFilterQuery : IRequest<InternalHandlerResponse<List<GetProcessTaskTransitionsByFilterQueryResult>>>
    {
        public Guid? ProcessTaskId { get; set; }
        public Guid? ActionId { get; set; }
        public bool? IsActive { get; set; }


        [JsonConstructor]
        private GetProcessTaskTransitionsByFilterQuery()
        {

        }

        public static GetProcessTaskTransitionsByFilterQuery Filter(Guid? processTaskId, Guid? actionId, bool? isActive) =>
            new GetProcessTaskTransitionsByFilterQuery
            {
                ProcessTaskId = processTaskId,
                ActionId = actionId,
                IsActive = isActive
            };
    }
}