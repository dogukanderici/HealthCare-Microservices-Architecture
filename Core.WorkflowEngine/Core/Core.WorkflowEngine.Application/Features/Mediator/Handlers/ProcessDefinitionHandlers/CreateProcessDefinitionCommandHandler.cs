using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands;
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

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class CreateProcessDefinitionCommandHandler : IRequestHandler<CreateProcessDefinitionCommand, InternalCommandResponse<Guid>>
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly ILogger<CreateProcessDefinitionCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProcessDefinitionCommandHandler(IRepository<ProcessDefinition> repository, ILogger<CreateProcessDefinitionCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<InternalCommandResponse<Guid>> Handle(CreateProcessDefinitionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ProcessDefinition dataFromDto = _mapper.Map<ProcessDefinition>(request);

                dataFromDto.Id = Guid.NewGuid();

                Guid result = await _repository.CreateDataAsync(dataFromDto);

                await _unitOfWork.CommitAsync(cancellationToken);

                return InternalCommandResponse<Guid>.Success(result, InternalCommandConstants.SuccessProcessDefinitionCreating);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(CreateProcessDefinitionCommandHandler),
                    ex);

                return InternalCommandResponse<Guid>.Failure(InternalCommandConstants.ErrorProcessDefinitionCreating);
            }
        }
    }
}