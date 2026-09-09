using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.AppointmentStatutes;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Cities;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Districts;
using HealthCare.Descriptions.Application.Services.HandlerServices.AppointmentStatuses;
using HealthCare.Descriptions.Application.Services.HandlerServices.Cities;
using HealthCare.Descriptions.Application.Services.HandlerServices.Districts;
using HealthCare.Descriptions.Persistence.Services.CurrentUserService;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCare.Descriptions.Configuration.Extentions
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));

            services.AddScoped(typeof(IAppointmentStatusQueryService), typeof(AppointmentStatusQueryService));
            services.AddScoped(typeof(IAppointmentStatusCommandService), typeof(AppointmentStatusCommandService));

            services.AddScoped(typeof(ICityQueryService), typeof(CityQueryService));
            services.AddScoped(typeof(ICityCommandService), typeof(CityCommandService));

            services.AddScoped(typeof(IDistrictQueryService), typeof(DistrictQueryService));
            services.AddScoped(typeof(IDistrictCommandService), typeof(DistrictCommandService));

            return services;
        }
    }
}
