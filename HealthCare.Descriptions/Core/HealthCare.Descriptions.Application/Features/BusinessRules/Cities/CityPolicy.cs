using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
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
    public class CityPolicy : PolicyRule<City>, ICityPolicy
    {
        private readonly ICityQueryService _queryService;

        public CityPolicy(ICityQueryService queryService)
        {
            _queryService = queryService;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(City entity)
        {
            DBQueryOptions<City> dBQueryOptions = new DBQueryOptions<City>();
            Expression<Func<City, bool>> filter = x => ((x.Plate == entity.Plate) || (x.CityName == entity.CityName));
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            if (serviceResponse.Data > 0)
            {
                return InternalPolicyResponse.Response(false, "Aynı Şehirden Birden Fazla Olamaz!");
            }

            return InternalPolicyResponse.Success();
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(City entity)
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