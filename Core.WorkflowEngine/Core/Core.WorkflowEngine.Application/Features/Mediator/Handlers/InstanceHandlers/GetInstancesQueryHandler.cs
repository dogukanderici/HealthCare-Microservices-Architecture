using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class GetInstancesQueryHandler : IRequestHandler<GetInstancesQuery, InternalHandlerResponse<IReadOnlyCollection<GetInstancesQueryResult>>>
    {
        private readonly IInstanceQueryService _queryService;

        public GetInstancesQueryHandler(IInstanceQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetInstancesQueryResult>>> Handle(GetInstancesQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Instance> dBQueryOptions = new DBQueryOptions<Instance>();

            InternalServiceResponse<IReadOnlyCollection<GetInstancesQueryResult>> serviceResponse =
                await _queryService.GetDatasAsync<GetInstancesQueryResult>(dBQueryOptions);

            return serviceResponse.ToHandlerResponse();
        }
    }
}