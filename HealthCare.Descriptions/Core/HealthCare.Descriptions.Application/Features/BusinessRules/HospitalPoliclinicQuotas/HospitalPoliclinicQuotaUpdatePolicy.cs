using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Extensions;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinicQuotas;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.HospitalPoliclinicQuotas
{
    public class HospitalPoliclinicQuotaUpdatePolicy : PolicyRule<HospitalPoliclinicQuota>, IHospitalPoliclinicQuotaUpdatePolicy
    {
        private readonly IHospitalPoliclinicQuotaQueryService _queryService;
        private readonly IHospitalPoliclinicQuotaCreatePolicy _createPolicy;

        public HospitalPoliclinicQuotaUpdatePolicy(IHospitalPoliclinicQuotaQueryService queryService, IHospitalPoliclinicQuotaCreatePolicy createPolicy)
        {
            _queryService = queryService;
            _createPolicy = createPolicy;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(HospitalPoliclinicQuota entity)
        {
            DBQueryOptions<HospitalPoliclinicQuota> dBQueryOptions = new DBQueryOptions<HospitalPoliclinicQuota>();
            Expression<Func<HospitalPoliclinicQuota, bool>> filter = x => (
                (x.Id == entity.Id)
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            return serviceResponse.ToPolicyResponse(x => x == 0, "Gönderilen Hastane-Poliklinik-Kota Id'sine Ait Veri Bulunamadı!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(HospitalPoliclinicQuota entity)
        {
            PolicyResponseHelper updatePolicy = new PolicyResponseHelper
            {
                ()=>_createPolicy.ExecuteAllRulesAsync(entity),
                ()=>CountExistingDataAsync(entity)
            };

            return await updatePolicy.ToPolicyResponseAsync();
        }
    }
}