using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class UpdateWorkItemCommandHandler : IRequestHandler<UpdateWorkItemCommand, InternalCommandResponse<DateTimeOffset>>
    {
        private readonly IRepository<WorkItem> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateWorkItemCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWorkItemCommandHandler(IRepository<WorkItem> wiRepository, IMapper mapper, ILogger<UpdateWorkItemCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _repository = wiRepository;
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<InternalCommandResponse<DateTimeOffset>> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                WorkItem dataFromDto = _mapper.Map<WorkItem>(request);

                DateTimeOffset updatedDate = await _repository.UpdateDataAsync(dataFromDto);

                await _unitOfWork.CommitAsync(cancellationToken);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(UpdateWorkItemCommandHandler),
                    LogConstants.SuccessMessages.DataUpdatedSuccessfully);

                return InternalCommandResponse<DateTimeOffset>.Success(updatedDate, InternalCommandConstants.SuccessWorkItemUpdating);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(UpdateWorkItemCommandHandler),
                    ex);

                return InternalCommandResponse<DateTimeOffset>.Failure(InternalCommandConstants.ErrorWorkItemUpdating);
            }
        }
    }
}