using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Extensions;
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

        public DeleteInstanceCommandHandler(IInstanceCommandService instanceCommandService)
        {
            _instanceCommandService = instanceCommandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteInstanceCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _instanceCommandService.DeleteAsync(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
