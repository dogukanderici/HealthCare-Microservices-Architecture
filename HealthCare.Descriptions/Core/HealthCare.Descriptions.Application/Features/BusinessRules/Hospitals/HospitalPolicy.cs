using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Cities;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Districts;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Hospitals;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.Hospitals
{
    public class HospitalPolicy : PolicyRule<Hospital>, IHospitalPolicy
    {
        private readonly IHospitalQueryService _queryService;
        private readonly IDistrictQueryService _districtQueryService;

        public HospitalPolicy(IHospitalQueryService queryService, IDistrictQueryService districtQueryService)
        {
            _queryService = queryService;
            _districtQueryService = districtQueryService;
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(Hospital entity)
        {
            DBQueryOptions<Hospital> dBQueryOptions = new DBQueryOptions<Hospital>();
            Expression<Func<Hospital, bool>> filter = x => (
                ((x.HospitalName == entity.HospitalName) || (x.HospitalCode == entity.HospitalCode)) &&
                (((x.HospitalName == entity.HospitalName) || (x.HospitalCode == entity.HospitalCode)) && (x.HospitalCity == entity.HospitalCity))
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            if (serviceResponse.Data > 0)
            {
                return InternalPolicyResponse.Response(false, "Aynı Hastaneden Birden Fazla Olamaz!");
            }

            return InternalPolicyResponse.Success();
        }

        private async Task<InternalPolicyResponse> CheckCityDistrict(Hospital entity)
        {
            DBQueryOptions<District> dBQueryOptions = new DBQueryOptions<District>();
            Expression<Func<District, bool>> filter = x => (
                ((x.CityId == entity.HospitalCity) && (x.Id == entity.HospitalDistrict))
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _districtQueryService.GetDataCountAsync(dBQueryOptions);

            if (serviceResponse.Data > 0)
            {
                return InternalPolicyResponse.Response(false, "Hastaneye Ait Şehir ve İlçe Bilgileri Uyuşmuyor!");
            }

            return InternalPolicyResponse.Success();
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(Hospital entity)
        {
            List<string> errorMessages = new List<string>();

            InternalPolicyResponse dataCount = await CountExistingDataAsync(entity);
            InternalPolicyResponse chekcCityDistrict = await CheckCityDistrict(entity);

            if (dataCount.IsSuccess)
            {
                return InternalPolicyResponse.Success();
            }

            errorMessages.AddRange(dataCount.BusinessRuleError);
            errorMessages.AddRange(chekcCityDistrict?.BusinessRuleError);

            return InternalPolicyResponse.Failure(errorMessages);
        }
    }
}