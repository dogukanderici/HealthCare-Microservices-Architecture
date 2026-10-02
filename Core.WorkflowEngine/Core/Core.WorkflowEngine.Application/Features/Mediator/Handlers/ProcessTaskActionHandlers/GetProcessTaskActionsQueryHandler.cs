using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskActionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskActionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskActionServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskActionHandlers
{
    public class GetProcessTaskActionsQueryHandler : IRequestHandler<GetProcessTaskActionsQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskActionsQueryResult>>>
    {
        private readonly IProcessTaskActionQueryService _queryService;

        public GetProcessTaskActionsQueryHandler(IProcessTaskActionQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskActionsQueryResult>>> Handle(GetProcessTaskActionsQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTaskAction> dBQueryOptions = new DBQueryOptions<ProcessTaskAction>();

            InternalServiceResponse<IReadOnlyCollection<GetProcessTaskActionsQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetProcessTaskActionsQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}