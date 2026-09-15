using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Extensions;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinics;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.HospitalPoliclinics
{
    public class HospitalPoliclinicUpdatePolicy : PolicyRule<HospitalPoliclinic>, IHospitalPoliclinicUpdatePolicy
    {
        private readonly IHospitalPoliclinicQueryService _queryService;
        private readonly IHospitalPoliclinicCreatePolicy _createPolicy;

        public HospitalPoliclinicUpdatePolicy(IHospitalPoliclinicQueryService queryService, IHospitalPoliclinicCreatePolicy createPolicy)
        {
            _queryService = queryService;
            _createPolicy = createPolicy;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(HospitalPoliclinic entity)
        {
            DBQueryOptions<HospitalPoliclinic> dBQueryOptions = new DBQueryOptions<HospitalPoliclinic>();
            Expression<Func<HospitalPoliclinic, bool>> filter = x => (x.Id == entity.Id);
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            return serviceResponse.ToPolicyResponse(x => x == 0, "Gönderilen Hastane-Poliklinik Id'sine Ait Aktif Veri Bulunamadı!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(HospitalPoliclinic entity)
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