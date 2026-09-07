using Core.WorkflowEngine.Application.Commons.Parameters;

namespace Core.WorkflowEngine.Application.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        public Task<List<TEntity>> GetAllDataAsync(DBQueryOptions<TEntity>? dBQueryOptions = null);
        public Task<TEntity> GetDataAsync(DBQueryOptions<TEntity> dBQueryOptions);
        public Task<int> GetAllDataCountAsync(DBQueryOptions<TEntity>? dBQueryOptions = null);
        public IQueryable<TEntity> GetQuerable(DBQueryOptions<TEntity>? dBQueryOptions = null);
        public Task<Guid> CreateDataAsync(TEntity entity);
        public Task<DateTimeOffset> UpdateDataAsync(TEntity entity);
        public Task DeleteDataAsync(TEntity entity);
    }
}
