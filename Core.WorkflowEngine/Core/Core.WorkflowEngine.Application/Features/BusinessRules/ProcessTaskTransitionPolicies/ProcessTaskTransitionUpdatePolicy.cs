using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Extensions;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Helpers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskTransitionPolicies
{
    public class ProcessTaskTransitionUpdatePolicy : PolicyRule<ProcessTaskTransition>, IProcessTaskTransitionUpdatePolicy
    {
        private readonly ITaskTransitionQueryService _queryService;
        private readonly IProcessTaskTransitionCreatePolicy _createPolicy;

        public ProcessTaskTransitionUpdatePolicy(ITaskTransitionQueryService queryService, IProcessTaskTransitionCreatePolicy createPolicy)
        {
            _queryService = queryService;
            _createPolicy = createPolicy;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(ProcessTaskTransition entity)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = new DBQueryOptions<ProcessTaskTransition>();
            dBQueryOptions.filter = x => x.Id == entity.Id;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "Task Transiton must be unique!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRuleAsync(ProcessTaskTransition entity)
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