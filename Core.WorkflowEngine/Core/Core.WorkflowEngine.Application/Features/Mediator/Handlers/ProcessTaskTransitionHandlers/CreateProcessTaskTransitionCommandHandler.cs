using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskTransitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskTransitionHandlers
{
    public class CreateProcessTaskTransitionCommandHandler : IRequestHandler<CreateProcessTaskTransitionCommand, InternalHandlerResponse<Guid>>
    {
        private readonly ITaskTransitionCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateProcessTaskTransitionCommandHandler(ITaskTransitionCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateProcessTaskTransitionCommand request, CancellationToken cancellationToken)
        {
            ProcessTaskTransition dataFromDto = _mapper.Map<ProcessTaskTransition>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFromDto, cancellationToken);

            return serviceResponse.ToHandlerResponse();
        }
    }
}