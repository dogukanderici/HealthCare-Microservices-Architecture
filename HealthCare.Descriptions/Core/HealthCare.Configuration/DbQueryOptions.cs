using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Configuration
{
    public class DbQueryOptions<TEntity>
        where TEntity : class
    {
        public Expression<Func<TEntity, bool>> filter { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> includes { get; set; } = null;
        public Dictionary<Expression<Func<TEntity, object>>, List<Expression<Func<object, object>>>> thenIncludes { get; set; } = null;
        public Expression<Func<TEntity, object>> shorting { get; set; } = null;
        public string shortingType { get; set; } = "ascending";
        public int DataTakeNumber { get; set; } = -1;
        public int SkipNumber { get; set; } = -1;
    }
}
