using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class GetWorkItemCountQueryHandler : IRequestHandler<GetWorkItemCountQuery, InternalHandlerResponse<int>>
    {
        private readonly IWorkItemQueryService _queryService;

        public GetWorkItemCountQueryHandler(IWorkItemQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<int>> Handle(GetWorkItemCountQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();
            dBQueryOptions.filter = x => x.InstanceId == request.InstanceId;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
