using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
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
    public class GetProcessTaskTransitionByIdQueryHandler : IRequestHandler<GetProcessTaskTransitionByIdQuery, GetProcessTaskTransitionByIdQueryResult>
    {
        private readonly ITaskTransitionService _taskTransitionService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProcessTaskTransitionByIdQueryHandler> _logger;

        public GetProcessTaskTransitionByIdQueryHandler(ITaskTransitionService taskTransitionService, IMapper mapper, ILogger<GetProcessTaskTransitionByIdQueryHandler> logger)
        {
            _taskTransitionService = taskTransitionService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetProcessTaskTransitionByIdQueryResult> Handle(GetProcessTaskTransitionByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<ProcessTaskTransition> result = await _taskTransitionService.GetDataByIdAsync(request.Id);

            return _mapper.Map<GetProcessTaskTransitionByIdQueryResult>(result.Data);
        }
    }
}