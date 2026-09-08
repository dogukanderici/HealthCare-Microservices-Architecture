using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Persistence.Context;
using Core.WorkflowEngine.Persistence.Repositories;
using Core.WorkflowEngine.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.WorkflowEngine.Configuration.Extensions.ServiceExtensions
{
    public static class DBConfiguration
    {
        public static IServiceCollection AddDbConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<DBContext>(
                opt => opt.UseNpgsql(configuration.GetConnectionString("DBConnectionSettings"))
            );

            // Repository Configuration
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // UnitOfWork Configuration
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}