using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskTransitionDtos;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskTransitionHandlers
{
    public class GetProcessTaskTransitionsQueryHandler : IRequestHandler<GetProcessTaskTransitionsQuery, InternalHandlerResponse<List<GetProcessTaskTransitionsQueryResult>>>
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

        public async Task<InternalHandlerResponse<List<GetProcessTaskTransitionsQueryResult>>> Handle(GetProcessTaskTransitionsQuery request, CancellationToken cancellationToken)
        {
            TaskTransitionFilterDto serviceQueryDto = _mapper.Map<TaskTransitionFilterDto>(request);

            InternalServiceResponse<List<ProcessTaskTransition>> result = await _taskTransitionService.GetDatasByFilterAsync(serviceQueryDto);

            return InternalHandlerResponse<List<GetProcessTaskTransitionsQueryResult>>.Success(_mapper.Map<List<GetProcessTaskTransitionsQueryResult>>(result.Data));
        }
    }
}