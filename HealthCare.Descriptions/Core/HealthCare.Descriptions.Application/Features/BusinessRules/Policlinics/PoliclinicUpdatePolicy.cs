using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Extensions;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Policlinics;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.Policlinics
{
    public class PoliclinicUpdatePolicy : PolicyRule<Policlinic>, IPoliclinicUpdatePolicy
    {
        private readonly IPoliclinicQueryService _queryService;
        private readonly IPoliclinicCreatePolicy _createPolicy;

        public PoliclinicUpdatePolicy(IPoliclinicQueryService queryService, IPoliclinicCreatePolicy createPolicy)
        {
            _queryService = queryService;
            _createPolicy = createPolicy;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(Policlinic entity)
        {
            DBQueryOptions<Policlinic> dBQueryOptions = new DBQueryOptions<Policlinic>();
            Expression<Func<Policlinic, bool>> filter = x => (x.Id == entity.Id);
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResult = await _queryService.GetDataCountAsync(dBQueryOptions);

            // Extension metota kontrol kuralı ve kontrolden geçmemesi halinde dönecek mesaj gönderilir.
            return serviceResult.ToPolicyResponse(x => x == 0, "Güncellenecek Id'ye Ait Poliklinik Bilgisi Bulunamadı!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(Policlinic entity)
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