using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskBusinessRules;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskHandlers
{
    public class UpdateProcessTaskCommandHandler : IRequestHandler<UpdateProcessTaskCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly IRepository<ProcessTask> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProcessTaskCommandHandler> _logger;
        private readonly IProcessTaskBusinessRule _businessRule;

        public UpdateProcessTaskCommandHandler(IRepository<ProcessTask> repository, IMapper mapper, ILogger<UpdateProcessTaskCommandHandler> logger, IProcessTaskBusinessRule businessRule)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _businessRule = businessRule;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateProcessTaskCommand request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();

            Expression<Func<ProcessTask, bool>> filter = x => x.Id == request.Id;
            dBQueryOptions.filter = filter;

            // Veri yoksa true döner.
            bool checkExistingData = await _businessRule.CheckExistingDataAsync(dBQueryOptions);

            if (checkExistingData)
            {

                _logger.LogError(LogConstants.LogMessageTemplate,
                         nameof(CreateProcessTaskCommandHandler),
                         LogConstants.ErrorMessages.DataUpdateFailed);

                return InternalHandlerResponse<DateTimeOffset>.Failure(InternalHandlerConstants.NotFoundData);
            }

            ProcessTask dataFromDto = _mapper.Map<ProcessTask>(request);

            await _repository.UpdateDataAsync(dataFromDto);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                 nameof(CreateProcessTaskCommandHandler),
                 LogConstants.SuccessMessages.DataUpdatedSuccessfully);

            return InternalHandlerResponse<DateTimeOffset>.Success(DateTimeOffset.Now, InternalHandlerConstants.SuccessProcessTaskUpdating);
        }
    }
}