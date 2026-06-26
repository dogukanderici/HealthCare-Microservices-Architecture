using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class CreateWorkItemCommandHandler : IRequestHandler<CreateWorkItemCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IWorkItemService _workItemService;

        public CreateWorkItemCommandHandler(IMapper mapper, IWorkItemService workItemService)
        {
            _mapper = mapper;
            _workItemService = workItemService;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            WorkItem dataFromDto = _mapper.Map<WorkItem>(request);

            InternalServiceResponse<Guid> result = await _workItemService.CreateAsync(dataFromDto, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalHandlerResponse<Guid>.Success(result.Data, InternalCommandConstants.SuccessWorkItemCreating);
            }

            return InternalHandlerResponse<Guid>.Failure(InternalCommandConstants.ErrorWorkItemCreating);
        }
    }
}