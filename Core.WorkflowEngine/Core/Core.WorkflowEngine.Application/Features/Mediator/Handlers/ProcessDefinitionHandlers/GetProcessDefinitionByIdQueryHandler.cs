using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class GetProcessDefinitionByIdQueryHandler : IRequestHandler<GetProcessDefinitionByIdQuery, InternalHandlerResponse<GetProcessDefinitionByIdQueryResult>>
    {
        private readonly IProcessDefinitionQueryService _processDefinitionService;

        public GetProcessDefinitionByIdQueryHandler(IProcessDefinitionQueryService processDefinitionService)
        {
            _processDefinitionService = processDefinitionService;
        }

        public async Task<InternalHandlerResponse<GetProcessDefinitionByIdQueryResult>> Handle(GetProcessDefinitionByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetProcessDefinitionByIdQueryResult> serviceResponse =
                await _processDefinitionService.GetDataByIdAsync<GetProcessDefinitionByIdQueryResult>(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}