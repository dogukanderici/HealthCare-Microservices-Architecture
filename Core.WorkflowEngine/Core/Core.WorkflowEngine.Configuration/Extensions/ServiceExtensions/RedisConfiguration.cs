using Core.WorkflowEngine.Application.Interfaces.HandlerServices.CacheServices;
using Core.WorkflowEngine.Persistence.CacheProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Configuration.Extensions.ServiceExtensions
{
    public static class RedisConfiguration
    {
        public static IServiceCollection AddRedisConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnection = configuration.GetConnectionString("RedisConnectionSettings");
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(redisConnection);
            });

            services.AddScoped<IDatabase>(sp =>
            {
                var multiplexer = sp.GetRequiredService<IConnectionMultiplexer>();
                return multiplexer.GetDatabase();
            });

            services.AddScoped(typeof(ICacheQueryProvider), typeof(CacheQueryProvider));
            services.AddScoped(typeof(ICacheCommandProvider), typeof(CacheCommandProvider));

            return services;
        }
    }
}