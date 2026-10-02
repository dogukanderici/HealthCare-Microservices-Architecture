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
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class GetInstanceByFilterQueryHandler : IRequestHandler<GetInstanceByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetInstancesByFilterQueryResult>>>
    {
        private readonly IInstanceQueryService _queryService;

        public GetInstanceByFilterQueryHandler(IInstanceQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetInstancesByFilterQueryResult>>> Handle(GetInstanceByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Instance> options = new DBQueryOptions<Instance>();
            options.filter = x => (
                (!request.Number.HasValue || x.Number == request.Number) &&
                (!request.InitiatorWorkItemId.HasValue || x.InitiatorWorkItemId == request.InitiatorWorkItemId) &&
                (!request.Status.HasValue || x.Status == request.Status)
            );

            InternalServiceResponse<IReadOnlyCollection<GetInstancesByFilterQueryResult>> serviceResponse =
                await _queryService.GetDatasByFilterAsync<GetInstancesByFilterQueryResult>(options);

            return serviceResponse.ToHandlerResponse();
        }
    }
}