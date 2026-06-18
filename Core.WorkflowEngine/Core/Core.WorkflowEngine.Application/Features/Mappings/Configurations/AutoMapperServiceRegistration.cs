using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mappings.Configurations
{
    public static class AutoMapperServiceRegistration
    {
        public static IServiceCollection AddAutoMapperServiceRegistration(this IServiceCollection service)
        {
            service.AddAutoMapper(cfg => { }, typeof(AutoMapperAssemblyMarker));

            return service;
        }
    }
}