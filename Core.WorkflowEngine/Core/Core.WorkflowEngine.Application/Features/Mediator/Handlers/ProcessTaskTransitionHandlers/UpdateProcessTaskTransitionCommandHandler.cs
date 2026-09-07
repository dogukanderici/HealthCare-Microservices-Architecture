using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskTransitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskTransitionHandlers
{
    public class UpdateProcessTaskTransitionCommandHandler : IRequestHandler<UpdateProcessTaskTransitionCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly ITaskTransitionService _service;
        private readonly IMapper _mapper;

        public UpdateProcessTaskTransitionCommandHandler(ITaskTransitionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateProcessTaskTransitionCommand request, CancellationToken cancellationToken)
        {
            ProcessTaskTransition dataFromDto = _mapper.Map<ProcessTaskTransition>(request);

            InternalServiceResponse<DateTimeOffset> result = await _service.UpdateAsync(dataFromDto, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalHandlerResponse<DateTimeOffset>.Success(result.Data, InternalHandlerConstants.SuccessProcessTaskTransitionUpdating);
            }

            return InternalHandlerResponse<DateTimeOffset>.Failure(InternalHandlerConstants.ErrorProcessTaskTransitionUpdating);
        }
    }
}