using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Extensions;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Helpers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskActionServices;
using Core.WorkflowEngine.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskActionPolicies
{
    public class ProcessTaskActionCreatePolicy : PolicyRule<ProcessTaskAction>, IProcessTaskActionCreatePolicy
    {
        private readonly IProcessTaskActionQueryService _queryService;

        public ProcessTaskActionCreatePolicy(IProcessTaskActionQueryService queryService)
        {
            _queryService = queryService;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(ProcessTaskAction entity)
        {
            DBQueryOptions<ProcessTaskAction> dBQueryOptions = new DBQueryOptions<ProcessTaskAction>();
            dBQueryOptions.filter = x => (
                (x.ProcessTaskId == entity.ProcessTaskId) &&
                (x.ActionId == x.ActionId) &&
                (x.ActionName == entity.ActionName) &&
                (x.Id != entity.Id)
            );

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x > 0, "Adımlara ait aksiyonlar tekil olmalıdır!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRuleAsync(ProcessTaskAction entity)
        {
            PolicyResponseHelper policyHelper = new PolicyResponseHelper
            {
                ()=>CountExistingDataAsync(entity)
            };

            return await policyHelper.ExecutePolicyRulesAync();
        }
    }
}