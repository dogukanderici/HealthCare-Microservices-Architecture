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
    public class CreateWorkItemCommandHandler : IRequestHandler<CreateWorkItemCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IWorkItemCommandService _workItemCommandService;
        private readonly IMapper _mapper;

        public CreateWorkItemCommandHandler(IWorkItemCommandService workItemCommandService, IMapper mapper)
        {
            _workItemCommandService = workItemCommandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            WorkItem dataFromDto = _mapper.Map<WorkItem>(request);

            InternalServiceResponse<Guid> result = await _workItemCommandService.CreateAsync(dataFromDto, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalHandlerResponse<Guid>.Success(result.Data, InternalHandlerConstants.SuccessWorkItemCreating);
            }

            return InternalHandlerResponse<Guid>.Failure(InternalHandlerConstants.ErrorWorkItemCreating);
        }
    }
}