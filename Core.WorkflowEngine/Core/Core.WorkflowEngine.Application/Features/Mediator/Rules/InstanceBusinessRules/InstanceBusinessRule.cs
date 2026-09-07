using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.InstanceBusinessRules
{
    public class InstanceBusinessRule : IInstanceBusinessRule
    {
        private readonly IBaseBusinessRule<Instance, DBQueryOptions<Instance>> _baseBusinessRule;

        public InstanceBusinessRule(IBaseBusinessRule<Instance, DBQueryOptions<Instance>> baseBusinessRule)
        {
            _baseBusinessRule = baseBusinessRule;
        }

        // Kriterlere uyan aynı veriden başka olup olmadığını kontrol eder.
        public async Task<bool> ExistingInstanceControlAsync(DBQueryOptions<Instance> queryData)
        {
            int dataCount = await _baseBusinessRule.ExistingDataControlAsync(queryData);

            return dataCount == 0;
        }

        // Tüm kurallar çalıştırılır ve true veya false dönen tek bir sonuç üretir.
        public async Task<bool> CheckAllRulesAsync(DBQueryOptions<Instance> queryData)
        {
            bool isExistInstance = await ExistingInstanceControlAsync(queryData);

            return isExistInstance;
        }
    }
}