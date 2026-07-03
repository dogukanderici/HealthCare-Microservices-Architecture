using Core.WorkflowEngine.Application.Features.Commons.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Configurations
{
    public static class MediatorServiceRegistration
    {
        public static IServiceCollection AddMediatorServiceRegistration(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                // Behavior Configurations
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));

                cfg.RegisterServicesFromAssembly(typeof(MediatorAssemblyMarker).Assembly);
            });

            return services;
        }
    }
}