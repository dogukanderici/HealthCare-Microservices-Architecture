using Core.WebUI.Application.Interfaces.Services.AuthService;
using Core.WebUI.Persistence.ExternalServices.IdentityServer;
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
            services.AddHttpClient<IAuthService, AuthService>();

            return services;
        }
    }
}