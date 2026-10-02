using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class CreateWorkItemCommandHandler : IRequestHandler<CreateWorkItemCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IWorkItemCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateWorkItemCommandHandler(IWorkItemCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            WorkItem dataFromDto = _mapper.Map<WorkItem>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFromDto, cancellationToken);

            return serviceResponse.ToHandlerResponse();
        }
    }
}