using Core.WorkflowEngine.Application.Interfaces.HandlerServices.CacheServices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Persistence.CacheProvider
{
    public class CacheCommandProvider : ICacheCommandProvider
    {
        private readonly IDatabase _redisDB;
        private ILogger<CacheQueryProvider> _logger;

        public CacheCommandProvider(IDatabase redisDB, ILogger<CacheQueryProvider> logger)
        {
            _redisDB = redisDB;
            _logger = logger;
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