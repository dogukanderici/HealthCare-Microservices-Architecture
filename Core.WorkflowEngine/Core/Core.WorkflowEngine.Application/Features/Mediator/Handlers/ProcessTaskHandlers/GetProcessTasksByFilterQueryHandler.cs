using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

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
            (!request.IsActive.HasValue || x.IsActive == request.IsActive) &&
            (!request.IsStartStep.HasValue || x.IsStartStep == request.IsStartStep)
            );

            dBQueryOptions.filter = filter;

            IReadOnlyCollection<ProcessTask> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalHandlerResponse<IReadOnlyCollection<GetProcessTasksByFilterQueryResult>>.Success(_mapper.Map<IReadOnlyCollection<GetProcessTasksByFilterQueryResult>>(result));
        }
    }
}