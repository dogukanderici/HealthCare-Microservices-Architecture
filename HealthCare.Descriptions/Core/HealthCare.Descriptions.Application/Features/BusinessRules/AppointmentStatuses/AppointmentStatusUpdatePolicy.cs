using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
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
    public class AppointmentStatusUpdatePolicy : PolicyRule<AppointmentStatus>, IAppointmentStatusPolicy
    {
        private readonly IAppointmentStatusQueryService _queryService;

        public AppointmentStatusUpdatePolicy(IAppointmentStatusQueryService queryService)
        {
            _queryService = queryService;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(AppointmentStatus entity)
        {
            DBQueryOptions<AppointmentStatus> dBQueryOptions = new DBQueryOptions<AppointmentStatus>();
            Expression<Func<AppointmentStatus, bool>> filter = x => x.StatusName == entity.StatusName;
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            if (serviceResponse.Data != 1)
            {
                return InternalPolicyResponse.Response(false, "Gönderilen Id'ye Ait Randevu Durumu Verisi Bulunamadı!");
            }

            return InternalPolicyResponse.Success();
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(AppointmentStatus entity)
        {
            List<string> errorMessages = new List<string>();

            InternalPolicyResponse dataCount = await CountExistingDataAsync(entity);

            if (dataCount.IsSuccess)
            {
                return InternalPolicyResponse.Success();
            }

            errorMessages.AddRange(dataCount.BusinessRuleError);

            return InternalPolicyResponse.Failure(errorMessages);
        }
    }
}