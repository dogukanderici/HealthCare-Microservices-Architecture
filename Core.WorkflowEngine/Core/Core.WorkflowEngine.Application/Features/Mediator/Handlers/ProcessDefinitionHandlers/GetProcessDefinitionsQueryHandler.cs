using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class GetProcessDefinitionsQueryHandler : IRequestHandler<GetProcessDefinitionsQuery, InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsQueryResult>>>
    {
        private readonly IProcessDefinitionService _processDefinitionService;
        private readonly IMapper _mapper;

        public GetProcessDefinitionsQueryHandler(IProcessDefinitionService processDefinitionService, IMapper mapper)
        {
            _processDefinitionService = processDefinitionService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsQueryResult>>> Handle(GetProcessDefinitionsQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<IReadOnlyCollection<ProcessDefinition>> serviceResponse =
                await _processDefinitionService.GetDatasAsync();

            return InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsQueryResult>>
                .Success(_mapper.Map<IReadOnlyCollection<GetProcessDefinitionsQueryResult>>(serviceResponse.Data));
        }
    }
}