using HealthCare.Operations.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        public Task<List<TEntity>> GetDatasAsync(DbQueryOptions<TEntity>? dbQueryOptions = null);
        public Task<TEntity> GetDataAsync(DbQueryOptions<TEntity>? dbQueryOptions = null);
        public IQueryable GetIQueryableDataAsync(DbQueryOptions<TEntity>? dbQueryOptions = null);
        public Task<int> GetDataCountAsync(DbQueryOptions<TEntity>? dbQueryOptions = null);
        public Task CreateDataAsync(TEntity entity);
        public Task UpdateDataAsync(TEntity entity);
        public Task CreateListDataAsync(List<TEntity> entities);
        public Task UpdateListDataAsync(List<TEntity> entities);
        public Task DeleteDataAsync(TEntity entity);
        public void CreateData(TEntity entity);
        public void UpdateData(TEntity entity);
        public void CreateListData(List<TEntity> entities);
        public void UpdateListData(List<TEntity> entities);
        public void DeleteData(TEntity entity);
    }
}
