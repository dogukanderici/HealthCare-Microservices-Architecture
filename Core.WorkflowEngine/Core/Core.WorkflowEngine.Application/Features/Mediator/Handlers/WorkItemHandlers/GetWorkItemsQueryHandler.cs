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
    public class GetWorkItemsQueryHandler : IRequestHandler<GetWorkItemsQuery, InternalHandlerResponse<IReadOnlyCollection<GetWorkItemsQueryResult>>>
    {
        private readonly IWorkItemQueryService _queryService;

        public GetWorkItemsQueryHandler(IWorkItemQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetWorkItemsQueryResult>>> Handle(GetWorkItemsQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();
            dBQueryOptions.filter = x => x.InstanceId == request.InstanceId;

            InternalServiceResponse<IReadOnlyCollection<GetWorkItemsQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetWorkItemsQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}