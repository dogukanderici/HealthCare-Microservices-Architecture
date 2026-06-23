using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands;
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

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskActionHandlers
{
    public class DeleteProcessTaskActionCommandHandler : IRequestHandler<DeleteProcessTaskActionCommand, InternalCommandResponse<bool>>
    {
        private readonly IRepository<ProcessTaskAction> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProcessTaskActionCommandHandler> _logger;

        public DeleteProcessTaskActionCommandHandler(IRepository<ProcessTaskAction> repository, IMapper mapper, ILogger<DeleteProcessTaskActionCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InternalCommandResponse<bool>> Handle(DeleteProcessTaskActionCommand request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTaskAction> dBQueryOptions = new DBQueryOptions<ProcessTaskAction>();

            Expression<Func<ProcessTaskAction, bool>> filter = x => x.Id == request.Id;
            dBQueryOptions.filter = filter;

            ProcessTaskAction result = await _repository.GetDataAsync(dBQueryOptions);

            if (result != null)
            {
                await _repository.DeleteDataAsync(result);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(UpdateProcessTaskActionCommandHandler),
                    LogConstants.SuccessMessages.DataDeletedSuccessfully);

                return InternalCommandResponse<bool>.Success(true, InternalCommandConstants.SuccessProcessTaskActionDeleting);
            }

            _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(UpdateProcessTaskActionCommandHandler),
                    LogConstants.ErrorMessages.DataDeletionFailed);

            return InternalCommandResponse<bool>.Failure(InternalCommandConstants.ErrorProcessTaskActionDeleting);
        }
    }
}