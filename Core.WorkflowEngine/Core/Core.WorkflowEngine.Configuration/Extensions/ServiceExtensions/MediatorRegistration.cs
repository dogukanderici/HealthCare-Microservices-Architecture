using Core.WorkflowEngine.Application.Behaviors;
using Core.WorkflowEngine.Application.Features.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace Core.WorkflowEngine.Configuration.Extensions.ServiceExtensions
{
    public static class MediatorRegistration
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
