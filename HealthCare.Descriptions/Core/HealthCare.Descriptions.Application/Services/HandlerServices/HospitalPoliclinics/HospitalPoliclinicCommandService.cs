using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Features.BusinessRules.HospitalPoliclinics;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinics;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.HospitalPoliclinics
{
    public class HospitalPoliclinicCommandService : IHospitalPoliclinicCommandService
    {
        private readonly IRepository<HospitalPoliclinic> _repository;
        private readonly IHospitalPoliclinicCreatePolicy _createPolicy;
        private readonly IHospitalPoliclinicUpdatePolicy _updatePolicy;

        public HospitalPoliclinicCommandService(IRepository<HospitalPoliclinic> repository, IHospitalPoliclinicCreatePolicy createPolicy, IHospitalPoliclinicUpdatePolicy updatePolicy)
        {
            _repository = repository;
            _createPolicy = createPolicy;
            _updatePolicy = updatePolicy;
        }

        public async Task<InternalServiceResponse<HospitalPoliclinic>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<HospitalPoliclinic> dBQueryOptions = new DBQueryOptions<HospitalPoliclinic>();
            Expression<Func<HospitalPoliclinic, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            HospitalPoliclinic existedData = await _repository.GetByIdAsync(dBQueryOptions);

            if (existedData == null)
            {
                return InternalServiceResponse<HospitalPoliclinic>.Failure();
            }

            return InternalServiceResponse<HospitalPoliclinic>.Success(existedData);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(HospitalPoliclinic entity)
        {
            InternalPolicyResponse policyResult = await _createPolicy.ExecuteAllRulesAsync(entity);

            if (!policyResult.IsSuccess)
            {
                return InternalServiceResponse<Guid>.Failure(policyResult.BusinessRuleError);
            }

            Guid id = await _repository.CreateAsync(entity);

            return InternalServiceResponse<Guid>.Success(id);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(HospitalPoliclinic entity)
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
            InternalServiceResponse<HospitalPoliclinic> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
            {
                return InternalServiceResponse<bool>.Failure();
            }

            await _repository.DeleteAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}