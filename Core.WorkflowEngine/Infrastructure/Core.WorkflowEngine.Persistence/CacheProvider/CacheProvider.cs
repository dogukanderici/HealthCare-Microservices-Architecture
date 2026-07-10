using Core.WorkflowEngine.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Core.WorkflowEngine.Persistence.CacheProvider
{
    public class CacheProvider : ICacheProvider
    {
        private readonly IDatabase _redisDB;
        private ILogger<CacheProvider> _logger;

        public CacheProvider(IDatabase redisDB, ILogger<CacheProvider> logger)
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

        public async Task<bool> SetCacheDataAsync<T>(string key, T data, TimeSpan? expiration = null)
        {
            try
            {
                TimeSpan cacheDuration = expiration ?? TimeSpan.FromHours(1);

                string cachedValue = JsonConvert.SerializeObject(data);

                await _redisDB.StringSetAsync(key, cachedValue, cacheDuration);

                _logger.LogInformation($"Value was cached successfully! Cached Key: {key}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Caching process was failed! Error: {ex}");

                return false;
            }
        }

        public async Task<bool> DeleteCacheDataAsync(string key)
        {
            try
            {
                string cachedValue = await _redisDB.StringGetAsync(key);

                await _redisDB.KeyDeleteAsync(key);

                _logger.LogInformation($"Cached value was deleted successfully! Deleted Key: {key}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Key deleting process was failed! Error: {ex}");

                return false;
            }
        }
    }
}