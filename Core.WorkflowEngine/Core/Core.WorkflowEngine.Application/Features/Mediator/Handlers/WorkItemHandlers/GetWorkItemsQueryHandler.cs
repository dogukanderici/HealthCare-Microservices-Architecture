using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
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
    public class GetWorkItemsQueryHandler : IRequestHandler<GetWorkItemsQuery, List<GetWorkItemsQueryResult>>
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

        public async Task<List<GetWorkItemsQueryResult>> Handle(GetWorkItemsQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();

            Expression<Func<WorkItem, bool>> filter = x => x.InstanceId == request.InstanceId;
            dBQueryOptions.filter = filter;

            List<WorkItem> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return _mapper.Map<List<GetWorkItemsQueryResult>>(result);
        }
    }
}