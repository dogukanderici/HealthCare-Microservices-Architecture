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
    public class GetProcessTasksQueryHandler : IRequestHandler<GetProcessTasksQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessTasksQueryResult>>>
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

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessTasksQueryResult>>> Handle(GetProcessTasksQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();

            Expression<Func<ProcessTask, bool>> filter = x => x.ProcessId == request.ProcessId;
            dBQueryOptions.filter = filter;

            List<Expression<Func<ProcessTask, object>>> includes = [
                x=>x.ProcessDefinition
                ];
            dBQueryOptions.includes = includes;

            IReadOnlyCollection<ProcessTask> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalHandlerResponse<IReadOnlyCollection<GetProcessTasksQueryResult>>.Success(_mapper.Map<IReadOnlyCollection<GetProcessTasksQueryResult>>(result));
        }
    }
}
