using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers
{
    public static class DBQueryOptionHelper<TEntity>
        where TEntity : class, IEntity
    {
        public static DBQueryOptions<TEntity> CreateQueryOptionForId(TEntity entity)
        {
            DBQueryOptions<TEntity> dBQueryOptions = new DBQueryOptions<TEntity>();
            Expression<Func<TEntity, bool>> filter = x => x.Id == entity.Id;
            dBQueryOptions.filter = filter;

            return dBQueryOptions;
        }
    }
}