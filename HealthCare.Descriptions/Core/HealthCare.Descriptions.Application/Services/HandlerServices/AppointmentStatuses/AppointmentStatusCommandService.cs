using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.AppointmentStatuses;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.AppointmentStatutes;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.AppointmentStatuses
{
    public class AppointmentStatusCommandService : IAppointmentStatusCommandService
    {
        private readonly IRepository<AppointmentStatus> _repository;
        private readonly IAppointmentStatusCreatePolicy _createPolicy;
        private readonly IAppointmentStatusUpdatePolicy _updatePolicy;

        public AppointmentStatusCommandService(IRepository<AppointmentStatus> repository, IAppointmentStatusCreatePolicy createPolicy, IAppointmentStatusUpdatePolicy updatePolicy)
        {
            _repository = repository;
            _createPolicy = createPolicy;
            _updatePolicy = updatePolicy;
        }

        public async Task<InternalServiceResponse<AppointmentStatus>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<AppointmentStatus> options = new DBQueryOptions<AppointmentStatus>();

            Expression<Func<AppointmentStatus, bool>> filter = x => x.Id == id;

            options.filter = filter;

            AppointmentStatus existedData = await _repository.GetByIdAsync(options);

            if (existedData == null)
            {
                return InternalServiceResponse<AppointmentStatus>.Failure();
            }

            return InternalServiceResponse<AppointmentStatus>.Success(existedData);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(AppointmentStatus entity)
        {
            InternalPolicyResponse policyResponse = await _createPolicy.ExecuteAllRulesAsync(entity);

            if (!policyResponse.IsSuccess)
            {
                InternalServiceResponse<Guid>.Failure(policyResponse?.BusinessRuleError ?? "");
            }

            Guid createdId = await _repository.CreateAsync(entity);

            return InternalServiceResponse<Guid>.Success(createdId);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(AppointmentStatus entity)
        {
            InternalPolicyResponse policyResponse = await _updatePolicy.ExecuteAllRulesAsync(entity);

            if (!policyResponse.IsSuccess)
            {
                return InternalServiceResponse<DateTimeOffset>.Failure(policyResponse?.BusinessRuleError ?? "");
            }

            DateTimeOffset updatedDate = await _repository.UpdateAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(updatedDate);
        }

        public async Task<InternalServiceResponse<bool>> RemoveAsync(Guid id)
        {
            InternalServiceResponse<AppointmentStatus> existedData = await GetDataForUpdateAsync(id);

            if (existedData.Data == null)
            {
                return InternalServiceResponse<bool>.Success(false);
            }

            await _repository.DeleteAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}