using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Configurations;
using HealthCare.Operations.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly HealthCareOperationsDbContext _context;

        public Repository(HealthCareOperationsDbContext context)
        {
            _context = context;
        }

        public Task<List<TEntity>> GetDatasAsync(DbQueryOptions<TEntity>? dbQueryOptions = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (dbQueryOptions.filter != null)
            {
                query = query.Where(dbQueryOptions.filter);
            }

            if (dbQueryOptions.thenIncludes == null)
            {
                if (dbQueryOptions.includes != null)
                {
                    foreach (var includeItem in dbQueryOptions.includes)
                    {
                        query = query.Include(includeItem);
                    }
                }
            }

            if (dbQueryOptions.thenIncludes != null)
            {
                foreach (var includeItem in dbQueryOptions.thenIncludes)
                {
                    var includeQuery = query.Include(includeItem.Key);

                    if (dbQueryOptions.thenIncludes?.ContainsKey(includeItem.Key) == true)
                    {
                        foreach (var thenIncludeItem in dbQueryOptions.thenIncludes[includeItem.Key])
                        {
                            includeQuery = includeQuery.ThenInclude(thenIncludeItem);
                        }
                    }

                    query = includeQuery;
                }
            }
            if (dbQueryOptions.shorting != null)
            {
                if (dbQueryOptions.shortingType == "ascending")
                {
                    query = query.OrderBy(dbQueryOptions.shorting);
                }
                else
                {
                    query = query.OrderByDescending(dbQueryOptions.shorting);
                }
            }

            if (dbQueryOptions.DataTakeNumber != -1)
            {
                query = query.Take(dbQueryOptions.DataTakeNumber);
            }

            if (dbQueryOptions.SkipNumber != -1)
            {
                query = query.Skip(dbQueryOptions.SkipNumber);
            }

            return query.ToListAsync();
        }

        public Task<TEntity> GetDataAsync(DbQueryOptions<TEntity>? dbQueryOptions = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (dbQueryOptions.filter != null)
            {
                query = query.Where(dbQueryOptions.filter);
            }

            if (dbQueryOptions.thenIncludes == null)
            {
                if (dbQueryOptions.includes != null)
                {
                    foreach (var includeItem in dbQueryOptions.includes)
                    {
                        query = query.Include(includeItem);
                    }
                }
            }

            if (dbQueryOptions.thenIncludes != null)
            {
                foreach (var includeItem in dbQueryOptions.thenIncludes)
                {
                    var includeQuery = query.Include(includeItem.Key);

                    if (dbQueryOptions.thenIncludes?.ContainsKey(includeItem.Key) == true)
                    {
                        foreach (var thenIncludeItem in dbQueryOptions.thenIncludes[includeItem.Key])
                        {
                            includeQuery = includeQuery.ThenInclude(thenIncludeItem);
                        }
                    }

                    query = includeQuery;
                }
            }

            if (dbQueryOptions.shorting != null)
            {
                if (dbQueryOptions.shortingType == "ascending")
                {
                    query = query.OrderBy(dbQueryOptions.shorting);
                }
                else
                {
                    query = query.OrderByDescending(dbQueryOptions.shorting);
                }
            }

            return query.FirstOrDefaultAsync();
        }

        public IQueryable GetIQueryableDataAsync(DbQueryOptions<TEntity>? dbQueryOptions = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (dbQueryOptions.filter != null)
            {
                query = query.Where(dbQueryOptions.filter);
            }

            if (dbQueryOptions.thenIncludes == null)
            {
                if (dbQueryOptions.includes != null)
                {
                    foreach (var includeItem in dbQueryOptions.includes)
                    {
                        query = query.Include(includeItem);
                    }
                }
            }

            if (dbQueryOptions.thenIncludes != null)
            {
                foreach (var includeItem in dbQueryOptions.thenIncludes)
                {
                    var includeQuery = query.Include(includeItem.Key);

                    if (dbQueryOptions.thenIncludes?.ContainsKey(includeItem.Key) == true)
                    {
                        foreach (var thenIncludeItem in dbQueryOptions.thenIncludes[includeItem.Key])
                        {
                            includeQuery = includeQuery.ThenInclude(thenIncludeItem);
                        }
                    }

                    query = includeQuery;
                }
            }

            if (dbQueryOptions.shorting != null)
            {
                if (dbQueryOptions.shortingType == "ascending")
                {
                    query = query.OrderBy(dbQueryOptions.shorting);
                }
                else
                {
                    query = query.OrderByDescending(dbQueryOptions.shorting);
                }
            }

            if (dbQueryOptions.DataTakeNumber != -1)
            {
                query = query.Take(dbQueryOptions.DataTakeNumber);
            }

            if (dbQueryOptions.SkipNumber != -1)
            {
                query = query.Skip(dbQueryOptions.SkipNumber);
            }

            return query;
        }

        public Task<int> GetDataCountAsync(DbQueryOptions<TEntity>? dbQueryOptions = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (dbQueryOptions.filter != null)
            {
                query = query.Where(dbQueryOptions.filter);
            }

            if (dbQueryOptions.thenIncludes == null)
            {
                if (dbQueryOptions.includes != null)
                {
                    foreach (var includeItem in dbQueryOptions.includes)
                    {
                        query = query.Include(includeItem);
                    }
                }
            }

            if (dbQueryOptions.thenIncludes != null)
            {
                foreach (var includeItem in dbQueryOptions.thenIncludes)
                {
                    var includeQuery = query.Include(includeItem.Key);

                    if (dbQueryOptions.thenIncludes?.ContainsKey(includeItem.Key) == true)
                    {
                        foreach (var thenIncludeItem in dbQueryOptions.thenIncludes[includeItem.Key])
                        {
                            includeQuery = includeQuery.ThenInclude(thenIncludeItem);
                        }
                    }

                    query = includeQuery;
                }
            }

            if (dbQueryOptions.shorting != null)
            {
                if (dbQueryOptions.shortingType == "ascending")
                {
                    query = query.OrderBy(dbQueryOptions.shorting);
                }
                else
                {
                    query = query.OrderByDescending(dbQueryOptions.shorting);
                }
            }

            return query.CountAsync();
        }

        public async Task CreateDataAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDataAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateListDataAsync(List<TEntity> entities)
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            _context.Set<TEntity>().AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateListDataAsync(List<TEntity> entities)
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            _context.Set<TEntity>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDataAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void CreateData(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void UpdateData(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void CreateListData(List<TEntity> entities)
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            _context.Set<TEntity>().AddRange(entities);
        }

        public void UpdateListData(List<TEntity> entities)
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            _context.Set<TEntity>().UpdateRange(entities);
        }

        public void DeleteData(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
