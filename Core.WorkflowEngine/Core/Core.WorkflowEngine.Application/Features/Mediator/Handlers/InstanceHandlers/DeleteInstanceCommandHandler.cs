using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands;
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

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers
{
    public class DeleteInstanceCommandHandler : IRequestHandler<DeleteInstanceCommand, InternalCommandResponse<bool>>
    {
        private readonly IRepository<Instance> _repository;
        private readonly ILogger<DeleteInstanceCommandHandler> _logger;
        private readonly IInstanceService _instanceService;

        public DeleteInstanceCommandHandler(IRepository<Instance> repository, ILogger<DeleteInstanceCommandHandler> logger, IInstanceService instanceService)
        {
            _repository = repository;
            _logger = logger;
            _instanceService = instanceService;
        }

        public async Task<InternalCommandResponse<bool>> Handle(DeleteInstanceCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _instanceService.DeleteAsync(request.Id, cancellationToken);

            if (serviceResponse.IsSuccess)
            {
                _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(UpdateInstanceCommandHandler),
                        LogConstants.SuccessMessages.DataDeletedSuccessfully);

                return InternalCommandResponse<bool>.Success(true, InternalCommandConstants.SuccessInstanceDeleting);
            }


            _logger.LogError(LogConstants.LogMessageTemplate,
                nameof(UpdateInstanceCommandHandler),
                LogConstants.ErrorMessages.DataNotFound);

            return InternalCommandResponse<bool>.Failure(InternalCommandConstants.NotFoundData);
        }
    }
}