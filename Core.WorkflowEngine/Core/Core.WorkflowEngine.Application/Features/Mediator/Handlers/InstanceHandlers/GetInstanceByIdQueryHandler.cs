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
    public class GetInstanceByIdQueryHandler : IRequestHandler<GetInstanceByIdQuery, InternalHandlerResponse<GetInstanceByIdQueryResult>>
    {
        private readonly IInstanceQueryService _queryService;

        public GetInstanceByIdQueryHandler(IInstanceQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<InternalHandlerResponse<GetInstanceByIdQueryResult>> Handle(GetInstanceByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<GetInstanceByIdQueryResult> serviceResponse =
                await _queryService.GetDataByIdAsync<GetInstanceByIdQueryResult>(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}