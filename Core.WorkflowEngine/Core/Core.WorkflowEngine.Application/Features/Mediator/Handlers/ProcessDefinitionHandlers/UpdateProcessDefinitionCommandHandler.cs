using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessDefinitionBusinessRules;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessDefinitionHandlers
{
    public class UpdateProcessDefinitionCommandHandler : IRequestHandler<UpdateProcessDefinitionCommand, InternalCommandResponse<DateTimeOffset>>
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateProcessDefinitionCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IProcessDefinitionBusinessRule _businessRule;

        public UpdateProcessDefinitionCommandHandler(IRepository<ProcessDefinition> repository, IUnitOfWork unitOfWork, ILogger<UpdateProcessDefinitionCommandHandler> logger, IMapper mapper, IProcessDefinitionBusinessRule businessRule)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _businessRule = businessRule;
        }

        public async Task<InternalCommandResponse<DateTimeOffset>> Handle(UpdateProcessDefinitionCommand request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => x.Id == request.Id;
            dBQueryOptions.filter = filter;

            // Veri yoksa true döner;
            bool checkExistData = await _businessRule.ExistingProcessDefinitionDataAsync(dBQueryOptions);

            if (checkExistData)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                nameof(UpdateProcessDefinitionCommandHandler),
                $"{LogConstants.ErrorMessages.DataUpdateFailed} Not Found Id: {request.Id}");

                return InternalCommandResponse<DateTimeOffset>.Failure(InternalCommandConstants.NotFoundData);
            }

            ProcessDefinition dataFromDto = _mapper.Map<ProcessDefinition>(request);

            DateTimeOffset result = await _repository.UpdateDataAsync(dataFromDto);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                nameof(UpdateProcessDefinitionCommandHandler),
                $"{LogConstants.SuccessMessages.DataDeletedSuccessfully} Updated Id: {request.Id}");

            return InternalCommandResponse<DateTimeOffset>.Success(result, InternalCommandConstants.SuccessProcessDefinitionUpdating);
        }
    }
}