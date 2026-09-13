using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
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
    public class DistrictPolicy : PolicyRule<District>, IDistrictPolicy
    {
        private readonly IDistrictQueryService _queryService;

        public DistrictPolicy(IDistrictQueryService queryService)
        {
            _queryService = queryService;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(District entity)
        {
            DBQueryOptions<District> dBQueryOptions = new DBQueryOptions<District>();
            Expression<Func<District, bool>> filter = x => (
                ((x.CityId == entity.CityId) && (x.Plate == entity.Plate) && (x.DistrictName == entity.DistrictName)) ||
                ((x.CityId == entity.CityId) && (x.DistrictName == entity.DistrictName)) ||
                ((x.DistrictName == entity.DistrictName))
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            if (serviceResponse.Data > 0)
            {
                return InternalPolicyResponse.Response(false, "Aynı Bölge Birden Fazla Olamaz!");
            }

            return InternalPolicyResponse.Success();
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(District entity)
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