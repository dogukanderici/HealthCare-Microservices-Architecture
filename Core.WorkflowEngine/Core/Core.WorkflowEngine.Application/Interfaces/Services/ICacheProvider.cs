using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces.Services
{
    public interface ICacheProvider
    {
        Task<bool> IsKeyExistsAsync(string key);
        Task<T?> GetCacheDataAsync<T>(string key); // GetCacheDataAsync<T> Ne tür bir veri olacağını çağrıldığı yer belirler.
        Task<bool> SetCacheDataAsync<T>(string key, T data, TimeSpan? expiration = null);
        Task<bool> DeleteCacheDataAsync(string key);
    }
}