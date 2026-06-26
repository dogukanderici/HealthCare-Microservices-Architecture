using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries
{
    public class GetProcessDefinitionByIdQuery : IRequest<InternalHandlerResponse<GetProcessDefinitionByIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetProcessDefinitionByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
