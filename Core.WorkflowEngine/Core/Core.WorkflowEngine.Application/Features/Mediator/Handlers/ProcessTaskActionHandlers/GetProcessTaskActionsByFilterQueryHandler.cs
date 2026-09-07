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
    public class GetProcessTaskActionsByFilterQueryHandler : IRequestHandler<GetProcessTaskActionsByFilterQuery, InternalHandlerResponse<List<GetProcessTaskActionsByFilterQueryResult>>>
    {
        private readonly IRepository<ProcessTaskAction> _repository;
        private readonly ILogger<GetProcessTaskActionsByFilterQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetProcessTaskActionsByFilterQueryHandler(IRepository<ProcessTaskAction> repository, ILogger<GetProcessTaskActionsByFilterQueryHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<List<GetProcessTaskActionsByFilterQueryResult>>> Handle(GetProcessTaskActionsByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTaskAction> dBQueryOptions = new DBQueryOptions<ProcessTaskAction>();

            Expression<Func<ProcessTaskAction, bool>> filter = x => (
            (!request.ProcessTaskId.HasValue || x.ProcessTaskId == request.ProcessTaskId) &&
            (request.ActionId.HasValue || x.ActionId == request.ActionId) &&
            (!string.IsNullOrEmpty(request.ActionName) || x.ActionName == request.ActionName) &&
            (!request.ActionType.HasValue || x.ActionType == request.ActionType) &&
            (request.IsActive.HasValue || x.IsActive == request.IsActive)
            );
            dBQueryOptions.filter = filter;

            List<ProcessTaskAction> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalHandlerResponse<List<GetProcessTaskActionsByFilterQueryResult>>.Success(_mapper.Map<List<GetProcessTaskActionsByFilterQueryResult>>(result));
        }
    }
}