using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkflowExecutionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkflowExecutionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkflowExecutionHandlers
{
    public class GetTransitionsByFilterQueryHandler : IRequestHandler<GetTransitionsByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetTransitionsByFilterQueryResult>>>
    {
        private readonly ITaskTransitionQueryService _taskTransitionQueryService;
        private readonly IMapper _mapper;

        public GetTransitionsByFilterQueryHandler(ITaskTransitionQueryService taskTransitionQueryService, IMapper mapper)
        {
            _taskTransitionQueryService = taskTransitionQueryService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetTransitionsByFilterQueryResult>>> Handle(GetTransitionsByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = new DBQueryOptions<ProcessTaskTransition>();
            dBQueryOptions.filter = x => (
                (!request.ProcessTaskId.HasValue || x.ProcessTaskId == request.ProcessTaskId) &&
                (!request.ActionId.HasValue || x.ActionId == request.ActionId) &&
                (!request.IsActive.HasValue || x.IsActive == request.IsActive)
            );

            InternalServiceResponse<IReadOnlyCollection<GetTransitionsByFilterQueryResult>> serviceResponse =
                await _taskTransitionQueryService.GetDatasByFilterAsync<GetTransitionsByFilterQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}