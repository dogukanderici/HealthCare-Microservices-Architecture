using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Extensions;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskHandlers
{
    public class DeleteProcessTaskCommandHandler : IRequestHandler<DeleteProcessTaskCommand, InternalHandlerResponse<bool>>
    {
        private readonly IProcessTaskCommandService _commandService;

        public DeleteProcessTaskCommandHandler(IProcessTaskCommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteProcessTaskCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _commandService.DeleteAsync(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}