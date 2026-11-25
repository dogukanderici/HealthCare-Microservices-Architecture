using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries
{
    public class GetProcessDefinitionCountQuery : IRequest<InternalHandlerResponse<int>>
    {
        public string? ProcessName { get; set; }
        public bool? IsActive { get; set; }


        [JsonConstructor]
        private GetProcessDefinitionCountQuery()
        {

        }

        public static GetProcessDefinitionCountQuery Filter(string? processName, bool? isActive) =>
            new GetProcessDefinitionCountQuery
            {
                ProcessName = processName,
                IsActive = isActive
            };
    }
}