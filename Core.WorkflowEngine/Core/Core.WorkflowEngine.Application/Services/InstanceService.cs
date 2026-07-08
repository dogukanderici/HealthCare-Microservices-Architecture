using AutoMapper;
using Core.WorkflowEngine.Application.Features.Constants;
using Core.WorkflowEngine.Application.Features.Mediator.Handlers.InstanceHandlers;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.InstanceBusinessRules;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Configuration.Constants;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Services
{
    public class InstanceService : IInstanceService
    {
        private readonly IRepository<Instance> _repository;
        private readonly IRepository<WorkItem> _wiRepository;
        private readonly IRepository<ProcessTask> _taskRepository;
        private readonly ILogger<InstanceService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInstanceBusinessRule _businessRule;

        public InstanceService(IRepository<Instance> repository, IRepository<WorkItem> wiRepository, ILogger<InstanceService> logger, IUnitOfWork unitOfWork, IInstanceBusinessRule businessRule, IRepository<ProcessTask> taskRepository)
        {
            _repository = repository;
            _wiRepository = wiRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _businessRule = businessRule;
            _taskRepository = taskRepository;
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(Instance entity, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                Guid instanceId = await _repository.CreateDataAsync(entity);

                // Circular Dependency hatası fırlatmaması için öncelikle Instances için ön kayıt yapılır.
                await _unitOfWork.CommitAsync(cancellationToken);

                WorkItem workitemEntity = new WorkItem();
                workitemEntity.InstanceId = entity.Id;
                workitemEntity.StepId = entity.TaskId;

                Guid workitemId = await _wiRepository.CreateDataAsync(workitemEntity);

                entity.InitiatorWorkItemId = workitemId;

                // WorkItems için ön kayıt yapılır.
                await _unitOfWork.CommitAsync(cancellationToken);

                // Transaction tamamlanır.
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(InstanceService),
                        LogConstants.SuccessMessages.DataCreatedSuccessfully);

                return InternalServiceResponse<Guid>.Success(instanceId);
            }
            catch (Exception ex)
            {

                await _unitOfWork.RollbackTransactionAsync();

                _logger.LogError(LogConstants.LogMessageTemplate,
                        nameof(InstanceService),
                        ex);

                return InternalServiceResponse<Guid>.Failure();
            }
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(Instance entity, CancellationToken cancellationToken)
        {
            #region BusinessRule

            DBQueryOptions<Instance> dbQueryOptions = new DBQueryOptions<Instance>();

            Expression<Func<Instance, bool>> filter = x => x.Id == entity.Id;
            dbQueryOptions.filter = filter;

            bool checkAllRules = await _businessRule.CheckAllRulesAsync(dbQueryOptions);
            #endregion

            if (checkAllRules)
            {
                await _repository.UpdateDataAsync(entity);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                            nameof(InstanceService),
                            LogConstants.SuccessMessages.DataUpdatedSuccessfully);

                return InternalServiceResponse<DateTimeOffset>.Success(DateTimeOffset.UtcNow);
            }

            _logger.LogError(LogConstants.LogMessageTemplate,
                        nameof(InstanceService),
                        LogConstants.ErrorMessages.DataUpdateFailed);

            return InternalServiceResponse<DateTimeOffset>.Failure();
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            DBQueryOptions<Instance> dbQueryOptions = new DBQueryOptions<Instance>();

            Expression<Func<Instance, bool>> filter = x => x.Id == id;
            dbQueryOptions.filter = filter;

            Instance existingData = await _repository.GetDataAsync(dbQueryOptions);

            if (existingData != null)
            {

                await _repository.DeleteDataAsync(existingData);

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(InstanceService),
                        LogConstants.SuccessMessages.DataDeletedSuccessfully);

                return InternalServiceResponse<bool>.Success(true);
            }

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(InstanceService),
                        LogConstants.ErrorMessages.DataNotFound);

            return InternalServiceResponse<bool>.Failure();
        }
    }
}