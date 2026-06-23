using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskActionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskActionResults;
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

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskActionHandlers
{
    public class GetProcessTaskActionByIdQueryHandler : IRequestHandler<GetProcessTaskActionByIdQuery, GetProcessTaskActionByIdQueryResult>
    {
        private readonly IRepository<ProcessTaskAction> _repository;
        private readonly ILogger<GetProcessTaskActionByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetProcessTaskActionByIdQueryHandler(IRepository<ProcessTaskAction> repository, ILogger<GetProcessTaskActionByIdQueryHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<GetProcessTaskActionByIdQueryResult> Handle(GetProcessTaskActionByIdQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTaskAction> dBQueryOptions = new DBQueryOptions<ProcessTaskAction>();

            Expression<Func<ProcessTaskAction, bool>> filter = x => x.Id == request.Id;
            dBQueryOptions.filter = filter;

            ProcessTaskAction result = await _repository.GetDataAsync(dBQueryOptions);

            return _mapper.Map<GetProcessTaskActionByIdQueryResult>(result);
        }
    }
}
