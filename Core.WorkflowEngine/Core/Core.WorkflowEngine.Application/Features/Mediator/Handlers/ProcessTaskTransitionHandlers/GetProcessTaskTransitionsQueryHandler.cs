using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskTransitionDtos;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskTransitionHandlers
{
    public class GetProcessTaskTransitionsQueryHandler : IRequestHandler<GetProcessTaskTransitionsQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskTransitionsQueryResult>>>
    {
        private readonly ITaskTransitionService _taskTransitionService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProcessTaskTransitionsQueryHandler> _logger;

        public GetProcessTaskTransitionsQueryHandler(ITaskTransitionService taskTransitionService, IMapper mapper, ILogger<GetProcessTaskTransitionsQueryHandler> logger)
        {
            _taskTransitionService = taskTransitionService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskTransitionsQueryResult>>> Handle(GetProcessTaskTransitionsQuery request, CancellationToken cancellationToken)
        {
            TaskTransitionFilterDto serviceQueryDto = _mapper.Map<TaskTransitionFilterDto>(request);

            InternalServiceResponse<IReadOnlyCollection<ProcessTaskTransition>> result = await _taskTransitionService.GetDatasByFilterAsync(serviceQueryDto);

            return InternalHandlerResponse<IReadOnlyCollection<GetProcessTaskTransitionsQueryResult>>.Success(_mapper.Map<IReadOnlyCollection<GetProcessTaskTransitionsQueryResult>>(result.Data));
        }
    }
}