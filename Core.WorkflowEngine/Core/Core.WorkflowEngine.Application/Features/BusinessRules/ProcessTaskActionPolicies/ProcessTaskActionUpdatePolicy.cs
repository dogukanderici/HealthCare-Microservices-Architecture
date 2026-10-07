using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Extensions;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Helpers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskActionServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskActionPolicies
{
    public class ProcessTaskActionUpdatePolicy : PolicyRule<ProcessTaskAction>, IProcessTaskActionUpdatePolicy
    {
        private readonly IProcessTaskActionQueryService _queryService;
        private readonly IProcessTaskActionCreatePolicy _createPolicy;

        public ProcessTaskActionUpdatePolicy(IProcessTaskActionQueryService queryService, IProcessTaskActionCreatePolicy createPolicy)
        {
            _queryService = queryService;
            _createPolicy = createPolicy;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(ProcessTaskAction entity)
        {
            DBQueryOptions<ProcessTaskAction> dBQueryOptions = new DBQueryOptions<ProcessTaskAction>();
            dBQueryOptions.filter = x => x.Id == entity.Id;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x > 0, "Data not found!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRuleAsync(ProcessTaskAction entity)
        {
            PolicyResponseHelper policyHelper = new PolicyResponseHelper
            {
                ()=>_createPolicy.ExecuteAllRuleAsync(entity),
                ()=>CountExistingDataAsync(entity)
            };

            return await policyHelper.ExecutePolicyRulesAync();
        }
    }
}