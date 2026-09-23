using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Extensions;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Helpers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinicQuotas;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinics;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.QuotaTypes;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.HospitalPoliclinicQuotas
{
    public class HospitalPoliclinicQuotaCreatePolicy : PolicyRule<HospitalPoliclinicQuota>, IHospitalPoliclinicQuotaCreatePolicy
    {
        private readonly IHospitalPoliclinicQuotaQueryService _queryService;
        private readonly IHospitalPoliclinicQueryService _hpQueryService;
        private readonly IQuotaTypeQueryService _quotaTypeQueryService;

        public HospitalPoliclinicQuotaCreatePolicy(IHospitalPoliclinicQuotaQueryService queryService, IHospitalPoliclinicQueryService hpQueryService, IQuotaTypeQueryService quotaTypeQueryService)
        {
            _queryService = queryService;
            _hpQueryService = hpQueryService;
            _quotaTypeQueryService = quotaTypeQueryService;
        }

        private async Task<InternalPolicyResponse> CheckHospitalPoliclinicByIdAsync(Guid id)
        {
            DBQueryOptions<HospitalPoliclinic> dBQueryOptions = new DBQueryOptions<HospitalPoliclinic>();
            Expression<Func<HospitalPoliclinic, bool>> filter = x => (
                (x.Id == id) && (x.IsAvailable == true)
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _hpQueryService.GetDataCountAsync(dBQueryOptions);

            return serviceResponse.ToPolicyResponse(x => x == 0, "Gönderilen Hastane-Poliklinik Id'sine Ait Aktif Veri Bulunamadı!");
        }

        private async Task<InternalPolicyResponse> CheckQuotaTypeByIdAsync(Guid id)
        {
            DBQueryOptions<QuotaType> dBQueryOptions = new DBQueryOptions<QuotaType>();
            Expression<Func<QuotaType, bool>> filter = x => (
                (x.Id == id) && (x.IsAvailable == true)
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _quotaTypeQueryService.GetDataCountAsync(dBQueryOptions);

            return serviceResponse.ToPolicyResponse(x => x == 0, "Gönderilen Hastane-Poliklinik Id'sine Ait Aktif Veri Bulunamadı!");
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(HospitalPoliclinicQuota entity)
        {
            DBQueryOptions<HospitalPoliclinicQuota> dBQueryOptions = new DBQueryOptions<HospitalPoliclinicQuota>();
            Expression<Func<HospitalPoliclinicQuota, bool>> filter = x => (
                (x.HospitalPoliclinicId == entity.HospitalPoliclinicId) &&
                (x.QuotaTypeId == entity.QuotaTypeId) &&
                (x.Quota == entity.Quota) &&
                (x.ValidityDate == entity.ValidityDate) &&
                (x.Id != entity.Id)
            );
            dBQueryOptions.filter = filter;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCountAsync(dBQueryOptions);

            return serviceResponse.ToPolicyResponse(x => x > 0, "Aynı Kota Tipi-Kota-Geçerlilik Tarihine Ait Birden Fazla Veri Olamaz!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRulesAsync(HospitalPoliclinicQuota entity)
        {
            PolicyResponseHelper createPolicy = new PolicyResponseHelper
            {
                ()=>CheckHospitalPoliclinicByIdAsync(entity.HospitalPoliclinicId),
                ()=>CheckQuotaTypeByIdAsync(entity.QuotaTypeId),
                ()=>CountExistingDataAsync(entity)
            };

            return await createPolicy.ToPolicyResponseAsync();
        }
    }
}