using HealthCare.Descriptions.Application.Features.BusinessRules.AppointmentStatuses;
using HealthCare.Descriptions.Application.Features.BusinessRules.Cities;
using HealthCare.Descriptions.Application.Features.BusinessRules.Districts;
using HealthCare.Descriptions.Application.Features.BusinessRules.Hospitals;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Configuration.Extentions
{
    public static class BusinessRuleRegistration
    {
        public static IServiceCollection AddBusinessRules(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAppointmentStatusPolicy), typeof(AppointmentStatusCreatePolicy));
            services.AddScoped(typeof(ICityPolicy), typeof(CityPolicy));
            services.AddScoped(typeof(IDistrictPolicy), typeof(DistrictPolicy));
            services.AddScoped(typeof(IHospitalPolicy), typeof(HospitalPolicy));

            return services;
        }
    }
}