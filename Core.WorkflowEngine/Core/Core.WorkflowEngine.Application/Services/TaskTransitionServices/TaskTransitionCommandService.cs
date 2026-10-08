using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskTransitionPolicies;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Services.TaskTransitionServices
{
    public class TaskTransitionCommandService : ITaskTransitionCommandService
    {
        private readonly IRepository<ProcessTaskTransition> _repository;
        private readonly IProcessTaskTransitionCreatePolicy _createPolicy;
        private readonly IProcessTaskTransitionUpdatePolicy _updatePolicy;

        public TaskTransitionCommandService(IRepository<ProcessTaskTransition> repository, IProcessTaskTransitionCreatePolicy createPolicy, IProcessTaskTransitionUpdatePolicy updatePolicy)
        {
            _repository = repository;
            _createPolicy = createPolicy;
            _updatePolicy = updatePolicy;
        }

        public async Task<InternalServiceResponse<ProcessTaskTransition>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = new DBQueryOptions<ProcessTaskTransition>();
            dBQueryOptions.filter = x => x.Id == id;

            ProcessTaskTransition repoResponse = await _repository.GetDataAsync(dBQueryOptions);

            if (repoResponse == null)
            {
                return InternalServiceResponse<ProcessTaskTransition>.Failure(InternalServiceResponseConstants.DataNotFound);
            }

            return InternalServiceResponse<ProcessTaskTransition>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(ProcessTaskTransition entity, CancellationToken cancellationToken)
        {
            InternalPolicyResponse policyResponse = await _createPolicy.ExecuteAllRuleAsync(entity);

            if (!policyResponse.IsSuccess)
                return InternalServiceResponse<Guid>.Failure(policyResponse.PolicyMessage);

            Guid repoResponse = await _repository.CreateDataAsync(entity);

            return InternalServiceResponse<Guid>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(ProcessTaskTransition entity)
        {
            InternalPolicyResponse policyResponse = await _updatePolicy.ExecuteAllRuleAsync(entity);

            if (!policyResponse.IsSuccess)
                return InternalServiceResponse<DateTimeOffset>.Failure(policyResponse.PolicyMessage);

            DateTimeOffset repoResponse = await _repository.UpdateDataAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            InternalServiceResponse<ProcessTaskTransition> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure(existedData.ServiceMessage);

            await _repository.DeleteDataAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}