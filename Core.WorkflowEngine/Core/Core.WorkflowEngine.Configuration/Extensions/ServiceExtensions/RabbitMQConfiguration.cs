using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Persistence.Commons.Helpers;
using Core.WorkflowEngine.Persistence.Commons.Helpers.Abstracts;
using Core.WorkflowEngine.Persistence.IntegrationServices.RabbitMQ;
using Core.WorkflowEngine.Persistence.IntegrationServices.RabbitMQ.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Configuration.Extensions.ServiceExtensions
{
    public static class RabbitMQConfiguration
    {
        public static IServiceCollection AddRabbitMQConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQOptions>(
                configuration.GetSection("RabbitMQOptions")
                );
            services.AddSingleton(typeof(IRabbitMQConnectionHelper), typeof(RabbitMQConnectionHelper));

            services.AddHostedService<CreateUserEventConsumer>();
            services.AddHostedService<UpdateUserEventConsumer>();

            return services;
        }
    }
}
