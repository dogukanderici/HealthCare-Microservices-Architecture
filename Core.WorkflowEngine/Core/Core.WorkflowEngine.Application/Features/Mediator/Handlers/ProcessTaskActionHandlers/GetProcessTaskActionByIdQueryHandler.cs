using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskActionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskActionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskActionServices;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskActionHandlers
{
    public class GetProcessTaskActionByIdQueryHandler : IRequestHandler<GetProcessTaskActionByIdQuery, InternalHandlerResponse<GetProcessTaskActionByIdQueryResult>>
    {
        private readonly IProcessTaskActionQueryService _queryService;

        public GetProcessTaskActionByIdQueryHandler(IProcessTaskActionQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetProcessTaskActionByIdQueryResult>> Handle(GetProcessTaskActionByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetProcessTaskActionByIdQueryResult> serviceResponse =
               await _queryService.GetDataByIdAsync<GetProcessTaskActionByIdQueryResult>(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}