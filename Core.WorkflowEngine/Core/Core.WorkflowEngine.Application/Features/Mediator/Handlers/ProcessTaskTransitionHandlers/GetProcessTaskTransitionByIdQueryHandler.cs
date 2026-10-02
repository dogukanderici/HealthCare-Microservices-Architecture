using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskTransitionHandlers
{
    public class GetProcessTaskTransitionByIdQueryHandler : IRequestHandler<GetProcessTaskTransitionByIdQuery, InternalHandlerResponse<GetProcessTaskTransitionByIdQueryResult>>
    {
        private readonly ITaskTransitionQueryService _queryService;

        public GetProcessTaskTransitionByIdQueryHandler(ITaskTransitionQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetProcessTaskTransitionByIdQueryResult>> Handle(GetProcessTaskTransitionByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetProcessTaskTransitionByIdQueryResult> serviceResponse =
                await _queryService.GetDataByIdAsync<GetProcessTaskTransitionByIdQueryResult>(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}