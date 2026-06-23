using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class CreateWorkItemCommandHandler : IRequestHandler<CreateWorkItemCommand, InternalCommandResponse<Guid>>
    {
        private readonly IRepository<WorkItem> _repository;
        private readonly ILogger<CreateWorkItemCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateWorkItemCommandHandler(IRepository<WorkItem> repository, ILogger<CreateWorkItemCommandHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<InternalCommandResponse<Guid>> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            WorkItem dataFromDto = _mapper.Map<WorkItem>(request);

            Guid workitemId = await _repository.CreateDataAsync(dataFromDto);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(CreateWorkItemCommandHandler),
                    LogConstants.SuccessMessages.DataCreatedSuccessfully);

            return InternalCommandResponse<Guid>.Success(workitemId, InternalCommandConstants.SuccessWorkItemCreating);
        }
    }
}