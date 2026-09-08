using Core.WorkflowEngine.Application.Features.Mappings.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace Core.WorkflowEngine.Configuration.Extensions.ServiceExtensions
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