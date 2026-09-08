using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class UpdateProcessDefinitionCommandHandler : IRequestHandler<UpdateProcessDefinitionCommand, InternalHandlerResponse<DateTimeOffset>>,
        IValidationRequest
    {
        private readonly IProcessDefinitionQueryService _processDefinitionQueryService;
        private readonly IProcessDefinitionCommandService _processDefinitionCommandService;
        private readonly IMapper _mapper;

        public UpdateProcessDefinitionCommandHandler(IProcessDefinitionQueryService processDefinitionQueryService, IProcessDefinitionCommandService processDefinitionCommandService, IMapper mapper)
        {
            _processDefinitionQueryService = processDefinitionQueryService;
            _processDefinitionCommandService = processDefinitionCommandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateProcessDefinitionCommand request, CancellationToken cancellationToken)
        {

            InternalServiceResponse<ProcessDefinition> serviceResponse = await _processDefinitionQueryService.GetDataByIdAsync(request.Id);

            ProcessDefinition existedData = serviceResponse.Data;

            _mapper.Map(request, existedData);

            InternalServiceResponse<DateTimeOffset> result = await _processDefinitionCommandService.UpdateAsync(existedData, cancellationToken);

            return InternalHandlerResponse<DateTimeOffset>.Success(result.Data, InternalHandlerConstants.SuccessProcessDefinitionUpdating);
        }
    }
}