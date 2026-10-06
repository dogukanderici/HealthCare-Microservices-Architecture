using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Extensions;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Helpers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Domain.Abstractions;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.InstancePolicies
{
    public class InstanceCreatePolicy : PolicyRule<Instance>, IInstanceCreatePolicy
    {
        private readonly IInstanceQueryService _queryService;

        public InstanceCreatePolicy(IInstanceQueryService queryService)
        {
            _queryService = queryService;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(Instance entity)
        {
            DBQueryOptions<Instance> dBQueryOptions = new DBQueryOptions<Instance>();
            dBQueryOptions.filter = x => (
                (x.Number == entity.Number) &&
                (x.Id != entity.Id)
            );

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x > 0, "Aynı instance number değerine sahip birden fazla veri olamaz!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRuleAsync(Instance entity)
        {
            PolicyResponseHelper policyHelper = new PolicyResponseHelper
            {
                ()=>CountExistingDataAsync(entity)
            };

            return await policyHelper.ExecutePolicyRulesAync();
        }
    }
}