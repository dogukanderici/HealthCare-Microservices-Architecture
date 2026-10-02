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
    public class UpdateWorkItemCommandHandler : IRequestHandler<UpdateWorkItemCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly IWorkItemCommandService _commandService;
        private readonly IMapper _mapper;

        public UpdateWorkItemCommandHandler(IWorkItemCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<WorkItem> existedData = await _commandService.GetDataForUpdateAsync(request.Id);

            if (!existedData.IsSuccess)
                return InternalHandlerResponse<DateTimeOffset>.Failure(existedData.ServiceMessage);

            _mapper.Map(request, existedData);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _commandService.UpdateAsync(existedData.Data);

            return serviceResponse.ToHandlerResponse();
        }
    }
}