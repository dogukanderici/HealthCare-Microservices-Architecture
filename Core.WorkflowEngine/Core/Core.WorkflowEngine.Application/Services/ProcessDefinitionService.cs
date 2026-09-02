using Core.WorkflowEngine.Application.Features.Mediator.Handlers.WorkItemHandlers;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessDefinitionBusinessRules;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessDefinitionDtos;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Services
{
    public class ProcessDefinitionService : IProcessDefinitionService
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly ILogger<ProcessDefinitionService> _logger;
        private readonly IProcessDefinitionBusinessRule _businessRule;

        public ProcessDefinitionService(IRepository<ProcessDefinition> respository, ILogger<ProcessDefinitionService> logger, IProcessDefinitionBusinessRule businessRule)
        {
            _repository = respository;
            _logger = logger;
            _businessRule = businessRule;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<ProcessDefinition>>> GetDatasAsync()
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            IReadOnlyCollection<ProcessDefinition> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalServiceResponse<IReadOnlyCollection<ProcessDefinition>>.Success(result);
        }

        public async Task<InternalServiceResponse<ProcessDefinition>> GetDataByIdAsync(Guid id)
        {
            DBQueryOptions<ProcessDefinition> dbQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => x.Id == id;
            dbQueryOptions.filter = filter;

            ProcessDefinition result = await _repository.GetDataAsync(dbQueryOptions);

            return InternalServiceResponse<ProcessDefinition>.Success(result);
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<ProcessDefinition>>> GetDatasByFilterAsync(ProcessDefinitionFilterDto filterDto)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => (
            (!filterDto.ProcessSpecId.HasValue || x.ProcessSpecId == filterDto.ProcessSpecId) &&
            (!filterDto.IsActive.HasValue || x.IsActive == filterDto.IsActive) &&
            (string.IsNullOrEmpty(filterDto.ProcessName) || x.ProcessName == filterDto.ProcessName)
            );

            dBQueryOptions.filter = filter;

            IReadOnlyCollection<ProcessDefinition> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalServiceResponse<IReadOnlyCollection<ProcessDefinition>>.Success(result);
        }

        public async Task<InternalServiceResponse<ProcessDefinition>> GetDataForLastestVersionAsync(Guid processSpecId)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => (
                x.ProcessSpecId == processSpecId &&
                x.IsActive == true
            );

            Expression<Func<ProcessDefinition, object>> orderBy = x => x.VersionNumber;

            int sortingType = 1; // Descending
            int dataTakeNumber = 1; // Son versiyonu alır.

            dBQueryOptions.filter = filter;
            dBQueryOptions.sortingType = sortingType;
            dBQueryOptions.orderBy = orderBy;
            dBQueryOptions.DataTakeNumber = dataTakeNumber;

            ProcessDefinition result = await _repository.GetDataAsync(dBQueryOptions);

            return InternalServiceResponse<ProcessDefinition>.Success(result);
        }

        public async Task<InternalServiceResponse<int>> GetDataCount(ProcessDefinitionFilterDto filterDto)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => (
            (!filterDto.ProcessSpecId.HasValue || x.ProcessSpecId == filterDto.ProcessSpecId) &&
            (!filterDto.IsActive.HasValue || x.IsActive == filterDto.IsActive) &&
            (string.IsNullOrEmpty(filterDto.ProcessName) || x.ProcessName == filterDto.ProcessName)
            );

            dBQueryOptions.filter = filter;

            int result = await _repository.GetAllDataCountAsync(dBQueryOptions);

            return InternalServiceResponse<int>.Success(result);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(ProcessDefinition entity, CancellationToken cancellationToken)
        {
            Guid result = await _repository.CreateDataAsync(entity);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessDefinitionService),
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
                    nameof(ProcessDefinitionService),
                    LogConstants.ErrorMessages.DataUpdateFailed);

                return InternalServiceResponse<DateTimeOffset>.Failure(LogConstants.ErrorMessages.DataNotFound);
            }

            DateTimeOffset updatedDate = await _repository.UpdateDataAsync(entity);

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                nameof(ProcessDefinitionService),
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
                    nameof(ProcessDefinitionService),
                    LogConstants.SuccessMessages.DataDeletedSuccessfully
                    );

                return InternalServiceResponse<bool>.Success(true);
            }

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                    nameof(ProcessDefinitionService),
                    LogConstants.ErrorMessages.DataDeletionFailed
                    );

            return InternalServiceResponse<bool>.Failure();
        }
    }
}