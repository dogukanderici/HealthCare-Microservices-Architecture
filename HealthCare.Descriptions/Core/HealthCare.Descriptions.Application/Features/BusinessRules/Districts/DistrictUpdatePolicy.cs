using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Districts;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.Districts
{
    public class DistrictUpdatePolicy : PolicyRule<District>, IDistrictUpdatePolicy
    {
        private readonly IDistrictQueryService _queryService;
        private readonly IDistrictCreatePolicy _createPolicy;

        public DistrictUpdatePolicy(IDistrictQueryService queryService, IDistrictCreatePolicy createPolicy)
        {
            _queryService = queryService;
            _createPolicy = createPolicy;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(District entity)
        {
            DBQueryOptions<District> dBQueryOptions = new DBQueryOptions<District>();
            Expression<Func<District, bool>> filter = x => (x.Id == entity.Id);
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            if (serviceResponse.Data < 1)
            {
                return InternalPolicyResponse.Response(false, "Güncellenecek İlçeye Ait Id Bilgisi Bulunamadı!");
            }

            return InternalPolicyResponse.Success();
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(District entity)
        {
            // Çalıştırılacak iş kuralları metotları liste içine eklenir.
            // İlk önce Create kuralları çalıştırılır. Update kuralları için kendi içinde öncelik verilir.
            // Hangi metotta hata alınırsa deva edilmez ve alınan hata döndürülür.
            PolicyResponseHelper updateRules = new PolicyResponseHelper
            {
                () => _createPolicy.ExecuteAllRulesAsync(entity),
                () => CountExistingDataAsync(entity)
            };

            return await updateRules.ToPolicyResponseAsync();
        }
    }
}