using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessDefinitionDtos;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class GetProcessDefinitionCountQueryHandler : IRequestHandler<GetProcessDefinitionCountQuery, InternalHandlerResponse<GetProcessDefinitionCountQueryResult>>
    {
        private readonly IProcessDefinitionQueryService _processDefinitionService;
        private readonly IMapper _mapper;

        public GetProcessDefinitionCountQueryHandler(IProcessDefinitionQueryService processDefinitionService, IMapper mapper)
        {
            _processDefinitionService = processDefinitionService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<GetProcessDefinitionCountQueryResult>> Handle(GetProcessDefinitionCountQuery request, CancellationToken cancellationToken)
        {
            ProcessDefinitionFilterDto mappedRequest = _mapper.Map<ProcessDefinitionFilterDto>(request);

            InternalServiceResponse<int> serviceResult = await _processDefinitionService.GetDataCount(mappedRequest);

            return InternalHandlerResponse<GetProcessDefinitionCountQueryResult>.Success(_mapper.Map<GetProcessDefinitionCountQueryResult>(serviceResult.Data));
        }
    }
}