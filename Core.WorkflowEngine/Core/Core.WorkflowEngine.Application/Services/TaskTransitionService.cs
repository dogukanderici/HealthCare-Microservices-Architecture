using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskTransitionRules;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskTransitionDtos;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Abstractions;
using Core.WorkflowEngine.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Services
{
    public class TaskTransitionService : ITaskTransitionService
    {
        private readonly IRepository<ProcessTaskTransition> _repository;
        private readonly ILogger<TaskTransitionService> _logger;
        private readonly ITaskTransitionBusinessRule _businessRule;

        public TaskTransitionService(IRepository<ProcessTaskTransition> repository, ILogger<TaskTransitionService> logger, ITaskTransitionBusinessRule businessRule)
        {
            _repository = repository;
            _logger = logger;
            _businessRule = businessRule;
        }

        public async Task<InternalServiceResponse<List<ProcessTaskTransition>>> GetDatasByFilterAsync(TaskTransitionFilterDto taskTransitionFilterDto)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = new DBQueryOptions<ProcessTaskTransition>();

            Expression<Func<ProcessTaskTransition, bool>> filter = x => (
            (!taskTransitionFilterDto.ProcessTaskId.HasValue || x.ProcessTaskId == taskTransitionFilterDto.ProcessTaskId) &&
            (!taskTransitionFilterDto.ActionId.HasValue || x.ActionId == taskTransitionFilterDto.ActionId) &&
            (!taskTransitionFilterDto.IsActive.HasValue || x.IsActive == taskTransitionFilterDto.IsActive)
            );
            dBQueryOptions.filter = filter;

            List<ProcessTaskTransition> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalServiceResponse<List<ProcessTaskTransition>>.Success(result);
        }

        public async Task<InternalServiceResponse<ProcessTaskTransition>> GetDataByIdAsync(Guid id)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = new DBQueryOptions<ProcessTaskTransition>();

            Expression<Func<ProcessTaskTransition, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            ProcessTaskTransition result = await _repository.GetDataAsync(dBQueryOptions);

            return InternalServiceResponse<ProcessTaskTransition>.Success(result);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(ProcessTaskTransition entity, CancellationToken cancellationToken)
        {
            Guid id = await _repository.CreateDataAsync(entity);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                nameof(TaskTransitionService),
                LogConstants.SuccessMessages.DataCreatedSuccessfully);

            return InternalServiceResponse<Guid>.Success(id);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(ProcessTaskTransition entity, CancellationToken cancellationToken)
        {
            InternalBusinessRuleResponse<bool> checkExisting = await _businessRule.CheckAllRulesForUpdateAsync(entity);

            if (checkExisting.Data)
            {
                DateTimeOffset result = await _repository.UpdateDataAsync(entity);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(TaskTransitionService),
                    LogConstants.SuccessMessages.DataUpdatedSuccessfully);

                return InternalServiceResponse<DateTimeOffset>.Success(result);
            }

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(TaskTransitionService),
                    LogConstants.ErrorMessages.DataUpdateFailed);

            return InternalServiceResponse<DateTimeOffset>.Failure();
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            InternalBusinessRuleResponse<ProcessTaskTransition> checkExisting = await _businessRule.CheckAllRulesForDeleteAsync(id);

            if (checkExisting.Data != null)
            {
                await _repository.DeleteDataAsync(checkExisting.Data);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(TaskTransitionService),
                    LogConstants.SuccessMessages.DataDeletedSuccessfully);

                return InternalServiceResponse<bool>.Success(true);
            }

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(TaskTransitionService),
                    LogConstants.ErrorMessages.DataUpdateFailed);

            return InternalServiceResponse<bool>.Failure();
        }
    }
}
