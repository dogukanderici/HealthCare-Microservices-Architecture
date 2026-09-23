using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Extensions;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinics;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Hospitals;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Policlinics;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.HospitalPoliclinics
{
    public class HospitalPoliclinicCreatePolicy : PolicyRule<HospitalPoliclinic>, IHospitalPoliclinicCreatePolicy
    {
        private readonly IHospitalPoliclinicQueryService _queryService;
        private readonly IHospitalQueryService _hospitalQueryService;
        private readonly IPoliclinicQueryService _policlinicQueryService;

        public HospitalPoliclinicCreatePolicy(IHospitalPoliclinicQueryService queryService, IHospitalQueryService hospitalQueryService, IPoliclinicQueryService policlinicQueryService)
        {
            _queryService = queryService;
            _hospitalQueryService = hospitalQueryService;
            _policlinicQueryService = policlinicQueryService;
        }

        // Gönderilen Hastane Id'si aktif durumda ve varolan bir kayıt mı kontrolünü yapar.
        private async Task<InternalPolicyResponse> CheckHospitalByIdAsync(Guid id)
        {
            DBQueryOptions<Hospital> dBQueryOptions = new DBQueryOptions<Hospital>();
            Expression<Func<Hospital, bool>> filter = x => (
                (x.Id == id) && (x.IsAvailable == true)
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _hospitalQueryService.GetDataCountAsync(dBQueryOptions);

            return serviceResponse.ToPolicyResponse(x => x == 0, "Gönderilen Hastane Id'sine Ait Aktif Veri Bulunamadı!");
        }

        // Gönderilen Policlinic Id'si aktif durumda ve varolan bir kayıt mı kontrolünü yapar.
        private async Task<InternalPolicyResponse> CheckPoliclinicByIdAsync(Guid id)
        {
            DBQueryOptions<Policlinic> dBQueryOptions = new DBQueryOptions<Policlinic>();
            Expression<Func<Policlinic, bool>> filter = x => (
                (x.Id == id) && (x.IsAvailable == true)
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _policlinicQueryService.GetDataCountAsync(dBQueryOptions);

            return serviceResponse.ToPolicyResponse(x => x == 0, "Gönderilen Poliklinik Id'sine Ait Aktif Veri Bulunamadı!");
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(HospitalPoliclinic entity)
        {
            DBQueryOptions<HospitalPoliclinic> dBQueryOptions = new DBQueryOptions<HospitalPoliclinic>();
            Expression<Func<HospitalPoliclinic, bool>> filter = x => (
                (x.HospitalId == entity.HospitalId) && (x.PoliclinicId == entity.PoliclinicId) && (x.Id != entity.Id)
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            return serviceResponse.ToPolicyResponse(x => x != 0, "Gönderilen Hastane ve Poliklinik Id'sine Ait Kayıtlı Veri Bulunmaktadır!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(HospitalPoliclinic entity)
        {
            PolicyResponseHelper createPolicy = new PolicyResponseHelper
            {
                ()=>CheckHospitalByIdAsync(entity.HospitalId),
                ()=>CheckPoliclinicByIdAsync(entity.PoliclinicId),
                ()=>CountExistingDataAsync(entity)
            };

            return await createPolicy.ToPolicyResponseAsync();
        }
    }
}