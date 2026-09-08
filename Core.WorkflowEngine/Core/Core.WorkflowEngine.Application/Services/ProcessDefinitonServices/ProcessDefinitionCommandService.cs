using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessDefinitionBusinessRules;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Application.Services.ProcessDefiniitonServices;
using Core.WorkflowEngine.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Services.ProcessDefinitonServices
{
    public class ProcessDefinitionCommandService : IProcessDefinitionCommandService
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly ILogger<ProcessDefinitionCommandService> _logger;
        private readonly IProcessDefinitionBusinessRule _businessRule;

        public ProcessDefinitionCommandService(IRepository<ProcessDefinition> repository, ILogger<ProcessDefinitionCommandService> logger, IProcessDefinitionBusinessRule businessRule)
        {
            _repository = repository;
            _logger = logger;
            _businessRule = businessRule;
        }

        public async Task<ProcessDefinition> GetWorkItemForUpdateAsync(Guid id)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            ProcessDefinition result = await _repository.GetDataAsync(dBQueryOptions);

            return result;
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(ProcessDefinition entity, CancellationToken cancellationToken)
        {
            Guid result = await _repository.CreateDataAsync(entity);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessDefinitionQueryService),
                    LogConstants.SuccessMessages.DataCreatedSuccessfully);

            return InternalServiceResponse<Guid>.Success(result);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(ProcessDefinition entity, CancellationToken cancellationToken)
        {
            // Veri yoksa true döner.
            bool ruleResult = await _businessRule.ExistingProcessDefinitionDataAsync(entity.Id);

            if (ruleResult)
            {
                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessDefinitionQueryService),
                    LogConstants.ErrorMessages.DataUpdateFailed);

                return InternalServiceResponse<DateTimeOffset>.Failure(LogConstants.ErrorMessages.DataNotFound);
            }

            DateTimeOffset updatedDate = await _repository.UpdateDataAsync(entity);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                nameof(ProcessDefinitionQueryService),
                LogConstants.SuccessMessages.DataUpdatedSuccessfully);

            return InternalServiceResponse<DateTimeOffset>.Success(updatedDate);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            ProcessDefinition existingData = await _repository.GetDataAsync(dBQueryOptions);

            if (existingData != null)
            {
                await _repository.DeleteDataAsync(existingData);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessDefinitionQueryService),
                    LogConstants.SuccessMessages.DataDeletedSuccessfully
                    );

                return InternalServiceResponse<bool>.Success(true);
            }

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessDefinitionQueryService),
                    LogConstants.ErrorMessages.DataDeletionFailed
                    );

            return InternalServiceResponse<bool>.Failure();
        }
    }
}