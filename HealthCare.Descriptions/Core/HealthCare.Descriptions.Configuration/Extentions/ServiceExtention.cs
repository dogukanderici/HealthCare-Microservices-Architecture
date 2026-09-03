using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Persistence.Services.CurrentUserService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Configuration.Extentions
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));

            return services;
        }
    }
}