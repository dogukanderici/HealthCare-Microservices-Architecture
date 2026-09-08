using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.CacheServices
{
    public interface ICacheCommandProvider
    {
        Task<bool> SetCacheDataAsync<T>(string key, T data, TimeSpan? expiration = null);
        Task<bool> DeleteCacheDataAsync(string key);
    }
}