using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskActionBusinessRules;
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
    public class UpdateProcessTaskActionCommandHandler : IRequestHandler<UpdateProcessTaskActionCommand, InternalCommandResponse<DateTimeOffset>>
    {
        private readonly IRepository<ProcessTaskAction> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProcessTaskActionCommandHandler> _logger;
        private readonly IProcessTaskActionBusinessRule _businessRule;

        public UpdateProcessTaskActionCommandHandler(IRepository<ProcessTaskAction> repository, IMapper mapper, ILogger<UpdateProcessTaskActionCommandHandler> logger, IProcessTaskActionBusinessRule businessRule)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _businessRule = businessRule;
        }

        public async Task<InternalCommandResponse<DateTimeOffset>> Handle(UpdateProcessTaskActionCommand request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTaskAction> dBQueryOptions = new DBQueryOptions<ProcessTaskAction>();

            Expression<Func<ProcessTaskAction, bool>> filter = x => x.Id == request.Id;
            dBQueryOptions.filter = filter;

            bool checkBusinessRule = await _businessRule.CheckAllRulesAsync(dBQueryOptions);

            if (checkBusinessRule)
            {

                ProcessTaskAction dataFromDto = _mapper.Map<ProcessTaskAction>(request);

                DateTimeOffset result = await _repository.UpdateDataAsync(dataFromDto);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(UpdateProcessTaskActionCommandHandler),
                    LogConstants.SuccessMessages.DataUpdatedSuccessfully);

                return InternalCommandResponse<DateTimeOffset>.Success(result, InternalCommandConstants.SuccessProcessTaskActionUpdating);
            }

            _logger.LogError(LogConstants.LogMessageTemplate,
                    nameof(UpdateProcessTaskActionCommandHandler),
                    LogConstants.ErrorMessages.DataUpdateFailed);

            return InternalCommandResponse<DateTimeOffset>.Failure(InternalCommandConstants.ErrorProcessTaskActionUpdating);
        }
    }
}