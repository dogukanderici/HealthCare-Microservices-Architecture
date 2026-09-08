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
    public class CreateProcessDefinitionCommandHandler : IRequestHandler<CreateProcessDefinitionCommand, InternalHandlerResponse<Guid>>,
        IValidationRequest
    {
        private readonly IProcessDefinitionCommandService _processDefinitionCommandService;
        private readonly IMapper _mapper;

        public CreateProcessDefinitionCommandHandler(IProcessDefinitionCommandService processDefinitionCommandService, IMapper mapper)
        {
            _processDefinitionCommandService = processDefinitionCommandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateProcessDefinitionCommand request, CancellationToken cancellationToken)
        {
            ProcessDefinition dataFromDto = _mapper.Map<ProcessDefinition>(request);

            dataFromDto.Id = Guid.NewGuid();
            dataFromDto.ProcessSpecId = Guid.NewGuid();

            InternalServiceResponse<Guid> result = await _processDefinitionCommandService.CreateAsync(dataFromDto, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalHandlerResponse<Guid>.Success(result.Data, InternalHandlerConstants.SuccessProcessDefinitionCreating);
            }

            return InternalHandlerResponse<Guid>.Failure(InternalHandlerConstants.ErrorProcessDefinitionCreating);
        }
    }
}