using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class GetProcessDefinitionsQueryHandler : IRequestHandler<GetProcessDefinitionsQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsQueryResult>>>
    {
        private readonly IProcessDefinitionQueryService _processDefinitionService;
        private readonly ITokenBasedPaginationHelper<ProcessDefinition, string, GetProcessDefinitionsQueryResult> _paginationHelper;

        public GetProcessDefinitionsQueryHandler(IProcessDefinitionQueryService processDefinitionService, ITokenBasedPaginationHelper<ProcessDefinition, string, GetProcessDefinitionsQueryResult> paginationHelper)
        {
            _processDefinitionService = processDefinitionService;
            _paginationHelper = paginationHelper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsQueryResult>>> Handle(GetProcessDefinitionsQuery request, CancellationToken cancellationToken)
        {
            PaginationConfig<ProcessDefinition, string, GetProcessDefinitionsQueryResult> config = new PaginationConfig<ProcessDefinition, string, GetProcessDefinitionsQueryResult>
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
                    InternalServiceResponse<IReadOnlyCollection<GetProcessDefinitionsQueryResult>> serviceResponse =
                        await _processDefinitionService.GetDatasAsync<GetProcessDefinitionsQueryResult>(options);

                    return serviceResponse;
                }
            };

            return await _paginationHelper.PaginationAsync(config);

            //InternalServiceResponse<IReadOnlyCollection<GetProcessDefinitionsQueryResult>> serviceResponse =
            //    await _processDefinitionService.GetDatasAsync<GetProcessDefinitionsQueryResult>();

            //return serviceResponse.ToHandlerResponse();
        }
    }
}