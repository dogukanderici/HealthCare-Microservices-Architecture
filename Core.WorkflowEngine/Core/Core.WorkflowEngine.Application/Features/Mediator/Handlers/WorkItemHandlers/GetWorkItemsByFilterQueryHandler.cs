using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class GetWorkItemsByFilterQueryHandler : IRequestHandler<GetWorkItemsByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetWorkItemsByFilterQueryResult>>>
    {
        private readonly IWorkItemQueryService _queryService;

        public GetWorkItemsByFilterQueryHandler(IWorkItemQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetWorkItemsByFilterQueryResult>>> Handle(GetWorkItemsByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();
            dBQueryOptions.filter = x => (
                (!request.InstanceId.HasValue || x.InstanceId == request.InstanceId) &&
                (!request.WorkItemId.HasValue || x.Id == request.WorkItemId) &&
                (!request.AssignedUserId.HasValue || x.AssignedUserId == request.AssignedUserId) &&
                (!request.Status.HasValue || x.Status == request.Status) &&
                (!request.CreatedAt.HasValue || x.CreatedAt == request.CreatedAt)
            );

            InternalServiceResponse<IReadOnlyCollection<GetWorkItemsByFilterQueryResult>> serviceResponse =
                await _queryService.GetDatasByFilterAsync<GetWorkItemsByFilterQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}