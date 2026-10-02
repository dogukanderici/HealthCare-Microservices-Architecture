using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class GetProcessDefinitionCountQueryHandler : IRequestHandler<GetProcessDefinitionCountQuery, InternalHandlerResponse<int>>
    {
        private readonly IProcessDefinitionQueryService _processDefinitionService;

        public GetProcessDefinitionCountQueryHandler(IProcessDefinitionQueryService processDefinitionService)
        {
            _processDefinitionService = processDefinitionService;
        }

        public async Task<InternalHandlerResponse<int>> Handle(GetProcessDefinitionCountQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();
            dBQueryOptions.filter = x => (
            (!request.ProcessSpecId.HasValue || x.ProcessSpecId == request.ProcessSpecId) &&
            (!request.IsActive.HasValue || x.IsActive == request.IsActive) &&
            (string.IsNullOrEmpty(request.ProcessName) || x.ProcessName == request.ProcessName)
            );

            InternalServiceResponse<int> serviceResult = await _processDefinitionService.GetDataCount(dBQueryOptions);

            return serviceResult.ToHandlerResponse();
        }
    }
}