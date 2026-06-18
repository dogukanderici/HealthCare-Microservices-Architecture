using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkItemQueries;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class GetWorkItemCountQueryHandler : IRequestHandler<GetWorkItemCountQuery, int>
    {
        private readonly IRepository<WorkItem> _repository;
        private readonly ILogger<GetWorkItemsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetWorkItemCountQueryHandler(IRepository<WorkItem> repository, ILogger<GetWorkItemsQueryHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetWorkItemCountQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllDataCountAsync();
        }
    }
}