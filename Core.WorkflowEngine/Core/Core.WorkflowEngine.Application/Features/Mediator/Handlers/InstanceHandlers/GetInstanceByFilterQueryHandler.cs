using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class GetInstanceByFilterQueryHandler : IRequestHandler<GetInstanceByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetInstancesByFilterQueryResult>>>
    {
        private readonly IInstanceQueryService _queryService;
        private readonly ITokenBasedPaginationHelper<Instance, int, GetInstancesByFilterQueryResult> _paginationHelper;

        public GetInstanceByFilterQueryHandler(IInstanceQueryService queryService, ITokenBasedPaginationHelper<Instance, int, GetInstancesByFilterQueryResult> paginationHelper)
        {
            _queryService = queryService;
            _paginationHelper = paginationHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetInstancesByFilterQueryResult>>> Handle(GetInstanceByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Instance> options = new DBQueryOptions<Instance>();
            options.filter = x => (
                (!request.Number.HasValue || x.Number == request.Number) &&
                (!request.InitiatorWorkItemId.HasValue || x.InitiatorWorkItemId == request.InitiatorWorkItemId) &&
                (!request.Status.HasValue || x.Status == request.Status)
            );

            PaginationConfig<Instance, int, GetInstancesByFilterQueryResult> config = new PaginationConfig<Instance, int, GetInstancesByFilterQueryResult>
            {
                Token = request.Token,

                OrderBy = x => x.Number,
                CursorSelector = x => x.Number,
                CreatedAtCursor = x => x.CreatedAt,

                ForwardFilter = lastNumber => x => x.Number > lastNumber,
                BackwardFilter = firstNumber => x => x.Number < firstNumber,

                GetTotalCountAsync = async (options) =>
                {
                    InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCount(options);
                    return serviceResponse.Data;
                },

                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetInstancesByFilterQueryResult>> serviceResponse =
                    await _queryService.GetDatasByFilterAsync<GetInstancesByFilterQueryResult>(options);

                    return serviceResponse;
                }
            };

            return await _paginationHelper.PaginationAsync(config, options);

            //InternalServiceResponse<IReadOnlyCollection<GetInstancesByFilterQueryResult>> serviceResponse =
            //    await _queryService.GetDatasByFilterAsync<GetInstancesByFilterQueryResult>(options);

            //return serviceResponse.ToHandlerResponse();
        }
    }
}