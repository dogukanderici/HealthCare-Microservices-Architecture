using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Extensions;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Helpers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.WorkItemPolicies
{
    public class WorkItemUpdatePolicy : PolicyRule<WorkItem>, IWorkItemUpdatePolicy
    {
        private readonly IWorkItemQueryService _queryService;
        private readonly IWorkItemCreatePolicy _createPolicy;

        public WorkItemUpdatePolicy(IWorkItemQueryService queryService, IWorkItemCreatePolicy createPolicy)
        {
            _queryService = queryService;
            _createPolicy = createPolicy;
        }

        private async Task<InternalPolicyResponse> CheckWorkItemStatusAsync(WorkItem entity)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();
            dBQueryOptions.filter = x => (
                (x.Id == entity.Id) &&
                (x.Status == 1)
            );

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "Workitem is unavailable for update due to workitem status! (Workitem status is not waiting!)");
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(WorkItem entity)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();
            dBQueryOptions.filter = x => x.Id == entity.Id;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "Workitem not found for update!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRuleAsync(WorkItem entity)
        {
            PolicyResponseHelper policyHelper = new PolicyResponseHelper
            {
                ()=>_createPolicy.ExecuteAllRuleAsync(entity),
                ()=>CheckWorkItemStatusAsync(entity),
                ()=>CountExistingDataAsync(entity)
            };

            return await policyHelper.ExecutePolicyRulesAync();
        }
    }
}