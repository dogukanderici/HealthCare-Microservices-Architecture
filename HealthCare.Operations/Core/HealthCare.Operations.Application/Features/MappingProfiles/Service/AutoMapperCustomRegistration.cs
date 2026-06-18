using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.MappingProfiles.Service
{
    public static class AutoMapperCustomRegistration
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfileMarker));

            return services;
        }
    }
}
