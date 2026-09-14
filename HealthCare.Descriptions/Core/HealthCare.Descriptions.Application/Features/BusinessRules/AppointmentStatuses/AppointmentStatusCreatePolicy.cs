using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Extensions;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.AppointmentStatutes;
using HealthCare.Descriptions.Domain.Abstracts;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.AppointmentStatuses
{
    public class AppointmentStatusCreatePolicy : PolicyRule<AppointmentStatus>, IAppointmentStatusCreatePolicy
    {
        private readonly IAppointmentStatusQueryService _queryService;

        public AppointmentStatusCreatePolicy(IAppointmentStatusQueryService queryService)
        {
            _queryService = queryService;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(AppointmentStatus entity)
        {
            DBQueryOptions<AppointmentStatus> dBQueryOptions = new DBQueryOptions<AppointmentStatus>();
            Expression<Func<AppointmentStatus, bool>> filter = x => (
                (x.StatusName == entity.StatusName) &&
                (x.Id != entity.Id)
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            // Extension metota kontrol kuralı ve kontrolden geçmemesi halinde dönecek mesaj gönderilir.
            return serviceResponse.ToPolicyResponse(x => x > 0, "Aynı Randevu Durumundan Birden Fazla Olamaz!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(AppointmentStatus entity)
        {
            // Çalıştırılacak iş kuralları metotları liste içine eklenir.
            // Hangi metotta hata alınırsa deva edilmez ve alınan hata döndürülür.
            PolicyResponseHelper createRules = new PolicyResponseHelper
            {
                ()=>CountExistingDataAsync(entity)
            };

            return await createRules.ToPolicyResponseAsync();
        }
    }
}