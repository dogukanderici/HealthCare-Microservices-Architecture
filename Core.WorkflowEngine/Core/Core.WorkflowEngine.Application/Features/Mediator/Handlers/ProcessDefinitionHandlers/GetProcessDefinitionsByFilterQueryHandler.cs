using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class GetProcessDefinitionsByFilterQueryHandler : IRequestHandler<GetProcessDefinitionsByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>>>
    {
        private readonly IProcessDefinitionQueryService _processDefinitionService;
        private readonly ITokenBasedPaginationHelper<ProcessDefinition, string, GetProcessDefinitionsByFilterQueryResult> _paginationHelper;

        public GetProcessDefinitionsByFilterQueryHandler(IProcessDefinitionQueryService processDefinitionService, ITokenBasedPaginationHelper<ProcessDefinition, string, GetProcessDefinitionsByFilterQueryResult> paginationHelper)
        {
            _processDefinitionService = processDefinitionService;
            _paginationHelper = paginationHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>>> Handle(GetProcessDefinitionsByFilterQuery request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            dBQueryOptions.filter = x => (
                (!request.ProcessSpecId.HasValue || x.ProcessSpecId == request.ProcessSpecId) &&
                (!request.IsActive.HasValue || x.IsActive == request.IsActive) &&
                (string.IsNullOrEmpty(request.ProcessName) || x.ProcessName.Contains(request.ProcessName))
            );

            PaginationConfig<ProcessDefinition, string, GetProcessDefinitionsByFilterQueryResult> config = new PaginationConfig<ProcessDefinition, string, GetProcessDefinitionsByFilterQueryResult>
            {
                Token = request.Token,

                OrderBy = x => x.ProcessName,
                CursorSelector = x => x.ProcessName,
                CreatedAtCursor = x => x.CreatedAt,

                ForwardFilter = lastName => x => string.Compare(x.ProcessName, lastName) > 0,
                BackwardFilter = firstName => x => string.Compare(x.ProcessName, firstName) < 0,

                GetTotalCountAsync = async (options) =>
                {
                    InternalServiceResponse<int> serviceResponse = await _processDefinitionService.GetDataCount(options);

                    if (!serviceResponse.IsSuccess)
                        return 0;

                    return serviceResponse.Data;
                },

                FetchDataAsync = async (options) =>
                {
                    InternalServiceResponse<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>> serviceResponse =
                        await _processDefinitionService.GetDatasByFilterAsync<GetProcessDefinitionsByFilterQueryResult>(options);

                    return serviceResponse;
                }
            };

            return await _paginationHelper.PaginationAsync(config, dBQueryOptions);

            //InternalServiceResponse<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>> serviceResponse =
            //    await _processDefinitionService.GetDatasByFilterAsync<GetProcessDefinitionsByFilterQueryResult>(dBQueryOptions);

            //return serviceResponse.ToHandlerResponse();
        }
    }
}