using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Extensions;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.AppointmentStatutes;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.AppointmentStatuses
{
    public class AppointmentStatusUpdatePolicy : PolicyRule<AppointmentStatus>, IAppointmentStatusUpdatePolicy
    {
        private readonly IAppointmentStatusQueryService _queryService;
        private readonly IAppointmentStatusCreatePolicy _createPolicy;

        public AppointmentStatusUpdatePolicy(IAppointmentStatusQueryService queryService, IAppointmentStatusCreatePolicy createPolicy)
        {
            _queryService = queryService;
            _createPolicy = createPolicy;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(AppointmentStatus entity)
        {
            DBQueryOptions<AppointmentStatus> dBQueryOptions = new DBQueryOptions<AppointmentStatus>();
            Expression<Func<AppointmentStatus, bool>> filter = x => x.StatusName == entity.StatusName;
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            // Extension metota kontrol kuralı ve kontrolden geçmemesi halinde dönecek mesaj gönderilir.
            return serviceResponse.ToPolicyResponse(x => x != 1, "Gönderilen Id'ye Ait Randevu Durumu Verisi Bulunamadı!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(AppointmentStatus entity)
        {
            // Çalıştırılacak iş kuralları metotları liste içine eklenir.
            // İlk önce Create kuralları çalıştırılır. Update kuralları için kendi içinde öncelik verilir.
            // Hangi metotta hata alınırsa deva edilmez ve alınan hata döndürülür.
            PolicyResponseHelper updateRules = new PolicyResponseHelper
            {
                ()=>_createPolicy.ExecuteAllRulesAsync(entity),
                ()=>CountExistingDataAsync(entity)
            };

            return await updateRules.ToPolicyResponseAsync();
        }
    }
}