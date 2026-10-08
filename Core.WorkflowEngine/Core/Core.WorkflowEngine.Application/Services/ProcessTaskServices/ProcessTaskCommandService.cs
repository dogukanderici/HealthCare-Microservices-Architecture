using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskPolicies;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Services.ProcessTaskServices
{
    public class ProcessTaskCommandService : IProcessTaskCommandService
    {
        private readonly IRepository<ProcessTask> _repository;
        private readonly IProcessTaskCreatePolicy _createPolicy;
        private readonly IProcessTaskUpdatePolicy _updatePolicy;

        public ProcessTaskCommandService(IRepository<ProcessTask> repository, IProcessTaskCreatePolicy createPolicy, IProcessTaskUpdatePolicy updatePolicy)
        {
            _repository = repository;
            _createPolicy = createPolicy;
            _updatePolicy = updatePolicy;
        }

        public async Task<InternalServiceResponse<ProcessTask>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();
            dBQueryOptions.filter = x => x.Id == id;

            ProcessTask repoResponse = await _repository.GetDataAsync(dBQueryOptions);

            if (repoResponse == null)
                return InternalServiceResponse<ProcessTask>.Failure(InternalServiceResponseConstants.DataNotFound);

            return InternalServiceResponse<ProcessTask>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(ProcessTask entity, CancellationToken cancellationToken)
        {
            InternalPolicyResponse policyResponse = await _createPolicy.ExecuteAllRuleAsync(entity);

            if (!policyResponse.IsSuccess)
                return InternalServiceResponse<Guid>.Failure(policyResponse.PolicyMessage);

            Guid repoResponse = await _repository.CreateDataAsync(entity);

            return InternalServiceResponse<Guid>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(ProcessTask entity)
        {
            InternalPolicyResponse policyResponse = await _updatePolicy.ExecuteAllRuleAsync(entity);

            if (!policyResponse.IsSuccess)
                return InternalServiceResponse<DateTimeOffset>.Failure(policyResponse.PolicyMessage);

            DateTimeOffset repoResponse = await _repository.UpdateDataAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            InternalServiceResponse<ProcessTask> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure("Data not found.");

            await _repository.DeleteDataAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}