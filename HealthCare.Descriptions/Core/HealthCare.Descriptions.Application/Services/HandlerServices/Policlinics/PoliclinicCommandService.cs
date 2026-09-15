using AutoMapper;
using AutoMapper.Execution;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Features.BusinessRules.Policlinics;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Policlinics;
using HealthCare.Descriptions.Domain.Abstracts;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.Policlinics
{
    public class PoliclinicCommandService : IPoliclinicCommandService
    {
        private readonly IRepository<Policlinic> _repository;
        private readonly IPoliclinicCreatePolicy _createPolicy;
        private readonly IPoliclinicUpdatePolicy _updatePolicy;

        public PoliclinicCommandService(IRepository<Policlinic> repository, IPoliclinicCreatePolicy createPolicy, IPoliclinicUpdatePolicy updatePolicy)
        {
            _repository = repository;
            _createPolicy = createPolicy;
            _updatePolicy = updatePolicy;
        }

        public async Task<InternalServiceResponse<Policlinic>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<Policlinic> dBQueryOptions = new DBQueryOptions<Policlinic>();
            Expression<Func<Policlinic, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            Policlinic existedData = await _repository.GetByIdAsync(dBQueryOptions);

            if (existedData == null)
            {
                return InternalServiceResponse<Policlinic>.Failure();
            }

            return InternalServiceResponse<Policlinic>.Success(existedData);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(Policlinic entity)
        {
            InternalPolicyResponse policyResult = await _createPolicy.ExecuteAllRulesAsync(entity);

            if (!policyResult.IsSuccess)
            {
                return InternalServiceResponse<Guid>.Failure(policyResult.BusinessRuleError);
            }

            Guid id = await _repository.CreateAsync(entity);

            return InternalServiceResponse<Guid>.Success(id);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(Policlinic entity)
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
            InternalServiceResponse<Policlinic> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
            {
                return InternalServiceResponse<bool>.Failure();
            }

            await _repository.DeleteAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}
