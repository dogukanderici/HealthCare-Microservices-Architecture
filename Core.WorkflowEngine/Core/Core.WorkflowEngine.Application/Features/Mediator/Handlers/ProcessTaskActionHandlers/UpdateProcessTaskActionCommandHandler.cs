using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskActionBusinessRules;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.Mediator.Handlers.ProcessTaskActionHandlers
{
    public class UpdateProcessTaskActionCommandHandler : IRequestHandler<UpdateProcessTaskActionCommand, InternalHandlerResponse<DateTimeOffset>>
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

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateProcessTaskActionCommand request, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessTaskAction> dBQueryOptions = new DBQueryOptions<ProcessTaskAction>();

            Expression<Func<ProcessTaskAction, bool>> filter = x => x.Id == request.Id;
            dBQueryOptions.filter = filter;

            bool checkBusinessRule = await _businessRule.CheckAllRulesAsync(dBQueryOptions);

            if (checkBusinessRule)
            {

                ProcessTaskAction dataFromDto = _mapper.Map<ProcessTaskAction>(request);

                DateTimeOffset result = await _repository.UpdateDataAsync(dataFromDto);

                return InternalHandlerResponse<DateTimeOffset>.Success(result, InternalHandlerConstants.SuccessProcessTaskActionUpdating);
            }

            return InternalHandlerResponse<DateTimeOffset>.Failure(InternalHandlerConstants.ErrorProcessTaskActionUpdating);
        }
    }
}