using HealthCare.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        public Task<List<TEntity>> GetDatasAsync(DbQueryOptions<TEntity>? dbQueryOptions = null);
        public Task<TEntity> GetDataAsync(DbQueryOptions<TEntity>? dbQueryOptions = null);
        public Task<int> GetAllDataCountAsync(DbQueryOptions<TEntity>? dbQueryOptions = null);
        public IQueryable<TEntity> GetQueryableEntity(DbQueryOptions<TEntity>? dbQueryOptions = null);
        public Task CreateDataAsync(TEntity entity);
        public Task UpdateDataAsync(TEntity entity);
        public Task DeleteDataAsync(TEntity entity);
    }
}
