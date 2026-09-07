using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Interfaces;

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

        public async Task<TEntity> ExistingDataAsync(TQueryData queryData)
        {
            TEntity existingData = await _repository.GetDataAsync(queryData);

            return existingData;
        }
    }
}