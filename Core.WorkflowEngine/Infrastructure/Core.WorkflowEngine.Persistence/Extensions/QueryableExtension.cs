using Core.WorkflowEngine.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Core.WorkflowEngine.Persistence.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<TEntity> ApplyQueryOptions<TEntity>(this IQueryable<TEntity> query, DBQueryOptions<TEntity> dBQueryOptions)
            where TEntity : class
        {
            if (dBQueryOptions != null)
            {
                if (dBQueryOptions.filter != null)
                {
                    query = query.Where(dBQueryOptions.filter);
                }

                if (dBQueryOptions.thenIncludes == null && dBQueryOptions.includes != null)
                {
                    foreach (var includeItem in dBQueryOptions.includes)
                    {
                        query = query.Include(includeItem);
                    }
                }

                if (dBQueryOptions.thenIncludes != null && dBQueryOptions.includes == null)
                {
                    foreach (var thenIncludeItem in dBQueryOptions.thenIncludes)
                    {
                        var includeQuery = query.Include(thenIncludeItem.Key);

                        if (dBQueryOptions.thenIncludes?.ContainsKey(thenIncludeItem.Key) == true)
                        {
                            foreach (var includeItem in dBQueryOptions.thenIncludes[thenIncludeItem.Key])
                            {
                                includeQuery = includeQuery.ThenInclude(includeItem);
                            }
                        }

                        query = includeQuery;
                    }
                }

                if (dBQueryOptions.orderBy != null)
                {
                    if (dBQueryOptions.sortingType == 0)
                    {
                        query = query.OrderBy(dBQueryOptions.orderBy);

                    }
                    else
                    {
                        query = query.OrderByDescending(dBQueryOptions.orderBy);
                    }

                }

                if (dBQueryOptions.DataSkipNumber != -1)
                {
                    query = query.Skip(dBQueryOptions.DataSkipNumber);
                }

                if (dBQueryOptions.DataTakeNumber != -1)
                {
                    query = query.Take(dBQueryOptions.DataTakeNumber);
                }
            }

            return query;
        }
    }
}
