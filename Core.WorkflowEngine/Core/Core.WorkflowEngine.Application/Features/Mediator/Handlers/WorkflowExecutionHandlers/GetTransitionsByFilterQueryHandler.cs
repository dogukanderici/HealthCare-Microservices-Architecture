using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkflowExecutionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkflowExecutionResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskTransitionDtos;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkflowExecutionHandlers
{
    public class GetTransitionsByFilterQueryHandler : IRequestHandler<GetTransitionsByFilterQuery, InternalHandlerResponse<List<GetTransitionsByFilterQueryResult>>>
    {
        private readonly ITaskTransitionService _taskTransitionService;
        private readonly IMapper _mapper;

        public GetTransitionsByFilterQueryHandler(ITaskTransitionService taskTransitionService, IMapper mapper)
        {
            _taskTransitionService = taskTransitionService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<List<GetTransitionsByFilterQueryResult>>> Handle(GetTransitionsByFilterQuery request, CancellationToken cancellationToken)
        {
            TaskTransitionFilterDto dataFromDto = _mapper.Map<TaskTransitionFilterDto>(request);

            InternalServiceResponse<List<ProcessTaskTransition>> result = await _taskTransitionService.GetDatasByFilterAsync(dataFromDto);

            return InternalHandlerResponse<List<GetTransitionsByFilterQueryResult>>.Success(
                _mapper.Map<List<GetTransitionsByFilterQueryResult>>(result)
                );
        }
    }
}