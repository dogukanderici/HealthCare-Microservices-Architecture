using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
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
        private readonly IProcessDefinitionCommandService _processDefinitionCommandService;
        private readonly IMapper _mapper;

        public UpdateProcessDefinitionCommandHandler(IProcessDefinitionCommandService processDefinitionCommandService, IMapper mapper)
        {
            _processDefinitionCommandService = processDefinitionCommandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateProcessDefinitionCommand request, CancellationToken cancellationToken)
        {

            InternalServiceResponse<ProcessDefinition> existedData = await _processDefinitionCommandService.GetDataForUpdateAsync(request.Id);

            if (!existedData.IsSuccess)
                return InternalHandlerResponse<DateTimeOffset>.Failure("Data not found!");

            _mapper.Map(request, existedData.Data);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _processDefinitionCommandService.UpdateAsync(existedData.Data);

            return serviceResponse.ToHandlerResponse();
        }
    }
}