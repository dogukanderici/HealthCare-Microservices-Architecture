using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskActionHandlers
{
    public class CreateProcessTaskActionCommandHandler : IRequestHandler<CreateProcessTaskActionCommand, InternalCommandResponse<Guid>>
    {
        private readonly IRepository<ProcessTaskAction> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProcessTaskActionCommandHandler> _logger;

        public CreateProcessTaskActionCommandHandler(IRepository<ProcessTaskAction> repository, IMapper mapper, ILogger<CreateProcessTaskActionCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InternalCommandResponse<Guid>> Handle(CreateProcessTaskActionCommand request, CancellationToken cancellationToken)
        {
            ProcessTaskAction dataFromDto = _mapper.Map<ProcessTaskAction>(request);

            Guid result = await _repository.CreateDataAsync(dataFromDto);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                nameof(CreateProcessTaskActionCommandHandler),
                LogConstants.SuccessMessages.DataCreatedSuccessfully);

            return InternalCommandResponse<Guid>.Success(result, InternalCommandConstants.SuccessProcessTaskActionCreating);
        }
    }
}