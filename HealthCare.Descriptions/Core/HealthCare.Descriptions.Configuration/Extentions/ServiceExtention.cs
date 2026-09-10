using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.AppointmentStatutes;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Cities;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Districts;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Hospitals;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Policlinics;
using HealthCare.Descriptions.Application.Services.HandlerServices.AppointmentStatuses;
using HealthCare.Descriptions.Application.Services.HandlerServices.Cities;
using HealthCare.Descriptions.Application.Services.HandlerServices.Districts;
using HealthCare.Descriptions.Application.Services.HandlerServices.Hospitals;
using HealthCare.Descriptions.Application.Services.HandlerServices.Policlinics;
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

            services.AddScoped(typeof(IHospitalQueryService), typeof(HospitalQueryService));
            services.AddScoped(typeof(IHospitalCommandService), typeof(HospitalCommandService));

            services.AddScoped(typeof(IPoliclinicQueryService), typeof(PoliclinicQueryService));
            services.AddScoped(typeof(IPoliclinicCommandService), typeof(PoliclinicCommandService));

            return services;
        }
    }
}
