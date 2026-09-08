using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.CacheServices
{
    public interface ICacheQueryProvider
    {
        Task<bool> IsKeyExistsAsync(string key);
        Task<T?> GetCacheDataAsync<T>(string key); // GetCacheDataAsync<T> Ne tür bir veri olacağını çağrıldığı yer belirler.
    }
}