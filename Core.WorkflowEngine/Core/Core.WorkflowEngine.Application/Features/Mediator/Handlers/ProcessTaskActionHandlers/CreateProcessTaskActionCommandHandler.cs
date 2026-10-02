using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskActionServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskActionHandlers
{
    public class CreateProcessTaskActionCommandHandler : IRequestHandler<CreateProcessTaskActionCommand, InternalHandlerResponse<Guid>>
    {
        private IProcessTaskActionCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateProcessTaskActionCommandHandler(IProcessTaskActionCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateProcessTaskActionCommand request, CancellationToken cancellationToken)
        {
            ProcessTaskAction dataFromDto = _mapper.Map<ProcessTaskAction>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFromDto, cancellationToken);

            return serviceResponse.ToHandlerResponse();
        }
    }
}