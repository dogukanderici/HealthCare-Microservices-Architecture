using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskHandlers
{
    public class GetProcessTasksQueryHandler : IRequestHandler<GetProcessTasksQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessTasksQueryResult>>>
    {
        private readonly IProcessTaskQueryService _queryService;

        public GetProcessTasksQueryHandler(IProcessTaskQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessTasksQueryResult>>> Handle(GetProcessTasksQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();
            dBQueryOptions.filter = x => x.ProcessId == request.ProcessId;
            dBQueryOptions.includes = [
                    x=>x.ProcessDefinition
                ];

            InternalServiceResponse<IReadOnlyCollection<GetProcessTasksQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetProcessTasksQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}