using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.AppointmentStatutes;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Cities;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Districts;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinicQuotas;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinics;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Hospitals;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Policlinics;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.QuotaTypes;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.ServiceType;
using HealthCare.Descriptions.Application.Services.HandlerServices.AppointmentStatuses;
using HealthCare.Descriptions.Application.Services.HandlerServices.Cities;
using HealthCare.Descriptions.Application.Services.HandlerServices.Districts;
using HealthCare.Descriptions.Application.Services.HandlerServices.HospitalPoliclinicQuotas;
using HealthCare.Descriptions.Application.Services.HandlerServices.HospitalPoliclinics;
using HealthCare.Descriptions.Application.Services.HandlerServices.Hospitals;
using HealthCare.Descriptions.Application.Services.HandlerServices.Policlinics;
using HealthCare.Descriptions.Application.Services.HandlerServices.QuotaTypes;
using HealthCare.Descriptions.Application.Services.HandlerServices.ServiceTypes;
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

            services.AddScoped(typeof(IQuotaTypeQueryService), typeof(QuotaTypeQueryService));
            services.AddScoped(typeof(IQuotaTypeCommandService), typeof(QuotaTypeCommandService));

            services.AddScoped(typeof(IServiceTypeQueryService), typeof(ServiceTypeQueryService));
            services.AddScoped(typeof(IServiceTypeCommandService), typeof(ServiceTypeCommandService));

            services.AddScoped(typeof(IHospitalPoliclinicQueryService), typeof(HospitalPoliclinicQueryService));
            services.AddScoped(typeof(IHospitalPoliclinicCommandService), typeof(HospitalPoliclinicCommandService));

            services.AddScoped(typeof(IHospitalPoliclinicQuotaQueryService), typeof(HospitalPoliclinicQuotaQueryService));
            services.AddScoped(typeof(IHospitalPoliclinicQuotaCommandService), typeof(HospitalPoliclinicQuotaCommandService));

            return services;
        }
    }
}