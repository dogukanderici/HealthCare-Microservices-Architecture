using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Features.BusinessRules.WorkItemPolicies;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Services.WorkItemServices
{
    public class WorkItemCommandService : IWorkItemCommandService
    {
        private readonly IRepository<WorkItem> _repository;
        private readonly IWorkItemCreatePolicy _createPolicy;
        private readonly IWorkItemUpdatePolicy _updatePolicy;

        public WorkItemCommandService(IRepository<WorkItem> repository, IWorkItemCreatePolicy createPolicy, IWorkItemUpdatePolicy updatePolicy)
        {
            _repository = repository;
            _createPolicy = createPolicy;
            _updatePolicy = updatePolicy;
        }

        public async Task<InternalServiceResponse<WorkItem>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();
            dBQueryOptions.filter = x => x.Id == id;

            WorkItem repoResponse = await _repository.GetDataAsync(dBQueryOptions);

            if (repoResponse == null)
                return InternalServiceResponse<WorkItem>.Failure(InternalServiceResponseConstants.DataNotFound);

            return InternalServiceResponse<WorkItem>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(WorkItem entity, CancellationToken cancellationToken)
        {
            InternalPolicyResponse policyResponse = await _createPolicy.ExecuteAllRuleAsync(entity);

            if (!policyResponse.IsSuccess)
                return InternalServiceResponse<Guid>.Failure(policyResponse.PolicyMessage);

            Guid repoResponse = await _repository.CreateDataAsync(entity);

            return InternalServiceResponse<Guid>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(WorkItem entity)
        {
            InternalPolicyResponse policyResponse = await _updatePolicy.ExecuteAllRuleAsync(entity);

            if (!policyResponse.IsSuccess)
                return InternalServiceResponse<DateTimeOffset>.Failure(policyResponse.PolicyMessage);

            DateTimeOffset repoResponse = await _repository.UpdateDataAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            InternalServiceResponse<WorkItem> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure(existedData.ServiceMessage);

            await _repository.DeleteDataAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}