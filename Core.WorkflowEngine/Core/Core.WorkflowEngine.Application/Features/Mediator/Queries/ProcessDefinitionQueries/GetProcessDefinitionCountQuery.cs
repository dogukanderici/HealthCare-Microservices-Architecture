using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using MediatR;
using System.Text.Json.Serialization;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries
{
    public class GetProcessDefinitionCountQuery : IRequest<InternalHandlerResponse<int>>
    {
        public Guid? ProcessSpecId { get; set; }
        public string? ProcessName { get; set; }
        public bool? IsActive { get; set; }


        [JsonConstructor]
        private GetProcessDefinitionCountQuery()
        {

        }

        public static GetProcessDefinitionCountQuery Filter(Guid? processSpecId, string? processName, bool? isActive) =>
            new GetProcessDefinitionCountQuery
            {
                ProcessSpecId = processSpecId,
                ProcessName = processName,
                IsActive = isActive
            };
    }
}