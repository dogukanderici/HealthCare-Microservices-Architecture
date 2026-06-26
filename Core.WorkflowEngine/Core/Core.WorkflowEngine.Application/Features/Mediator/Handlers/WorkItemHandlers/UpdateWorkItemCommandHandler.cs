using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.WorkItemBusinessRules;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

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
                return InternalHandlerResponse<DateTimeOffset>.Success(result.Data, InternalCommandConstants.SuccessWorkItemUpdating);
            }

            return InternalHandlerResponse<DateTimeOffset>.Failure(InternalCommandConstants.ErrorWorkItemUpdating);
        }
    }
}