using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Domain.Abstracts;
using HealthCare.Descriptions.Persistence.DBContexts;
using HealthCare.Descriptions.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Descriptions.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly DBContext _dbContext;

        public Repository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<TEntity>> GetAllAsync(DBQueryOptions<TEntity>? queryOptions = null)
        {
            List<TEntity> datas = await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .ApplyQueryOptions(queryOptions)
                .ToListAsync();

            return datas;
        }

        public async Task<TEntity> GetByIdAsync(DBQueryOptions<TEntity> queryOptions)
        {
            TEntity data = await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .ApplyQueryOptions(queryOptions)
                .FirstOrDefaultAsync();

            return data;
        }

        public IQueryable<TEntity> GetQuearble(DBQueryOptions<TEntity>? queryOptions = null)
        {
            return _dbContext.Set<TEntity>()
                .AsNoTracking()
                .ApplyQueryOptions(queryOptions);
        }
        public async Task<Guid> CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);

            return entity.Id;
        }

        public Task<DateTimeOffset> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);

            return Task.FromResult(entity.UpdatedAt);
        }

        public Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);

            return Task.CompletedTask;
        }
    }
}