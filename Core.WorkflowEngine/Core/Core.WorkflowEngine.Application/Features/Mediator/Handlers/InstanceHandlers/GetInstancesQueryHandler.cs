using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class GetInstancesQueryHandler : IRequestHandler<GetInstancesQuery, InternalHandlerResponse<IReadOnlyCollection<GetInstancesQueryResult>>>
    {
        private readonly IInstanceQueryService _queryService;
        private readonly ITokenBasedPaginationHelper<Instance, int, GetInstancesQueryResult> _paginationHelper;

        public GetInstancesQueryHandler(IInstanceQueryService queryService, ITokenBasedPaginationHelper<Instance, int, GetInstancesQueryResult> paginationHelper)
        {
            _queryService = queryService;
            _paginationHelper = paginationHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetInstancesQueryResult>>> Handle(GetInstancesQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<Instance> dBQueryOptions = new DBQueryOptions<Instance>();

            PaginationConfig<Instance, int, GetInstancesQueryResult> config = new PaginationConfig<Instance, int, GetInstancesQueryResult>
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

                    if (!serviceResponse.IsSuccess)
                        return 0;

                    return serviceResponse.Data;
                },

                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetInstancesQueryResult>> serviceResponse =
                    await _queryService.GetDatasAsync<GetInstancesQueryResult>(options);

                    return serviceResponse;
                }
            };

            return await _paginationHelper.PaginationAsync(config, dBQueryOptions);

            //InternalServiceResponse<IReadOnlyCollection<GetInstancesQueryResult>> serviceResponse =
            //    await _queryService.GetDatasAsync<GetInstancesQueryResult>(dBQueryOptions);

            //return serviceResponse.ToHandlerResponse();
        }
    }
}