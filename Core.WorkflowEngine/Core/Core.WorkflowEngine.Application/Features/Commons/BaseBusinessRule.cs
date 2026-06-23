using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Commons
{
    public class BaseBusinessRule<TEntity, TQueryData> : IBaseBusinessRule<TEntity, TQueryData>
        where TEntity : class
        where TQueryData : DBQueryOptions<TEntity>
    {
        private readonly IRepository<TEntity> _repository;

        public BaseBusinessRule(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<int> ExistingDataControlAsync(TQueryData queryData)
        {
            int existingDataCount = await _repository.GetAllDataCountAsync(queryData);

            return existingDataCount;
        }
    }
}