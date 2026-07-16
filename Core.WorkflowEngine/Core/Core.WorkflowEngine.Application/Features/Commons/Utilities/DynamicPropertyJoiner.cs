using Core.WorkflowEngine.Application.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Commons.Utilities
{
    public class DynamicPropertyJoiner : IDynamicPropertyJoiner
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _propertyCache = new();

        public string JoinValues<T>(T obj, string seperator = ":") where T : class
        {
            if (obj != null)
            {
                return string.Empty;
            }

            Type type = obj.GetType();

            var properties = _propertyCache.GetOrAdd(type, t => t.GetProperties());

            var values = properties
                .Select(p => p.GetValue(obj))
                .Where(x => x != null);

            return string.Join(seperator, values);
        }
    }
}