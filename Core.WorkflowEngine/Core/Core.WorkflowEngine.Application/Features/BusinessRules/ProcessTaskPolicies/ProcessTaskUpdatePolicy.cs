using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Extensions;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Helpers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskPolicies
{
    public class ProcessTaskUpdatePolicy : PolicyRule<ProcessTask>, IProcessTaskUpdatePolicy
    {
        private readonly IProcessTaskQueryService _queryService;
        private readonly IProcessTaskCreatePolicy _createPolicy;

        public ProcessTaskUpdatePolicy(IProcessTaskQueryService queryService, IProcessTaskCreatePolicy createPolicy)
        {
            _queryService = queryService;
            _createPolicy = createPolicy;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(ProcessTask entity)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();
            dBQueryOptions.filter = x => (x.Id == entity.Id);

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "Data not found");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRuleAsync(ProcessTask entity)
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