using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Persistence.DBContexts;
using HealthCare.Descriptions.Persistence.Repositories;
using HealthCare.Descriptions.Persistence.UnitofWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCare.Descriptions.Configuration.Extentions
{
    public static class DBConfiguration
    {
        public static IServiceCollection AddDBConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // DBContext Configuration
            services.AddDbContext<DBContext>(
                opt => opt.UseNpgsql(configuration.GetConnectionString("DBConnectionSettings"))
                );

            // UnitofWork Configuration
            services.AddScoped(typeof(IUnitofWork), typeof(UnitofWork));

            // Repository Configuration
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}