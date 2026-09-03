using HealthCare.Descriptions.Application.Common.Parameters;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Descriptions.Persistence.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<TEntity> ApplyQueryOptions<TEntity>(this IQueryable<TEntity> query, DBQueryOptions<TEntity>? queryOptions)
            where TEntity : class
        {
            if (queryOptions != null)
            {

                if (queryOptions.filter != null)
                {
                    query = query.Where(queryOptions.filter);
                }

                // Includes ve ThenIncludes aynı anda dolu gönderilirse sadece Includes çalışacak.
                if (queryOptions.includes != null)
                {
                    foreach (var item in queryOptions.includes)
                    {
                        query = query.Include(item);
                    }
                }

                // ThenIncludes çalışması için includes null olmalı.
                // Dictionary yapısında geldiğinden Key değeri Include için Value değeri ise ThenInclude için kullanılır.
                if (queryOptions.includes == null && queryOptions.thenIncludes != null)
                {
                    foreach (var item in queryOptions.thenIncludes)
                    {
                        var includeQuery = query.Include(item.Key);

                        if (queryOptions.thenIncludes?.ContainsKey(item.Key) == true)
                        {
                            foreach (var thenIncludeItem in queryOptions.thenIncludes[item.Key])
                            {
                                includeQuery = includeQuery.ThenInclude(thenIncludeItem);
                            }
                        }

                        query = includeQuery;
                    }
                }

                if (queryOptions.orderBy != null)
                {
                    // 0 -> Ascending, 1 -> Descending
                    if (queryOptions.sortingType == 0)
                    {
                        query = query.OrderBy(queryOptions.orderBy);
                    }
                    else
                    {
                        query = query.OrderByDescending(queryOptions.orderBy);
                    }
                }

                if (queryOptions.DataSkipNumber != -1)
                {
                    query = query.Skip(queryOptions.DataSkipNumber);
                }

                if (queryOptions.DataTakeNumber != -1)
                {
                    query = query.Take(queryOptions.DataTakeNumber);
                }
            }

            return query;
        }
    }
}