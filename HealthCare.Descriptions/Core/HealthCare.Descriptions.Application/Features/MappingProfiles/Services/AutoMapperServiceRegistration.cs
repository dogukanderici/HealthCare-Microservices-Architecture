using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.MappingProfiles.Services
{
    public static class AutoMapperServiceRegistration
    {
        public static IServiceCollection AddAutoMapperCustomRegistration(this IServiceCollection services)
        {
            // AutoMapper Profile nesneleri ile AutoMapperAssemblyMarker aynı assembly'de olduğundan { } şeklinde tanımlandı.
            services.AddAutoMapper(cfg =>
            { }, typeof(AutoMapperAssemblyMarker));

            return services;
        }
    }
}
