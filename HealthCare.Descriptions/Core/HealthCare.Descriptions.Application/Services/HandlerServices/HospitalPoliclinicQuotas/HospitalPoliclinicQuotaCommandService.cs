using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Features.BusinessRules.HospitalPoliclinicQuotas;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinicQuotas;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.HospitalPoliclinicQuotas
{
    public class HospitalPoliclinicQuotaCommandService : IHospitalPoliclinicQuotaCommandService
    {
        private readonly IRepository<HospitalPoliclinicQuota> _repository;
        private readonly IHospitalPoliclinicQuotaCreatePolicy _createPolicy;
        private readonly IHospitalPoliclinicQuotaUpdatePolicy _updatePolicy;

        public HospitalPoliclinicQuotaCommandService(IRepository<HospitalPoliclinicQuota> repository, IHospitalPoliclinicQuotaCreatePolicy createPolicy, IHospitalPoliclinicQuotaUpdatePolicy updatePolicy)
        {
            _repository = repository;
            _createPolicy = createPolicy;
            _updatePolicy = updatePolicy;
        }

        public async Task<InternalServiceResponse<HospitalPoliclinicQuota>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<HospitalPoliclinicQuota> dBQueryOptions = new DBQueryOptions<HospitalPoliclinicQuota>();
            Expression<Func<HospitalPoliclinicQuota, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            HospitalPoliclinicQuota existedData = await _repository.GetByIdAsync(dBQueryOptions);

            if (existedData == null)
            {
                return InternalServiceResponse<HospitalPoliclinicQuota>.Failure();
            }

            return InternalServiceResponse<HospitalPoliclinicQuota>.Success(existedData);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(HospitalPoliclinicQuota entity)
        {
            InternalPolicyResponse policyResult = await _createPolicy.ExecuteAllRulesAsync(entity);

            if (!policyResult.IsSuccess)
            {
                return InternalServiceResponse<Guid>.Failure(policyResult.BusinessRuleError);
            }

            Guid id = await _repository.CreateAsync(entity);

            return InternalServiceResponse<Guid>.Success(id);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(HospitalPoliclinicQuota entity)
        {
            InternalPolicyResponse policyResult = await _updatePolicy.ExecuteAllRulesAsync(entity);

            if (!policyResult.IsSuccess)
            {
                return InternalServiceResponse<DateTimeOffset>.Failure(policyResult.BusinessRuleError);
            }

            DateTimeOffset updatedDate = await _repository.UpdateAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(updatedDate);
        }

        public async Task<InternalServiceResponse<bool>> RemoveAsync(Guid id)
        {
            InternalServiceResponse<HospitalPoliclinicQuota> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
            {
                return InternalServiceResponse<bool>.Failure();
            }

            await _repository.DeleteAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}