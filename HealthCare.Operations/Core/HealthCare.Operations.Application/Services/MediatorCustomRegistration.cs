using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Services
{
    public static class MediatorCustomRegistration
    {
        public static IServiceCollection AddMediatorCustomConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MediatorCustomRegistration).Assembly));

            return services;
        }
    }
}
