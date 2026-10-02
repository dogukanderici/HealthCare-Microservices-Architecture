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
    public class GetProcessTaskActionsByFilterQueryHandler : IRequestHandler<GetProcessTaskActionsByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskActionsByFilterQueryResult>>>
    {
        private readonly IProcessTaskActionQueryService _queryService;

        public GetProcessTaskActionsByFilterQueryHandler(IProcessTaskActionQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskActionsByFilterQueryResult>>> Handle(GetProcessTaskActionsByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTaskAction> dBQueryOptions = new DBQueryOptions<ProcessTaskAction>();
            dBQueryOptions.filter = x => (
                (!request.ProcessTaskId.HasValue || x.ProcessTaskId == request.ProcessTaskId) &&
                (request.ActionId.HasValue || x.ActionId == request.ActionId) &&
                (!string.IsNullOrEmpty(request.ActionName) || x.ActionName == request.ActionName) &&
                (!request.ActionType.HasValue || x.ActionType == request.ActionType) &&
                (request.IsActive.HasValue || x.IsActive == request.IsActive)
            );

            InternalServiceResponse<IReadOnlyCollection<GetProcessTaskActionsByFilterQueryResult>> serviceResponse =
                await _queryService.GetDatasByFilterAsync<GetProcessTaskActionsByFilterQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}