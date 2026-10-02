using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskTransitionHandlers
{
    public class GetProcessTaskTransitionsByFilterQueryHandler : IRequestHandler<GetProcessTaskTransitionsByFilterQuery,
        InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskTransitionsByFilterQueryResult>>>
    {
        private readonly ITaskTransitionQueryService _queryService;

        public GetProcessTaskTransitionsByFilterQueryHandler(ITaskTransitionQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskTransitionsByFilterQueryResult>>> Handle(GetProcessTaskTransitionsByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = new DBQueryOptions<ProcessTaskTransition>();
            dBQueryOptions.filter = x => (
                (!request.ProcessTaskId.HasValue || x.ProcessTaskId == request.ProcessTaskId) &&
                (!request.ActionId.HasValue || x.ActionId == request.ActionId) &&
                (!request.IsActive.HasValue || x.IsActive == request.IsActive)
            );

            InternalServiceResponse<IReadOnlyCollection<GetProcessTaskTransitionsByFilterQueryResult>> serviceResponse =
                await _queryService.GetDatasByFilterAsync<GetProcessTaskTransitionsByFilterQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}