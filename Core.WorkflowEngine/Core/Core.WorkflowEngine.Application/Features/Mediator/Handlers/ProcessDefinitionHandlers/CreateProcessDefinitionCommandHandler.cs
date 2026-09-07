using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class CreateProcessDefinitionCommandHandler : IRequestHandler<CreateProcessDefinitionCommand, InternalHandlerResponse<Guid>>,
        IValidationRequest
    {
        private readonly IProcessDefinitionService _processDefinitionService;
        private readonly IMapper _mapper;

        public CreateProcessDefinitionCommandHandler(IProcessDefinitionService processDefinitionService, IMapper mapper)
        {
            _processDefinitionService = processDefinitionService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateProcessDefinitionCommand request, CancellationToken cancellationToken)
        {
            ProcessDefinition dataFromDto = _mapper.Map<ProcessDefinition>(request);

            dataFromDto.Id = Guid.NewGuid();
            dataFromDto.ProcessSpecId = Guid.NewGuid();

            InternalServiceResponse<Guid> result = await _processDefinitionService.CreateAsync(dataFromDto, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalHandlerResponse<Guid>.Success(result.Data, InternalHandlerConstants.SuccessProcessDefinitionCreating);
            }

            return InternalHandlerResponse<Guid>.Failure(InternalHandlerConstants.ErrorProcessDefinitionCreating);
        }
    }
}