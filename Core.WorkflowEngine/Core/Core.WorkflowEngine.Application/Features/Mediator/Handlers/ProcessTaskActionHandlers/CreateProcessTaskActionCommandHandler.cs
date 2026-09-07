using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskActionHandlers
{
    public class CreateProcessTaskActionCommandHandler : IRequestHandler<CreateProcessTaskActionCommand, InternalHandlerResponse<Guid>>
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

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateProcessTaskActionCommand request, CancellationToken cancellationToken)
        {
            ProcessTaskAction dataFromDto = _mapper.Map<ProcessTaskAction>(request);

            Guid result = await _repository.CreateDataAsync(dataFromDto);

            return InternalHandlerResponse<Guid>.Success(result, InternalHandlerConstants.SuccessProcessTaskActionCreating);
        }
    }
}