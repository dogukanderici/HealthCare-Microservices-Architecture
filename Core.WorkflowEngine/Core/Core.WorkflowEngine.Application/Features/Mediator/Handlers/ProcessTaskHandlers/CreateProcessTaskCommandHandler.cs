using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskHandlers
{
    public class CreateProcessTaskCommandHandler : IRequestHandler<CreateProcessTaskCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IRepository<ProcessTask> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProcessTaskCommandHandler> _logger;

        public CreateProcessTaskCommandHandler(IRepository<ProcessTask> repository, IMapper mapper, ILogger<CreateProcessTaskCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateProcessTaskCommand request, CancellationToken cancellationToken)
        {
            ProcessTask dataFropmDto = _mapper.Map<ProcessTask>(request);

            Guid id = await _repository.CreateDataAsync(dataFropmDto);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                     nameof(CreateProcessTaskCommandHandler),
                     LogConstants.SuccessMessages.DataCreatedSuccessfully);

            return InternalHandlerResponse<Guid>.Success(id, InternalHandlerConstants.SuccessProcessTaskCreating);
        }
    }
}