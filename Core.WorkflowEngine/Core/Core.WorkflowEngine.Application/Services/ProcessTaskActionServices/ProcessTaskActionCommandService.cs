using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskActionPolicies;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskActionServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Services.ProcessTaskActionServices
{
    public class ProcessTaskActionCommandService : IProcessTaskActionCommandService
    {
        private readonly IRepository<ProcessTaskAction> _repository;
        private readonly IProcessTaskActionCreatePolicy _createPolicy;
        private readonly IProcessTaskActionUpdatePolicy _updatePolicy;

        public ProcessTaskActionCommandService(IRepository<ProcessTaskAction> repository, IProcessTaskActionCreatePolicy createPolicy, IProcessTaskActionUpdatePolicy updatePolicy)
        {
            _repository = repository;
            _createPolicy = createPolicy;
            _updatePolicy = updatePolicy;
        }

        public async Task<InternalServiceResponse<ProcessTaskAction>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<ProcessTaskAction> options = new DBQueryOptions<ProcessTaskAction>();
            options.filter = x => x.Id == id;

            ProcessTaskAction repoResponse = await _repository.GetDataAsync(options);

            if (repoResponse == null)
                return InternalServiceResponse<ProcessTaskAction>.Failure("Data not found");

            return InternalServiceResponse<ProcessTaskAction>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(ProcessTaskAction entity, CancellationToken cancellationToken)
        {
            InternalPolicyResponse policyResponse = await _createPolicy.ExecuteAllRuleAsync(entity);

            if (!policyResponse.IsSuccess)
                return InternalServiceResponse<Guid>.Failure(policyResponse.PolicyMessage);

            Guid repoResponse = await _repository.CreateDataAsync(entity);

            return InternalServiceResponse<Guid>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(ProcessTaskAction entity)
        {
            InternalPolicyResponse policyResponse = await _updatePolicy.ExecuteAllRuleAsync(entity);

            if (!policyResponse.IsSuccess)
                return InternalServiceResponse<DateTimeOffset>.Failure(policyResponse.PolicyMessage);

            DateTimeOffset repoResponse = await _repository.UpdateDataAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            InternalServiceResponse<ProcessTaskAction> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure(existedData.ServiceMessage);

            await _repository.DeleteDataAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}