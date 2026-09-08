using Core.WorkflowEngine.Application.Interfaces.HandlerServices.CacheServices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Core.WorkflowEngine.Persistence.CacheProvider
{
    public class CacheQueryProvider : ICacheQueryProvider
    {
        private readonly IDatabase _redisDB;
        private ILogger<CacheQueryProvider> _logger;

        public CacheQueryProvider(IDatabase redisDB, ILogger<CacheQueryProvider> logger)
        {
            _redisDB = redisDB;
            _logger = logger;
        }

        public async Task<bool> IsKeyExistsAsync(string key)
        {
            return await _redisDB.KeyExistsAsync(key);
        }

        public async Task<T?> GetCacheDataAsync<T>(string key)
        {
            string cacheData = await _redisDB.StringGetAsync(key);

            return JsonConvert.DeserializeObject<T>(cacheData);
        }
    }
}
