using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Extensions;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Helpers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.ProcessDefinitionPolicies
{
    public class ProcessDefinitionUpdatePolicy : PolicyRule<ProcessDefinition>, IProcessDefinitionUpdatePolicy
    {
        private readonly IProcessDefinitionQueryService _queryService;
        private readonly IProcessDefinitionCreatePolicy _createPolicy;

        public ProcessDefinitionUpdatePolicy(IProcessDefinitionQueryService queryService, IProcessDefinitionCreatePolicy createPolicy)
        {
            _queryService = queryService;
            _createPolicy = createPolicy;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(ProcessDefinition entity)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();
            dBQueryOptions.filter = x => (
                (x.Id == entity.Id)
            );

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x == 0, "Data not found!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRuleAsync(ProcessDefinition entity)
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