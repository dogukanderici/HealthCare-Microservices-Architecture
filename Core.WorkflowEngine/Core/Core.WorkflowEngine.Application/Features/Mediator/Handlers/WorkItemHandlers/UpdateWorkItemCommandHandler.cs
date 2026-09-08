using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class UpdateWorkItemCommandHandler : IRequestHandler<UpdateWorkItemCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly IWorkItemCommandService _workItemCommandService;
        private readonly IMapper _mapper;

        public UpdateWorkItemCommandHandler(IWorkItemCommandService workItemCommandService, IMapper mapper)
        {
            _workItemCommandService = workItemCommandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
        {
            WorkItem existedData = await _workItemCommandService.GetWorkItemForUpdateAsync(request.Id);

            _mapper.Map(request, existedData);

            InternalServiceResponse<DateTimeOffset> result = await _workItemCommandService.UpdateAsync(existedData, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalHandlerResponse<DateTimeOffset>.Success(result.Data, InternalHandlerConstants.SuccessWorkItemUpdating);
            }

            return InternalHandlerResponse<DateTimeOffset>.Failure(InternalHandlerConstants.ErrorWorkItemUpdating);
        }
    }
}