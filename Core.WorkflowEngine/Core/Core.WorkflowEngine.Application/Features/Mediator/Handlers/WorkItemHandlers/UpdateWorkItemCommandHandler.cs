using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class UpdateWorkItemCommandHandler : IRequestHandler<UpdateWorkItemCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly IMapper _mapper;
        private readonly IWorkItemService _workItemService;

        public UpdateWorkItemCommandHandler(IMapper mapper, IWorkItemService workItemService)
        {
            _mapper = mapper;
            _workItemService = workItemService;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
        {
            WorkItem dataFromDto = _mapper.Map<WorkItem>(request);

            InternalServiceResponse<DateTimeOffset> result = await _workItemService.UpdateAsync(dataFromDto, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalHandlerResponse<DateTimeOffset>.Success(result.Data, InternalHandlerConstants.SuccessWorkItemUpdating);
            }

            return InternalHandlerResponse<DateTimeOffset>.Failure(InternalHandlerConstants.ErrorWorkItemUpdating);
        }
    }
}