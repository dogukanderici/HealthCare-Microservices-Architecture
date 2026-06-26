using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskCommands;
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

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskHandlers
{
    public class DeleteProcessTaskCommandHandler : IRequestHandler<DeleteProcessTaskCommand, InternalHandlerResponse<bool>>
    {
        private readonly IRepository<ProcessTask> _repository;
        private readonly ILogger<DeleteProcessTaskCommandHandler> _logger;

        public DeleteProcessTaskCommandHandler(IRepository<ProcessTask> repository, ILogger<DeleteProcessTaskCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteProcessTaskCommand request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();

            Expression<Func<ProcessTask, bool>> filter = x => x.Id == request.Id;
            dBQueryOptions.filter = filter;

            ProcessTask result = await _repository.GetDataAsync(dBQueryOptions);

            if (result != null)
            {

                await _repository.DeleteDataAsync(result);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(DeleteProcessTaskCommandHandler),
                    InternalCommandConstants.SuccessProcessTaskDeleting);

                return InternalHandlerResponse<bool>.Success(true, InternalCommandConstants.SuccessProcessTaskDeleting);
            }

            _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(DeleteProcessTaskCommandHandler),
                    InternalCommandConstants.NotFoundData);

            return InternalHandlerResponse<bool>.Failure(InternalCommandConstants.NotFoundData);
        }
    }
}