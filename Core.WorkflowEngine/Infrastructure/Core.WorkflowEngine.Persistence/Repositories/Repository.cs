using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Configuration;
using Core.WorkflowEngine.Domain.Abstractions;
using Core.WorkflowEngine.Persistence.Context;
using Core.WorkflowEngine.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Core.WorkflowEngine.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly DBContext _dbContext;

        public Repository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TEntity>> GetAllDataAsync(DBQueryOptions<TEntity>? dBQueryOptions = null)
        {
            List<TEntity> dataList = await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .ApplyQueryOptions(dBQueryOptions) // Filtreler, Sıralama, İlişkilendirmeler Extension Class İle Uygulanır.
                .ToListAsync();

            return dataList;
        }

        public async Task<TEntity> GetDataAsync(DBQueryOptions<TEntity> dBQueryOptions)
        {
            TEntity data = await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .ApplyQueryOptions(dBQueryOptions) // Filtreler, Sıralama, İlişkilendirmeler Extension Class İle Uygulanır.
                .FirstOrDefaultAsync();

            return data;
        }

        public async Task<int> GetAllDataCountAsync(DBQueryOptions<TEntity>? dBQueryOptions = null)
        {
            int dataCount = await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .ApplyQueryOptions(dBQueryOptions) // Filtreler, Sıralama, İlişkilendirmeler Extension Class İle Uygulanır.
                .CountAsync();

            return dataCount;
        }

        public IQueryable<TEntity> GetQuerable(DBQueryOptions<TEntity>? dBQueryOptions = null)
        {
            IQueryable<TEntity> queryEntity = _dbContext.Set<TEntity>()
                .AsNoTracking()
                .ApplyQueryOptions(dBQueryOptions);

            return queryEntity;
        }

        public async Task<Guid> CreateDataAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            // UnitOfWork kullanıldığı için SaveChangesAsync() yazılmadı.

            return entity.Id;
        }

        public Task<DateTimeOffset> UpdateDataAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            // UnitOfWork kullanıldığı için SaveChangesAsync() yazılmadı.

            return Task.FromResult(entity.UpdatedAt);
        }

        public Task DeleteDataAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            // UnitOfWork kullanıldığı için SaveChangesAsync() yazılmadı.

            return Task.CompletedTask;
        }
    }
}