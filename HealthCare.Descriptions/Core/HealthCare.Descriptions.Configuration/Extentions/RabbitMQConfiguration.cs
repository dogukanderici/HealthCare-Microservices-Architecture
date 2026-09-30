using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Persistence.IntegrationServices.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Configuration.Extentions
{
    public static class RabbitMQConfiguration
    {
        public static IServiceCollection AddRabbitMQConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQOptions>(
                configuration.GetSection("RabbitMQOptions")
            );
            services.AddHostedService<UserCreateEventConsumer>();
            services.AddHostedService<UserUpdateEventConsumer>();

            return services;
        }
    }
}