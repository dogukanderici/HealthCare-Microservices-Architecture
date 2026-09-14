using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Cities;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.Cities
{
    public class CityUpdatePolicy : PolicyRule<City>, ICityUpdatePolicy
    {
        private readonly ICityQueryService _queryService;
        private readonly ICityCreatePolicy _createPolicy;

        public CityUpdatePolicy(ICityQueryService queryService, ICityCreatePolicy createPolicy)
        {
            _queryService = queryService;
            _createPolicy = createPolicy;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(City entity)
        {
            DBQueryOptions<City> dBQueryOptions = new DBQueryOptions<City>();
            Expression<Func<City, bool>> filter = x => (x.Id == entity.Id);
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            if (serviceResponse.Data != 1)
            {
                return InternalPolicyResponse.Response(false, "Güncellenecek Id'ye Ait Şehir Bilgisi Bulunamadı!");
            }

            return InternalPolicyResponse.Success();
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(City entity)
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