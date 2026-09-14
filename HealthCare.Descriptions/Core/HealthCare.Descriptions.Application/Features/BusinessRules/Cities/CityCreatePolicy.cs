using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Extensions;
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
    public class CityCreatePolicy : PolicyRule<City>, ICityCreatePolicy
    {
        private readonly ICityQueryService _queryService;

        public CityCreatePolicy(ICityQueryService queryService)
        {
            _queryService = queryService;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(City entity)
        {
            DBQueryOptions<City> dBQueryOptions = new DBQueryOptions<City>();
            Expression<Func<City, bool>> filter = x => ((x.Plate == entity.Plate) || (x.CityName == entity.CityName));
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            // Extension metota kontrol kuralı ve kontrolden geçmemesi halinde dönecek mesaj gönderilir.
            return serviceResponse.ToPolicyResponse(x => x > 0, "Aynı Şehirden Birden Fazla Olamaz!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(City entity)
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