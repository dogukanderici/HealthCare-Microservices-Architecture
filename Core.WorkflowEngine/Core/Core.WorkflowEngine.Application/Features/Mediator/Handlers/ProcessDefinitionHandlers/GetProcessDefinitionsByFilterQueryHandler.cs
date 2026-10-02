using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class GetProcessDefinitionsByFilterQueryHandler : IRequestHandler<GetProcessDefinitionsByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>>>
    {
        private readonly IProcessDefinitionQueryService _processDefinitionService;

        public GetProcessDefinitionsByFilterQueryHandler(IProcessDefinitionQueryService processDefinitionService)
        {
            _processDefinitionService = processDefinitionService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>>> Handle(GetProcessDefinitionsByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            dBQueryOptions.filter = x => (
                (!request.ProcessSpecId.HasValue || x.ProcessSpecId == request.ProcessSpecId) &&
                (!request.IsActive.HasValue || x.IsActive == request.IsActive) &&
                (string.IsNullOrEmpty(request.ProcessName) || x.ProcessName.Contains(request.ProcessName))
            );

            InternalServiceResponse<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>> serviceResponse =
                await _processDefinitionService.GetDatasByFilterAsync<GetProcessDefinitionsByFilterQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}