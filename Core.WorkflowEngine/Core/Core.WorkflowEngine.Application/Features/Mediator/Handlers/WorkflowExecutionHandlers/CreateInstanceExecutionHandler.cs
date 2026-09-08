using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkflowExecutionHandlers
{
    public class CreateInstanceExecutionHandler : IRequestHandler<CreateInstanceExecutionCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IInstanceCommandService _instanceCommandService;
        private readonly IMapper _mapper;

        public CreateInstanceExecutionHandler(IInstanceCommandService instanceCommandService, IMapper mapper)
        {
            _instanceCommandService = instanceCommandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateInstanceExecutionCommand request, CancellationToken cancellationToken)
        {
            Instance dataFromDto = _mapper.Map<Instance>(request);

            InternalServiceResponse<Guid> result = await _instanceCommandService.CreateAsync(dataFromDto, cancellationToken);

            return InternalHandlerResponse<Guid>.Success(result.Data);
        }
    }
}
