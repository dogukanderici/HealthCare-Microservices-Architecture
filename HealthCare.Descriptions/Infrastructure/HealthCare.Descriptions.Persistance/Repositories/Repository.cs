using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Persistance.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly HealthCareDescriptionsDbContext _context;

        public Repository(HealthCareDescriptionsDbContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetDatasAsync(DbQueryOptions<TEntity>? dbQueryOptions = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            if (dbQueryOptions != null)
            {

                if (dbQueryOptions.filter != null)
                {
                    query = query.Where(dbQueryOptions.filter);
                }

                if (dbQueryOptions.thenIncludes == null && dbQueryOptions.includes != null)
                {
                    foreach (var includeIem in dbQueryOptions.includes)
                    {

                        query = query.Include(includeIem);
                    }
                }

                if (dbQueryOptions.thenIncludes != null)
                {
                    foreach (var thenIncludeItem in dbQueryOptions.thenIncludes)
                    {
                        var includeQuery = query.Include(thenIncludeItem.Key);

                        if (dbQueryOptions.thenIncludes?.ContainsKey(thenIncludeItem.Key) == true)
                        {
                            foreach (var includeItem in dbQueryOptions.thenIncludes[thenIncludeItem.Key])
                            {
                                includeQuery = includeQuery.ThenInclude(includeItem);
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
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetDataAsync(DbQueryOptions<TEntity>? dbQueryOptions = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            if (dbQueryOptions != null)
            {
                if (dbQueryOptions.filter != null)
                {
                    query = query.Where(dbQueryOptions.filter);
                }

                if (dbQueryOptions.thenIncludes == null && dbQueryOptions.includes != null)
                {
                    foreach (var includeItem in dbQueryOptions.includes)
                    {
                        query = query.Include(includeItem);
                    }
                }

                if (dbQueryOptions.thenIncludes != null)
                {
                    foreach (var thenIncludeItem in dbQueryOptions.thenIncludes)
                    {
                        var includeQuery = query.Include(thenIncludeItem.Key);

                        if (dbQueryOptions.thenIncludes?.ContainsKey(thenIncludeItem.Key) == true)
                        {
                            foreach (var includeItem in dbQueryOptions.thenIncludes[thenIncludeItem.Key])
                            {
                                includeQuery = includeQuery.ThenInclude(includeItem);
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
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> GetAllDataCountAsync(DbQueryOptions<TEntity>? dbQueryOptions = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            if (dbQueryOptions != null)
            {
                if (dbQueryOptions.filter != null)
                {
                    query = query.Where(dbQueryOptions.filter);
                }
            }

            return await query.CountAsync();
        }

        public IQueryable<TEntity> GetQueryableEntity(DbQueryOptions<TEntity>? dbQueryOptions = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (dbQueryOptions != null)
            {
                if (dbQueryOptions.filter != null)
                {
                    query = query.Where(dbQueryOptions.filter);
                }

                if (dbQueryOptions.includes != null)
                {
                    foreach (var includeItem in dbQueryOptions.includes)
                    {
                        query = query.Include(includeItem);
                    }
                }

                if (dbQueryOptions.thenIncludes != null)
                {
                    foreach (var thenIncludeItem in dbQueryOptions.thenIncludes)
                    {
                        var includeQuery = query.Include(thenIncludeItem.Key);

                        if (dbQueryOptions.thenIncludes?.ContainsKey(thenIncludeItem.Key) == true)
                        {
                            foreach (var includeItem in dbQueryOptions.thenIncludes[thenIncludeItem.Key])
                            {
                                includeQuery = includeQuery.ThenInclude(includeItem);
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
            }

            return query;
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

        public async Task DeleteDataAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
