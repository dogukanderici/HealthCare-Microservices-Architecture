using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries
{
    public class GetProcessDefinitionsByFilterQuery : IRequest<InternalHandlerResponse<List<GetProcessDefinitionsByFilterQueryResult>>>
    {
        public string? ProcessName { get; set; }
        public bool? IsActive { get; set; }

        [JsonConstructor]
        private GetProcessDefinitionsByFilterQuery()
        {

        }

        public static GetProcessDefinitionsByFilterQuery Filter(string? processName, bool? isActive) =>
            new GetProcessDefinitionsByFilterQuery
            {
                ProcessName = processName,
                IsActive = isActive
            };
    }
}