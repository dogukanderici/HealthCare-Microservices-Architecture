using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Configurations
{
    public class DbQueryOptions<TEntity>
    {
        public Expression<Func<TEntity, bool>> filter { get; set; }
        public List<Expression<Func<TEntity, object>>> includes { get; set; }
        public Dictionary<Expression<Func<TEntity, object>>, List<Expression<Func<object, object>>>> thenIncludes { get; set; }
        public Expression<Func<TEntity, object>> shorting { get; set; }
        public string shortingType { get; set; } = "ascending";
        public int DataTakeNumber { get; set; } = -1;
        public int SkipNumber { get; set; } = -1;
    }
}
