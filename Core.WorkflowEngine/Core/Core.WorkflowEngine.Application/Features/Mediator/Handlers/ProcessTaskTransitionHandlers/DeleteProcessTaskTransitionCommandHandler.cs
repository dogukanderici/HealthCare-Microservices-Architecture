using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskTransitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using MediatR;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskTransitionHandlers
{
    public class DeleteProcessTaskTransitionCommandHandler : IRequestHandler<DeleteProcessTaskTransitionCommand, InternalHandlerResponse<bool>>
    {
        private readonly ITaskTransitionCommandService _service;

        public DeleteProcessTaskTransitionCommandHandler(ITaskTransitionCommandService service)
        {
            _service = service;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteProcessTaskTransitionCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> result = await _service.DeleteAsync(request.Id, cancellationToken);

            if (result.IsSuccess)
            {
                return InternalHandlerResponse<bool>.Success(result.Data, InternalHandlerConstants.SuccessProcessTaskTransitionDeleting);
            }

            return InternalHandlerResponse<bool>.Failure(InternalHandlerConstants.ErrorProcessTaskTransitionDeleting);
        }
    }
}
