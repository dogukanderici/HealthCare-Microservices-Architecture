using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Extensions;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Helpers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Domain.Entities;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.ProcessDefinitionPolicies
{
    public class ProcessDefinitionCreatePolicy : PolicyRule<ProcessDefinition>, IProcessDefinitionCreatePolicy
    {
        private readonly IProcessDefinitionQueryService _queryService;

        public ProcessDefinitionCreatePolicy(IProcessDefinitionQueryService queryService)
        {
            _queryService = queryService;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(ProcessDefinition entity)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();
            dBQueryOptions.filter = x => (
                (x.ProcessName == entity.ProcessName) &&
                (x.Id != entity.Id)
            );

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x > 0, "Aynı süreç isminden birden fazla olamaz!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRuleAsync(ProcessDefinition entity)
        {
            PolicyResponseHelper policyHelper = new PolicyResponseHelper
            {
                ()=>CountExistingDataAsync(entity)
            };

            return await policyHelper.ExecutePolicyRulesAync();
        }
    }
}