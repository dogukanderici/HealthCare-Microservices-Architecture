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
    public class GetProcessTaskTransitionsQueryHandler : IRequestHandler<GetProcessTaskTransitionsQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskTransitionsQueryResult>>>
    {
        private readonly ITaskTransitionQueryService _queryService;

        public GetProcessTaskTransitionsQueryHandler(ITaskTransitionQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskTransitionsQueryResult>>> Handle(GetProcessTaskTransitionsQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = new DBQueryOptions<ProcessTaskTransition>();
            dBQueryOptions.filter = x => x.ProcessTaskId == request.ProcessTaskId;

            InternalServiceResponse<IReadOnlyCollection<GetProcessTaskTransitionsQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetProcessTaskTransitionsQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}