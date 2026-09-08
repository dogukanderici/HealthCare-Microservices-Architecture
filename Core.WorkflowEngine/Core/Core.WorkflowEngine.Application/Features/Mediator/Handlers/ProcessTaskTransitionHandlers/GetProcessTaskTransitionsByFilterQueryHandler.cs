using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskTransitionDtos;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskTransitionHandlers
{
    public class GetProcessTaskTransitionsByFilterQueryHandler : IRequestHandler<GetProcessTaskTransitionsByFilterQuery,
        InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskTransitionsByFilterQueryResult>>>
    {
        private readonly ITaskTransitionQueryService _taskTransitionService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProcessTaskTransitionsByFilterQueryHandler> _logger;

        public GetProcessTaskTransitionsByFilterQueryHandler(ITaskTransitionQueryService taskTransitionService, IMapper mapper, ILogger<GetProcessTaskTransitionsByFilterQueryHandler> logger)
        {
            _taskTransitionService = taskTransitionService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskTransitionsByFilterQueryResult>>> Handle(GetProcessTaskTransitionsByFilterQuery request, CancellationToken cancellationToken)
        {
            TaskTransitionFilterDto serviceQueryDto = _mapper.Map<TaskTransitionFilterDto>(request);

            InternalServiceResponse<IReadOnlyCollection<ProcessTaskTransition>> result = await _taskTransitionService.GetDatasByFilterAsync(serviceQueryDto);

            return InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskTransitionsByFilterQueryResult>>
                .Success(_mapper.Map<IReadOnlyCollection<GetProcessTaskTransitionsByFilterQueryResult>>(result.Data));
        }
    }
}