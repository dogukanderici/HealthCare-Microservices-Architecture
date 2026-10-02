using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskHandlers
{
    public class GetProcessTasksByFilterQueryHandler : IRequestHandler<GetProcessTasksByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessTasksByFilterQueryResult>>>
    {
        private readonly IProcessTaskQueryService _queryService;

        public GetProcessTasksByFilterQueryHandler(IProcessTaskQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessTasksByFilterQueryResult>>> Handle(GetProcessTasksByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();
            dBQueryOptions.filter = x => (
                (!request.ProcessId.HasValue || x.ProcessId == request.ProcessId) &&
                (string.IsNullOrEmpty(request.StepName) || x.StepName == request.StepName) &&
                (!request.IsActive.HasValue || x.IsActive == request.IsActive) &&
                (!request.IsStartStep.HasValue || x.IsStartStep == request.IsStartStep)
            );

            InternalServiceResponse<IReadOnlyCollection<GetProcessTasksByFilterQueryResult>> serviceResponse =
                await _queryService.GetDatasByFilterAsync<GetProcessTasksByFilterQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}