using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class GetWorkItemByIdQueryHandler : IRequestHandler<GetWorkItemByIdQuery, InternalHandlerResponse<GetWorkItemByIdQueryResult>>
    {
        private readonly IRepository<WorkItem> _repository;
        private readonly IWorkItemQueryService _workItemQueryService;
        private readonly ILogger<GetWorkItemsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetWorkItemByIdQueryHandler(IRepository<WorkItem> repository, IWorkItemQueryService workItemQueryService, ILogger<GetWorkItemsQueryHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _workItemQueryService = workItemQueryService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<GetWorkItemByIdQueryResult>> Handle(GetWorkItemByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<WorkItem> result = await _workItemQueryService.GetDataByIdAsync(request.WorkItemId);

            return InternalHandlerResponse<GetWorkItemByIdQueryResult>
                .Success(_mapper.Map<GetWorkItemByIdQueryResult>(result.Data));
        }
    }
}