using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
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
    public class GetWorkItemsByFilterQueryHandler : IRequestHandler<GetWorkItemsByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetWorkItemsByFilterQueryResult>>>
    {
        private readonly IRepository<WorkItem> _repository;
        private readonly ILogger<GetWorkItemsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetWorkItemsByFilterQueryHandler(IRepository<WorkItem> repository, ILogger<GetWorkItemsQueryHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetWorkItemsByFilterQueryResult>>> Handle(GetWorkItemsByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();

            Expression<Func<WorkItem, bool>> filter = x => (
                (!request.InstanceId.HasValue || x.InstanceId == request.InstanceId) &&
                (!request.AssignedUserId.HasValue || x.AssignedUserId == request.AssignedUserId) &&
                (!request.Status.HasValue || x.Status == request.Status) &&
                (!request.CreatedAt.HasValue || x.CreatedAt == request.CreatedAt) &&
                (request.CreatedBy.HasValue || x.CreatedBy == request.CreatedBy));

            dBQueryOptions.filter = filter;

            IReadOnlyCollection<WorkItem> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalHandlerResponse<IReadOnlyCollection<GetWorkItemsByFilterQueryResult>>
                .Success(_mapper.Map<IReadOnlyCollection<GetWorkItemsByFilterQueryResult>>(result));
        }
    }
}