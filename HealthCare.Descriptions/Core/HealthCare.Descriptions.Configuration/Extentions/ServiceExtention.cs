using HealthCare.Descriptions.Application.Behaviors;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.AppointmentStatutes;
using HealthCare.Descriptions.Application.Services.HandlerServices.AppointmentStatuses;
using HealthCare.Descriptions.Persistence.Services.CurrentUserService;
using HealthCare.Descriptions.Persistence.UnitofWork;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCare.Descriptions.Configuration.Extentions
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitofWork), typeof(UnitofWork));

            services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
            services.AddScoped(typeof(IAppointmentStatusQueryService), typeof(AppointmentStatusQueryService));
            services.AddScoped(typeof(IAppointmentStatusCommandService), typeof(AppointmentStatusCommandService));

            return services;
        }
    }
}