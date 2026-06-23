using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands;
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
    public class DeleteProcessDefinitionCommandHandler : IRequestHandler<DeleteProcessDefinitionCommand, InternalCommandResponse<bool>>
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly ILogger<DeleteProcessDefinitionCommandHandler> _logger;

        public DeleteProcessDefinitionCommandHandler(IRepository<ProcessDefinition> repository, ILogger<DeleteProcessDefinitionCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<InternalCommandResponse<bool>> Handle(DeleteProcessDefinitionCommand request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => x.Id == request.Id;
            dBQueryOptions.filter = filter;

            ProcessDefinition result = await _repository.GetDataAsync(dBQueryOptions);

            if (result == null)
            {
                _logger.LogError(LogConstants.LogMessageTemplate,
                nameof(DeleteProcessDefinitionCommandHandler),
                $"{LogConstants.ErrorMessages.DataUpdateFailed} Not Found Id: {request.Id}");

                return InternalCommandResponse<bool>.Failure(InternalCommandConstants.NotFoundData);
            }

            await _repository.DeleteDataAsync(result);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                nameof(DeleteProcessDefinitionCommandHandler),
                $"{LogConstants.SuccessMessages.DataDeletedSuccessfully} Deleted Id: {request.Id}");

            return InternalCommandResponse<bool>.Success(true, InternalCommandConstants.SuccessProcessDefinitionDeleting);
        }
    }
}