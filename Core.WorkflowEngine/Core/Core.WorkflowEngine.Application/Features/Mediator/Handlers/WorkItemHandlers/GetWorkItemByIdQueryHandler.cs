using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class GetWorkItemByIdQueryHandler : IRequestHandler<GetWorkItemByIdQuery, InternalHandlerResponse<GetWorkItemByIdQueryResult>>
    {
        private readonly IWorkItemQueryService _queryService;

        public GetWorkItemByIdQueryHandler(IWorkItemQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetWorkItemByIdQueryResult>> Handle(GetWorkItemByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetWorkItemByIdQueryResult> serviceResponse =
                await _queryService.GetDataByIdAsync<GetWorkItemByIdQueryResult>(request.WorkItemId);

            return serviceResponse.ToHandlerResponse();
        }
    }
}