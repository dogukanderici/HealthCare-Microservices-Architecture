using Core.WorkflowEngine.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

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
                cfg.AddOpenBehavior(typeof(CachingBehavior<,>));

                cfg.RegisterServicesFromAssembly(typeof(MediatorAssemblyMarker).Assembly);
            });

            return services;
        }
    }
}