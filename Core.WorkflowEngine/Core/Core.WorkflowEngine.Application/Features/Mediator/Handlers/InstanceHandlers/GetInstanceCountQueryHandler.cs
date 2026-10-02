using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class GetInstanceCountQueryHandler : IRequestHandler<GetInstanceCountQuery, InternalHandlerResponse<int>>
    {
        private readonly IInstanceQueryService _queryService;

        public GetInstanceCountQueryHandler(IInstanceQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<int>> Handle(GetInstanceCountQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Instance> options = new DBQueryOptions<Instance>();

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCount(options);

            return serviceResponse.ToHandlerResponse();
        }
    }
}