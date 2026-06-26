using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class CreateProcessDefinitionCommandHandler : IRequestHandler<CreateProcessDefinitionCommand, InternalHandlerResponse<Guid>>,
        IValidationRequest
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly ILogger<CreateProcessDefinitionCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateProcessDefinitionCommandHandler(IRepository<ProcessDefinition> repository, ILogger<CreateProcessDefinitionCommandHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateProcessDefinitionCommand request, CancellationToken cancellationToken)
        {
            ProcessDefinition dataFromDto = _mapper.Map<ProcessDefinition>(request);

            dataFromDto.Id = Guid.NewGuid();

            Guid result = await _repository.CreateDataAsync(dataFromDto);

            return InternalHandlerResponse<Guid>.Success(result, InternalCommandConstants.SuccessProcessDefinitionCreating);
        }
    }
}