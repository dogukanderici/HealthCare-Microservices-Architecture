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
    public class GetProcessTaskByIdQueryHandler : IRequestHandler<GetProcessTaskByIdQuery, InternalHandlerResponse<GetProcessTaskByIdQueryResult>>
    {
        private readonly IRepository<ProcessTask> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProcessTaskByIdQueryHandler> _logger;

        public GetProcessTaskByIdQueryHandler(IRepository<ProcessTask> repository, IMapper mapper, ILogger<GetProcessTaskByIdQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InternalHandlerResponse<GetProcessTaskByIdQueryResult>> Handle(GetProcessTaskByIdQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();

            Expression<Func<ProcessTask, bool>> filter = x => x.Id == request.Id;
            dBQueryOptions.filter = filter;

            ProcessTask result = await _repository.GetDataAsync(dBQueryOptions);

            return InternalHandlerResponse<GetProcessTaskByIdQueryResult>.Success(_mapper.Map<GetProcessTaskByIdQueryResult>(result));
        }
    }
}
