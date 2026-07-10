using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces
{
    /**
     * Marker interface to represent a cacheable Queries
     */
    public interface ICacheableQuery
    {
        public string CacheKey { get; }
        public TimeSpan ExpirationTime { get; }
    }
}