using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.InstanceBusinessRules
{
    public class InstanceBusinessRule<TEntity, TQueryData> : IInstanceBusinessRule<TEntity, TQueryData>
        where TEntity : class
        where TQueryData : DBQueryOptions<TEntity>
    {
        private readonly IBaseBusinessRule<TEntity, TQueryData> _baseBusinessRule;

        public InstanceBusinessRule(IBaseBusinessRule<TEntity, TQueryData> baseBusinessRule)
        {
            _baseBusinessRule = baseBusinessRule;
        }

        // Kriterlere uyan aynı veriden başka olup olmadığını kontrol eder.
        public async Task<bool> ExistingInstanceControlAsync(TQueryData queryData)
        {
            int dataCount = await _baseBusinessRule.ExistingDataControlAsync(queryData);

            return dataCount == 0;
        }

        // Tüm kurallar çalıştırılır ve true veya false dönen tek bir sonuç üretir.
        public async Task<bool> CheckAllRulesAsync(TQueryData queryData)
        {
            bool isExistInstance = await ExistingInstanceControlAsync(queryData);

            return isExistInstance;
        }
    }
}