using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.QuotaTypes;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.QuotaTypes
{
    public class QuotaTypeCommandService : IQuotaTypeCommandService
    {
        private readonly IRepository<QuotaType> _repository;

        public QuotaTypeCommandService(IRepository<QuotaType> repository)
        {
            _repository = repository;
        }

        public async Task<InternalServiceResponse<QuotaType>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<QuotaType> dBQueryOptions = new DBQueryOptions<QuotaType>();
            Expression<Func<QuotaType, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            QuotaType existedData = await _repository.GetByIdAsync(dBQueryOptions);

            if (existedData == null)
            {
                return InternalServiceResponse<QuotaType>.Failure();
            }

            return InternalServiceResponse<QuotaType>.Success(existedData);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(QuotaType entity)
        {
            Guid id = await _repository.CreateAsync(entity);

            return InternalServiceResponse<Guid>.Success(id);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(QuotaType entity)
        {
            DateTimeOffset updatedDate = await _repository.UpdateAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(updatedDate);
        }

        public async Task<InternalServiceResponse<bool>> RemoveAsync(Guid id)
        {
            InternalServiceResponse<QuotaType> existedData = await GetDataForUpdateAsync(id);

            if (existedData.Data != null)
            {
                await _repository.DeleteAsync(existedData.Data);

                return InternalServiceResponse<bool>.Success(true);
            }

            return InternalServiceResponse<bool>.Failure();
        }
    }
}