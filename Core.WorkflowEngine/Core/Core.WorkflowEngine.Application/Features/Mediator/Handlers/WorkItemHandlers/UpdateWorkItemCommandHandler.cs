using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.WorkItemBusinessRules;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers
{
    public class UpdateWorkItemCommandHandler : IRequestHandler<UpdateWorkItemCommand, InternalCommandResponse<DateTimeOffset>>
    {
        private readonly IRepository<WorkItem> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateWorkItemCommandHandler> _logger;
        private readonly IWorkItemBusinessRule _businessRule;

        public UpdateWorkItemCommandHandler(IRepository<WorkItem> wiRepository, IMapper mapper, ILogger<UpdateWorkItemCommandHandler> logger, IWorkItemBusinessRule businessRule)
        {
            _repository = wiRepository;
            _mapper = mapper;
            _logger = logger;
            _businessRule = businessRule;
        }

        public async Task<InternalCommandResponse<DateTimeOffset>> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();

            Expression<Func<WorkItem, bool>> filter = x => x.Id == request.Id;
            dBQueryOptions.filter = filter;

            // Veri yoksa true döner.
            bool CheckData = await _businessRule.CheckExistingDataAsync(dBQueryOptions);

            if (CheckData)
            {
                _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(UpdateWorkItemCommandHandler),
                        LogConstants.ErrorMessages.DataUpdateFailed);

                return InternalCommandResponse<DateTimeOffset>.Failure(InternalCommandConstants.NotFoundData);
            }

            WorkItem dataFromDto = _mapper.Map<WorkItem>(request);

            DateTimeOffset updatedDate = await _repository.UpdateDataAsync(dataFromDto);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                nameof(UpdateWorkItemCommandHandler),
                LogConstants.SuccessMessages.DataUpdatedSuccessfully);

            return InternalCommandResponse<DateTimeOffset>.Success(updatedDate, InternalCommandConstants.SuccessWorkItemUpdating);
        }
    }
}