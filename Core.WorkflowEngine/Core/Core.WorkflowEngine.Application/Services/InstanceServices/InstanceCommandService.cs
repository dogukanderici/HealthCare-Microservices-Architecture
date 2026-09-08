using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.InstanceBusinessRules;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Services.InstanceServices
{
    public class InstanceCommandService : IInstanceCommandService
    {
        private readonly IRepository<Instance> _repository;
        private readonly IRepository<WorkItem> _wiRepository;
        private readonly ILogger<InstanceQueryService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInstanceBusinessRule _businessRule;
        private readonly IProcessTaskQueryService _processTaskService;

        public InstanceCommandService(IRepository<Instance> repository, IRepository<WorkItem> wiRepository, ILogger<InstanceQueryService> logger, IUnitOfWork unitOfWork, IInstanceBusinessRule businessRule, IProcessTaskQueryService processTaskService)
        {
            _repository = repository;
            _wiRepository = wiRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _businessRule = businessRule;
            _processTaskService = processTaskService;
        }
        public async Task<Instance> GetWorkItemForUpdateAsync(Guid id)
        {
            DBQueryOptions<Instance> dBQueryOptions = new DBQueryOptions<Instance>();

            Expression<Func<Instance, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            Instance result = await _repository.GetDataAsync(dBQueryOptions);

            return result;
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(Instance entity, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                Guid instanceId = await _repository.CreateDataAsync(entity);

                // Circular Dependency hatası fırlatmaması için öncelikle Instances için ön kayıt yapılır.
                await _unitOfWork.CommitAsync(cancellationToken);

                // ProcessId ile başlangıç adımı bulunur.
                ProcessTask taskData = await _processTaskService.GetDataByProcessIdAsync(entity.ProcessId);
                Guid processTaskId = taskData.Id;

                WorkItem workitemEntity = new WorkItem();
                workitemEntity.InstanceId = entity.Id;
                workitemEntity.StepId = processTaskId; // Başlangıç adımı ProcessId kullanılarak bulunur.

                Guid workitemId = await _wiRepository.CreateDataAsync(workitemEntity);

                entity.InitiatorWorkItemId = workitemId;

                // WorkItems için ön kayıt yapılır.
                await _unitOfWork.CommitAsync(cancellationToken);

                // Transaction tamamlanır.
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(InstanceQueryService),
                        LogConstants.SuccessMessages.DataCreatedSuccessfully);

                return InternalServiceResponse<Guid>.Success(instanceId);
            }
            catch (Exception ex)
            {

                await _unitOfWork.RollbackTransactionAsync();

                _logger.LogError(LogConstants.LogMessageTemplate,
                        nameof(InstanceQueryService),
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
                            nameof(InstanceQueryService),
                            LogConstants.SuccessMessages.DataUpdatedSuccessfully);

                return InternalServiceResponse<DateTimeOffset>.Success(DateTimeOffset.UtcNow);
            }

            _logger.LogError(LogConstants.LogMessageTemplate,
                        nameof(InstanceQueryService),
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
                        nameof(InstanceQueryService),
                        LogConstants.SuccessMessages.DataDeletedSuccessfully);

                return InternalServiceResponse<bool>.Success(true);
            }

            _logger.LogInformation(LogConstants.LogMessageTemplate,
                        nameof(InstanceQueryService),
                        LogConstants.ErrorMessages.DataNotFound);

            return InternalServiceResponse<bool>.Failure();
        }
    }
}