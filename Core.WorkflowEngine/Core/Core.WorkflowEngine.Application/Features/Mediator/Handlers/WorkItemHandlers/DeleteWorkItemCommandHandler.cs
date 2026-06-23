using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers;
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

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class DeleteWorkItemCommandHandler : IRequestHandler<DeleteWorkItemCommand, InternalCommandResponse<bool>>
    {
        private readonly IRepository<WorkItem> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteWorkItemCommandHandler> _logger;

        public DeleteWorkItemCommandHandler(IRepository<WorkItem> repository, IMapper mapper, ILogger<DeleteWorkItemCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InternalCommandResponse<bool>> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();

            Expression<Func<WorkItem, bool>> filter = x => x.Id == request.Id;
            dBQueryOptions.filter = filter;

            WorkItem result = await _repository.GetDataAsync(dBQueryOptions);

            if (result != null)
            {
                await _repository.DeleteDataAsync(result);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(DeleteWorkItemCommandHandler),
                    LogConstants.SuccessMessages.DataDeletedSuccessfully);

                return InternalCommandResponse<bool>.Success(true, InternalCommandConstants.SuccessWorkItemDeleting);
            }

            _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(DeleteWorkItemCommandHandler),
                    LogConstants.SuccessMessages.DataDeletedSuccessfully);

            return InternalCommandResponse<bool>.Failure(InternalCommandConstants.WorkItemNotFound);
        }
    }
}