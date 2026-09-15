using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Features.BusinessRules.Hospitals;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Hospitals;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.Hospitals
{
    public class HospitalCommandService : IHospitalCommandService
    {
        private readonly IRepository<Hospital> _repository;
        private readonly IHospitalCreatePolicy _createPolicy;
        private readonly IHospitalUpdatePolicy _updatePolicy;

        public HospitalCommandService(IRepository<Hospital> repository, IHospitalCreatePolicy createPolicy, IHospitalUpdatePolicy updatePolicy)
        {
            _repository = repository;
            _createPolicy = createPolicy;
            _updatePolicy = updatePolicy;
        }

        public async Task<InternalServiceResponse<Hospital>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<Hospital> dBQueryOptions = new DBQueryOptions<Hospital>();
            Expression<Func<Hospital, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            Hospital existedData = await _repository.GetByIdAsync(dBQueryOptions);

            if (existedData == null)
            {
                return InternalServiceResponse<Hospital>.Failure();
            }

            return InternalServiceResponse<Hospital>.Success(existedData);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(Hospital entity)
        {
            InternalPolicyResponse policyResult = await _createPolicy.ExecuteAllRulesAsync(entity);

            if (!policyResult.IsSuccess)
            {
                return InternalServiceResponse<Guid>.Failure(policyResult.BusinessRuleError);
            }

            Guid id = await _repository.CreateAsync(entity);

            return InternalServiceResponse<Guid>.Success(id);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(Hospital entity)
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
            InternalServiceResponse<Hospital> existedData = await GetDataForUpdateAsync(id);

            if (existedData.Data != null)
            {
                await _repository.DeleteAsync(existedData.Data);

                return InternalServiceResponse<bool>.Success(true);
            }

            return InternalServiceResponse<bool>.Failure();
        }
    }
}