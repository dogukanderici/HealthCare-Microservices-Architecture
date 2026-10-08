using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using Core.WorkflowEngine.Application.Features.BusinessRules.InstancePolicies;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;

namespace Core.WorkflowEngine.Application.Services.InstanceServices
{
    public class InstanceCommandService : IInstanceCommandService
    {
        private readonly IRepository<Instance> _repository;
        private readonly IRepository<WorkItem> _wiRepository;
        private readonly ILogger<InstanceQueryService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInstanceCreatePolicy _createPolicy;
        private readonly IInstanceUpdatePolicy _updatePolicy;
        private readonly IProcessTaskQueryService _processTaskService;

        public InstanceCommandService(IRepository<Instance> repository, IRepository<WorkItem> wiRepository, ILogger<InstanceQueryService> logger, IUnitOfWork unitOfWork, IInstanceCreatePolicy createPolicy, IInstanceUpdatePolicy updatePolicy, IProcessTaskQueryService processTaskService)
        {
            _repository = repository;
            _wiRepository = wiRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _createPolicy = createPolicy;
            _updatePolicy = updatePolicy;
            _processTaskService = processTaskService;
        }

        public async Task<InternalServiceResponse<Instance>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<Instance> dBQueryOptions = new DBQueryOptions<Instance>();
            dBQueryOptions.filter = x => x.Id == id;

            Instance result = await _repository.GetDataAsync(dBQueryOptions);

            if (result == null)
                return InternalServiceResponse<Instance>.Failure(InternalServiceResponseConstants.DataNotFound);

            return InternalServiceResponse<Instance>.Success(result);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(Instance entity, CancellationToken cancellationToken)
        {
            InternalPolicyResponse policyResponse = await _createPolicy.ExecuteAllRuleAsync(entity);

            if (!policyResponse.IsSuccess)
            {
                return InternalServiceResponse<Guid>.Failure(policyResponse.PolicyMessage);
            }

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                Guid instanceId = await _repository.CreateDataAsync(entity);

                // Circular Dependency hatası fırlatmaması için öncelikle Instances için ön kayıt yapılır.
                await _unitOfWork.CommitAsync(cancellationToken);

                // ProcessId ile başlangıç adımı bulunur.
                InternalServiceResponse<GetProcessTaskByIdQueryResult> serviceResponse =
                    await _processTaskService.GetDataByProcessIdAsync<GetProcessTaskByIdQueryResult>(entity.ProcessId);
                Guid processTaskId = serviceResponse.Data.Id;

                WorkItem workitemEntity = new WorkItem();
                workitemEntity.InstanceId = entity.Id;
                workitemEntity.StepId = processTaskId; // Başlangıç adımı ProcessId kullanılarak bulunur.

                Guid workitemId = await _wiRepository.CreateDataAsync(workitemEntity);

                entity.InitiatorWorkItemId = workitemId;

                // WorkItems için ön kayıt yapılır.
                await _unitOfWork.CommitAsync(cancellationToken);

                // Transaction tamamlanır.
                await _unitOfWork.CommitTransactionAsync();

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

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(Instance entity)
        {
            InternalPolicyResponse policyResponse = await _updatePolicy.ExecuteAllRuleAsync(entity);

            if (!policyResponse.IsSuccess)
            {
                return InternalServiceResponse<DateTimeOffset>.Failure(policyResponse.PolicyMessage);
            }

            await _repository.UpdateDataAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(DateTimeOffset.UtcNow);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            DBQueryOptions<Instance> dbQueryOptions = new DBQueryOptions<Instance>();

            Expression<Func<Instance, bool>> filter = x => x.Id == id;
            dbQueryOptions.filter = filter;

            Instance existingData = await _repository.GetDataAsync(dbQueryOptions);

            if (existingData != null)
            {

                await _repository.DeleteDataAsync(existingData);

                return InternalServiceResponse<bool>.Success(true);
            }

            return InternalServiceResponse<bool>.Failure();
        }
    }
}