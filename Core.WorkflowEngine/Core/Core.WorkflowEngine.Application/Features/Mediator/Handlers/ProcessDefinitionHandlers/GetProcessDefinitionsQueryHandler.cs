using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class GetProcessDefinitionsQueryHandler : IRequestHandler<GetProcessDefinitionsQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsQueryResult>>>
    {
        private readonly IProcessDefinitionQueryService _processDefinitionService;

        public GetProcessDefinitionsQueryHandler(IProcessDefinitionQueryService processDefinitionService)
        {
            _processDefinitionService = processDefinitionService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsQueryResult>>> Handle(GetProcessDefinitionsQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<IReadOnlyCollection<GetProcessDefinitionsQueryResult>> serviceResponse =
                await _processDefinitionService.GetDatasAsync<GetProcessDefinitionsQueryResult>();

            return serviceResponse.ToHandlerResponse();
        }
    }
}