using HealthCare.Descriptions.Application.Features.BusinessRules.AppointmentStatuses;
using HealthCare.Descriptions.Application.Features.BusinessRules.Cities;
using HealthCare.Descriptions.Application.Features.BusinessRules.Districts;
using HealthCare.Descriptions.Application.Features.BusinessRules.HospitalPoliclinicQuotas;
using HealthCare.Descriptions.Application.Features.BusinessRules.HospitalPoliclinics;
using HealthCare.Descriptions.Application.Features.BusinessRules.Hospitals;
using HealthCare.Descriptions.Application.Features.BusinessRules.Policlinics;
using HealthCare.Descriptions.Application.Features.BusinessRules.QuotaTypes;
using HealthCare.Descriptions.Application.Features.BusinessRules.ServiceTypes;
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
            services.AddScoped(typeof(IAppointmentStatusCreatePolicy), typeof(AppointmentStatusCreatePolicy));
            services.AddScoped(typeof(IAppointmentStatusUpdatePolicy), typeof(AppointmentStatusUpdatePolicy));

            services.AddScoped(typeof(ICityCreatePolicy), typeof(CityCreatePolicy));
            services.AddScoped(typeof(ICityUpdatePolicy), typeof(CityUpdatePolicy));

            services.AddScoped(typeof(IDistrictCreatePolicy), typeof(DistrictCreatePolicy));
            services.AddScoped(typeof(IDistrictUpdatePolicy), typeof(DistrictUpdatePolicy));

            services.AddScoped(typeof(IHospitalCreatePolicy), typeof(HospitalCreatePolicy));
            services.AddScoped(typeof(IHospitalUpdatePolicy), typeof(HospitalUpdatePolicy));

            services.AddScoped(typeof(IPoliclinicCreatePolicy), typeof(PoliclinicCreatePolicy));
            services.AddScoped(typeof(IPoliclinicUpdatePolicy), typeof(PoliclinicUpdatePolicy));

            services.AddScoped(typeof(IQuotaTypeCreatePolicy), typeof(QuotaTypeCreatePolicy));
            services.AddScoped(typeof(IQuotaTypeUpdatePolicy), typeof(QuotaTypeUpdatePolicy));

            services.AddScoped(typeof(IServiceTypeCreatePolicy), typeof(ServiceTypeCreatePolicy));
            services.AddScoped(typeof(IServiceTypeUpdatePolicy), typeof(ServiceTypeUpdatePolicy));

            services.AddScoped(typeof(IHospitalPoliclinicCreatePolicy), typeof(HospitalPoliclinicCreatePolicy));
            services.AddScoped(typeof(IHospitalPoliclinicUpdatePolicy), typeof(HospitalPoliclinicUpdatePolicy));

            services.AddScoped(typeof(IHospitalPoliclinicQuotaCreatePolicy), typeof(HospitalPoliclinicQuotaCreatePolicy));
            services.AddScoped(typeof(IHospitalPoliclinicQuotaUpdatePolicy), typeof(HospitalPoliclinicQuotaUpdatePolicy));

            return services;
        }
    }
}