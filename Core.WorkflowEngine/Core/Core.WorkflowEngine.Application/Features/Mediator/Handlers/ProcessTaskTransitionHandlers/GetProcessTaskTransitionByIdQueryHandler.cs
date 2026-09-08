using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskTransitionHandlers
{
    public class GetProcessTaskTransitionByIdQueryHandler : IRequestHandler<GetProcessTaskTransitionByIdQuery, InternalHandlerResponse<GetProcessTaskTransitionByIdQueryResult>>
    {
        private readonly ITaskTransitionQueryService _taskTransitionService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProcessTaskTransitionByIdQueryHandler> _logger;

        public GetProcessTaskTransitionByIdQueryHandler(ITaskTransitionQueryService taskTransitionService, IMapper mapper, ILogger<GetProcessTaskTransitionByIdQueryHandler> logger)
        {
            _taskTransitionService = taskTransitionService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InternalHandlerResponse<GetProcessTaskTransitionByIdQueryResult>> Handle(GetProcessTaskTransitionByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<ProcessTaskTransition> result = await _taskTransitionService.GetDataByIdAsync(request.Id);

            return InternalHandlerResponse<GetProcessTaskTransitionByIdQueryResult>
                .Success(_mapper.Map<GetProcessTaskTransitionByIdQueryResult>(result.Data));
        }
    }
}