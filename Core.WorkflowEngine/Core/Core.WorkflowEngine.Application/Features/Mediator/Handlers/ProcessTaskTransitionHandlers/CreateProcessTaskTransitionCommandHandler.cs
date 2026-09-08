using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskTransitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskTransitionHandlers
{
    public class CreateProcessTaskTransitionCommandHandler : IRequestHandler<CreateProcessTaskTransitionCommand, InternalHandlerResponse<Guid>>
    {
        private readonly ITaskTransitionCommandService _service;
        private readonly IMapper _mapper;

        public CreateProcessTaskTransitionCommandHandler(ITaskTransitionCommandService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateProcessTaskTransitionCommand request, CancellationToken cancellationToken)
        {
            ProcessTaskTransition dataFromDto = _mapper.Map<ProcessTaskTransition>(request);

            InternalServiceResponse<Guid> result = await _service.CreateAsync(dataFromDto, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalHandlerResponse<Guid>.Success(result.Data, InternalHandlerConstants.SuccessProcessTaskTransitionCreating);
            }

            return InternalHandlerResponse<Guid>.Failure(InternalHandlerConstants.ErrorProcessTaskTransitionCreating);
        }
    }
}