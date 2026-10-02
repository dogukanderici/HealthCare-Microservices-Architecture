using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskHandlers
{
    public class CreateProcessTaskCommandHandler : IRequestHandler<CreateProcessTaskCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IProcessTaskCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateProcessTaskCommandHandler(IProcessTaskCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateProcessTaskCommand request, CancellationToken cancellationToken)
        {
            ProcessTask dataFropmDto = _mapper.Map<ProcessTask>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFropmDto, cancellationToken);

            return serviceResponse.ToHandlerResponse();
        }
    }
}