using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services
{
    public static class MediatRServiceRegistration
    {
        public static void AddMediatRCustomRegistration(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MediatRServiceRegistration).Assembly));
        }
    }
}
