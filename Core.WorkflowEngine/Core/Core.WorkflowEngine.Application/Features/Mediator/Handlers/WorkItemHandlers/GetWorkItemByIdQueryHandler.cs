using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Application.ServiceDtos.WorkItemServiceDtos;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class GetWorkItemByIdQueryHandler : IRequestHandler<GetWorkItemByIdQuery, InternalHandlerResponse<GetWorkItemByIdQueryResult>>
    {
        private readonly IRepository<WorkItem> _repository;
        private readonly ILogger<GetWorkItemsQueryHandler> _logger;
        private readonly IMapper _mapper;

        private readonly IWorkItemService _workItemService;

        public GetWorkItemByIdQueryHandler(IRepository<WorkItem> repository, ILogger<GetWorkItemsQueryHandler> logger, IMapper mapper, IWorkItemService workItemService)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _workItemService = workItemService;
        }

        public async Task<InternalHandlerResponse<GetWorkItemByIdQueryResult>> Handle(GetWorkItemByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<WorkItem> result = await _workItemService.GetWorkItemByIdAsync(request.WorkItemId);

            return InternalHandlerResponse<GetWorkItemByIdQueryResult>
                .Success(_mapper.Map<GetWorkItemByIdQueryResult>(result.Data));
        }
    }
}
