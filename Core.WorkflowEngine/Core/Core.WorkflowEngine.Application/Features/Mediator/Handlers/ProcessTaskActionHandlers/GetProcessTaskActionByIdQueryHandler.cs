using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskActionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskActionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskActionHandlers
{
    public class GetProcessTaskActionByIdQueryHandler : IRequestHandler<GetProcessTaskActionByIdQuery, InternalHandlerResponse<GetProcessTaskActionByIdQueryResult>>
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

        public async Task<InternalHandlerResponse<GetProcessTaskActionByIdQueryResult>> Handle(GetProcessTaskActionByIdQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTaskAction> dBQueryOptions = new DBQueryOptions<ProcessTaskAction>();

            Expression<Func<ProcessTaskAction, bool>> filter = x => x.Id == request.Id;
            dBQueryOptions.filter = filter;

            ProcessTaskAction result = await _repository.GetDataAsync(dBQueryOptions);

            return InternalHandlerResponse<GetProcessTaskActionByIdQueryResult>.Success(_mapper.Map<GetProcessTaskActionByIdQueryResult>(result));
        }
    }
}
