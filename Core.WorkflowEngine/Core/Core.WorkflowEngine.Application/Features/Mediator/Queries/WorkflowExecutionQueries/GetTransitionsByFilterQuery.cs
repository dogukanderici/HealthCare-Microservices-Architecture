using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkflowExecutionResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkflowExecutionQueries
{
    public class GetTransitionsByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetTransitionsByFilterQueryResult>>>
    {
        public Guid ProcessTaskId { get; set; }
        public Guid ActionId { get; set; }
        public bool IsActive { get; set; }
        public Guid VersionId { get; set; }


        [JsonConstructor]
        private GetTransitionsByFilterQuery()
        {

        }

        public GetTransitionsByFilterQuery Filter(Guid processTaskId, Guid actionId, bool isActive, Guid VersionId) =>
            new GetTransitionsByFilterQuery
            {
                ProcessTaskId = processTaskId,
                ActionId = actionId,
                IsActive = isActive,
                VersionId = VersionId
            };
    }
}