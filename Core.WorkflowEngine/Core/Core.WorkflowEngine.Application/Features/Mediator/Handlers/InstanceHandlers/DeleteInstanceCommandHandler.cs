using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class DeleteInstanceCommandHandler : IRequestHandler<DeleteInstanceCommand, InternalHandlerResponse<bool>>
    {
        private readonly IInstanceCommandService _instanceCommandService;
        private readonly ILogger<DeleteInstanceCommandHandler> _logger;

        public DeleteInstanceCommandHandler(IInstanceCommandService instanceCommandService, ILogger<DeleteInstanceCommandHandler> logger)
        {
            _instanceCommandService = instanceCommandService;
            _logger = logger;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteInstanceCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _instanceCommandService.DeleteAsync(request.Id, cancellationToken);

            if (serviceResponse.IsSuccess)
            {
                _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(UpdateInstanceCommandHandler),
                        LogConstants.SuccessMessages.DataDeletedSuccessfully);

                return InternalHandlerResponse<bool>.Success(true, InternalHandlerConstants.SuccessInstanceDeleting);
            }


            _logger.LogError(LogConstants.LogMessageTemplate,
                nameof(UpdateInstanceCommandHandler),
                LogConstants.ErrorMessages.DataNotFound);

            return InternalHandlerResponse<bool>.Failure(InternalHandlerConstants.NotFoundData);
        }
    }
}