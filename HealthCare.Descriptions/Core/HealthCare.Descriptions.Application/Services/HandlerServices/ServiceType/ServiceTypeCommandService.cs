using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Features.BusinessRules.ServiceTypes;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.ServiceType;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.ServiceTypes
{
    public class ServiceTypeCommandService : IServiceTypeCommandService
    {
        private readonly IRepository<ServicingType> _repository;
        private readonly IServiceTypeCreatePolicy _createPolicy;
        private readonly IServiceTypeUpdatePolicy _updatePolicy;

        public ServiceTypeCommandService(IRepository<ServicingType> repository, IServiceTypeCreatePolicy createPolicy, IServiceTypeUpdatePolicy updatePolicy)
        {
            _repository = repository;
            _createPolicy = createPolicy;
            _updatePolicy = updatePolicy;
        }

        public async Task<InternalServiceResponse<ServicingType>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<ServicingType> dBQueryOptions = new DBQueryOptions<ServicingType>();
            Expression<Func<ServicingType, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            ServicingType existedData = await _repository.GetByIdAsync(dBQueryOptions);

            if (existedData == null)
            {
                return InternalServiceResponse<ServicingType>.Failure();
            }

            return InternalServiceResponse<ServicingType>.Success(existedData);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(ServicingType entity)
        {
            InternalPolicyResponse policyResult = await _createPolicy.ExecuteAllRulesAsync(entity);

            if (!policyResult.IsSuccess)
            {
                return InternalServiceResponse<Guid>.Failure(policyResult.BusinessRuleError);
            }

            Guid id = await _repository.CreateAsync(entity);

            return InternalServiceResponse<Guid>.Success(id);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(ServicingType entity)
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
            InternalServiceResponse<ServicingType> existedData = await GetDataForUpdateAsync(id);

            if (existedData.Data != null)
            {
                await _repository.DeleteAsync(existedData.Data);

                return InternalServiceResponse<bool>.Success(true);
            }

            return InternalServiceResponse<bool>.Failure();
        }
    }
}