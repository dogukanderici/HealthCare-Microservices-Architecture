using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskHandlers
{
    public class GetProcessTaskByIdQueryHandler : IRequestHandler<GetProcessTaskByIdQuery, InternalHandlerResponse<GetProcessTaskByIdQueryResult>>
    {
        private readonly IProcessTaskQueryService _queryService;

        public GetProcessTaskByIdQueryHandler(IProcessTaskQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetProcessTaskByIdQueryResult>> Handle(GetProcessTaskByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetProcessTaskByIdQueryResult> serviceResponse =
                await _queryService.GetDataByIdAsync<GetProcessTaskByIdQueryResult>(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}