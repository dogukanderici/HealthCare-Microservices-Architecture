using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
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

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskHandlers
{
    public class GetProcessTasksByFilterQueryHandler : IRequestHandler<GetProcessTasksByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessTasksByFilterQueryResult>>>
    {
        private readonly IRepository<ProcessTask> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProcessTasksByFilterQueryHandler> _logger;

        public GetProcessTasksByFilterQueryHandler(IRepository<ProcessTask> repository, IMapper mapper, ILogger<GetProcessTasksByFilterQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessTasksByFilterQueryResult>>> Handle(GetProcessTasksByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();

            Expression<Func<ProcessTask, bool>> filter = x => (
            (!request.ProcessId.HasValue || x.ProcessId == request.ProcessId) &&
            (string.IsNullOrEmpty(request.StepName) || x.StepName == request.StepName) &&
            (!request.IsActive.HasValue || x.IsActive == request.IsActive)
            );

            dBQueryOptions.filter = filter;

            IReadOnlyCollection<ProcessTask> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalHandlerResponse<IReadOnlyCollection<GetProcessTasksByFilterQueryResult>>.Success(_mapper.Map<IReadOnlyCollection<GetProcessTasksByFilterQueryResult>>(result));
        }
    }
}