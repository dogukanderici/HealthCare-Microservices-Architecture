using Core.WebUI.Application.Interfaces.Services.AuthService;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Core.WebUI.Persistence.ExternalServices.IdentityServer;
using Core.WebUI.Persistence.ExternalServices.WorkflowEngine;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Configuration.Extensions
{
    public static class HttpClientServiceConfiguration
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IProcessDefinitionService, ProcessDefinitionService>();
            services.AddScoped<IDynamicPolicyService, DynamicPolicyService>();
            services.AddScoped<IDynamicRoleService, DynamicRoleService>();

            return services;
        }
    }
}