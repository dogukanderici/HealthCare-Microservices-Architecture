using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Helpers;
using Core.WorkflowEngine.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Configuration.Extensions.ServiceExtensions
{
    public static class HelperRegistration
    {
        public static IServiceCollection AddHelperConfiguration(this IServiceCollection services)
        {
            services.AddScoped(typeof(ITokenBasedPaginationHelper<,,>), typeof(TokenBasedPaginationHelper<,,>));

            return services;
        }
    }
}