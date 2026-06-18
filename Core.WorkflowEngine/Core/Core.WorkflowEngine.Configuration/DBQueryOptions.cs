using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Configuration
{
    public class DBQueryOptions<TEntity>
        where TEntity : class
    {
        public enum SortDirection
        {
            Ascending,
            Descending
        }

        public Expression<Func<TEntity, bool>> filter { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> includes { get; set; } = null;
        public Dictionary<Expression<Func<TEntity, object>>, List<Expression<Func<object, object>>>> thenIncludes { get; set; } = null;
        public Expression<Func<TEntity, object>> orderBy { get; set; } = null;
        public int sortingType { get; set; } = (int)SortDirection.Ascending;
        public int DataTakeNumber { get; set; } = -1;
        public int DataSkipNumber { get; set; } = -1;
    }
}