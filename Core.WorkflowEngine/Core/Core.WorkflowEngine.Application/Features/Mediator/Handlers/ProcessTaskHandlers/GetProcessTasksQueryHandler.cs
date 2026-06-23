using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskHandlers
{
    public class GetProcessTasksQueryHandler : IRequestHandler<GetProcessTasksQuery, List<GetProcessTasksQueryResult>>
    {
        private readonly IRepository<ProcessTask> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProcessTasksQueryHandler> _logger;

        public GetProcessTasksQueryHandler(IRepository<ProcessTask> repository, IMapper mapper, ILogger<GetProcessTasksQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<GetProcessTasksQueryResult>> Handle(GetProcessTasksQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();

            Expression<Func<ProcessTask, bool>> filter = x => x.ProcessId == request.ProcessId;
            dBQueryOptions.filter = filter;

            List<ProcessTask> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return _mapper.Map<List<GetProcessTasksQueryResult>>(result);
        }
    }
}
