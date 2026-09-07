using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class GetWorkItemsQueryHandler : IRequestHandler<GetWorkItemsQuery, InternalHandlerResponse<IReadOnlyCollection<GetWorkItemsQueryResult>>>
    {
        private readonly IRepository<WorkItem> _repository;
        private readonly ILogger<GetWorkItemsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetWorkItemsQueryHandler(IRepository<WorkItem> repository, ILogger<GetWorkItemsQueryHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetWorkItemsQueryResult>>> Handle(GetWorkItemsQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();

            Expression<Func<WorkItem, bool>> filter = x => x.InstanceId == request.InstanceId;
            dBQueryOptions.filter = filter;

            IReadOnlyCollection<WorkItem> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalHandlerResponse<IReadOnlyCollection<GetWorkItemsQueryResult>>.Success(_mapper.Map<IReadOnlyCollection<GetWorkItemsQueryResult>>(result));
        }
    }
}