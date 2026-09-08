using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class GetProcessDefinitionByIdQueryHandler : IRequestHandler<GetProcessDefinitionByIdQuery, InternalHandlerResponse<GetProcessDefinitionByIdQueryResult>>
    {
        private readonly IProcessDefinitionQueryService _processDefinitionService;
        private readonly IMapper _mapper;

        public GetProcessDefinitionByIdQueryHandler(IProcessDefinitionQueryService processDefinitionService, IMapper mapper)
        {
            _processDefinitionService = processDefinitionService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<GetProcessDefinitionByIdQueryResult>> Handle(GetProcessDefinitionByIdQuery request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<ProcessDefinition> serviceResponse = await _processDefinitionService.GetDataByIdAsync(request.Id);

            return InternalHandlerResponse<GetProcessDefinitionByIdQueryResult>.Success(_mapper.Map<GetProcessDefinitionByIdQueryResult>(serviceResponse.Data));
        }
    }
}