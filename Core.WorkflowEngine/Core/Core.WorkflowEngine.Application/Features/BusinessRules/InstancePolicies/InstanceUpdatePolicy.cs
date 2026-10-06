using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Extensions;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Helpers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.InstancePolicies
{
    public class InstanceUpdatePolicy : PolicyRule<Instance>, IInstanceUpdatePolicy
    {
        private readonly IInstanceQueryService _queryService;
        private readonly IInstanceCreatePolicy _createPolicy;

        public InstanceUpdatePolicy(IInstanceQueryService queryService, IInstanceCreatePolicy createPolicy)
        {
            _queryService = queryService;
            _createPolicy = createPolicy;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(Instance entity)
        {
            DBQueryOptions<Instance> dBQueryOptions = new DBQueryOptions<Instance>();
            dBQueryOptions.filter = x => x.Id == entity.Id;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x == 0, "Data not found");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRuleAsync(Instance entity)
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