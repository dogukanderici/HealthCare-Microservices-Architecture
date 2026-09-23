using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Extensions;
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
    public class DistrictCreatePolicy : PolicyRule<District>, IDistrictCreatePolicy
    {
        private readonly IDistrictQueryService _queryService;

        public DistrictCreatePolicy(IDistrictQueryService queryService)
        {
            _queryService = queryService;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(District entity)
        {
            DBQueryOptions<District> dBQueryOptions = new DBQueryOptions<District>();
            Expression<Func<District, bool>> filter = x => (
                (((x.CityId == entity.CityId) && (x.Plate == entity.Plate) && (x.DistrictName == entity.DistrictName)) ||
                ((x.CityId == entity.CityId) && (x.DistrictName == entity.DistrictName)) ||
                ((x.Plate == entity.Plate) && (x.DistrictName == entity.DistrictName)) &&
                (x.Id != entity.Id))
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            // Extension metota kontrol kuralı ve kontrolden geçmemesi halinde dönecek mesaj gönderilir.
            return serviceResponse.ToPolicyResponse(x => x > 0, "Aynı Bölge Birden Fazla Olamaz!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(District entity)
        {
            // Çalıştırılacak iş kuralları metotları liste içine eklenir.
            // Hangi metotta hata alınırsa deva edilmez ve alınan hata döndürülür.
            PolicyResponseHelper createRules = new PolicyResponseHelper
            {
                () => CountExistingDataAsync(entity)
            };

            return await createRules.ToPolicyResponseAsync();
        }
    }
}