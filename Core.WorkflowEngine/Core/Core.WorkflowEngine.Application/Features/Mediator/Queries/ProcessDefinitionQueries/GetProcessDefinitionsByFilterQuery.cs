using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries
{
    public class GetProcessDefinitionsByFilterQuery : IRequest<List<GetProcessDefinitionsByFilterQueryResult>>
    {
        public string? ProcessName { get; set; }
        public bool? IsActive { get; set; }

        private GetProcessDefinitionsByFilterQuery()
        {

        }

        public static GetProcessDefinitionsByFilterQuery Filter(string? ProcessName, bool? isActive) =>
            new GetProcessDefinitionsByFilterQuery
            {
                ProcessName = ProcessName,
                IsActive = isActive
            };
    }
}