using HealthCare.Descriptions.Application.Features.Wrappers.Helpers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Persistence.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Configuration.Extentions
{
    public static class HelpersConfiguration
    {
        public static IServiceCollection AddHelperConfiguration(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IEncryptionHelper), typeof(EncryptionHelper));
            services.AddSingleton(typeof(IDecryptionHelper), typeof(DecryptionHelper));

            services.AddScoped(typeof(ITokenBasedPaginationHelper<,,,>), typeof(TokenBasedPaginationHelper<,,,>));

            return services;
        }
    }
}