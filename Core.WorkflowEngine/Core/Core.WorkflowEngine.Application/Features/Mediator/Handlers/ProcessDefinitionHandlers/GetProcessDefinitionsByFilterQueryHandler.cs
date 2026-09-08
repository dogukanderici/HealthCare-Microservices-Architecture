using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessDefinitionDtos;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class GetProcessDefinitionsByFilterQueryHandler : IRequestHandler<GetProcessDefinitionsByFilterQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>>>
    {
        private readonly IProcessDefinitionQueryService _processDefinitionService;
        private readonly IMapper _mapper;

        public GetProcessDefinitionsByFilterQueryHandler(IProcessDefinitionQueryService processDefinitionService, IMapper mapper)
        {
            _processDefinitionService = processDefinitionService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>>> Handle(GetProcessDefinitionsByFilterQuery request, CancellationToken cancellationToken)
        {
            ProcessDefinitionFilterDto mappedRequest = _mapper.Map<ProcessDefinitionFilterDto>(request);

            InternalServiceResponse<IReadOnlyCollection<ProcessDefinition>> serviceResponse =
                await _processDefinitionService.GetDatasByFilterAsync(mappedRequest);

            return InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>>
                .Success(_mapper.Map<IReadOnlyCollection<GetProcessDefinitionsByFilterQueryResult>>(serviceResponse.Data));
        }
    }
}