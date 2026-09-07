using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries
{
    public class GetProcessDefinitionCountQuery : IRequest<InternalHandlerResponse<GetProcessDefinitionCountQueryResult>>
    {
    }
}
