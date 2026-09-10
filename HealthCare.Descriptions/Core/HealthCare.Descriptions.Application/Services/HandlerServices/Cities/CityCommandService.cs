using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Cities;
using HealthCare.Descriptions.Domain.Abstracts;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.Cities
{
    public class CityCommandService : ICityCommandService
    {
        private readonly IRepository<City> _repository;

        public CityCommandService(IRepository<City> repository)
        {
            _repository = repository;
        }

        public async Task<InternalServiceResponse<City>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<City> options = new DBQueryOptions<City>();
            Expression<Func<City, bool>> filter = x => x.Id == id;
            options.filter = filter;

            City existedData = await _repository.GetByIdAsync(options);

            if (existedData != null)
            {
                return InternalServiceResponse<City>.Success(existedData);
            }

            return InternalServiceResponse<City>.Failure();
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(City entity)
        {

            Guid createdId = await _repository.CreateAsync(entity);

            return InternalServiceResponse<Guid>.Success(createdId);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(City entity)
        {
            DateTimeOffset updatedDate = await _repository.UpdateAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(updatedDate);
        }

        public async Task<InternalServiceResponse<bool>> RemoveAsync(Guid id)
        {
            InternalServiceResponse<City> existedData = await GetDataForUpdateAsync(id);

            if (existedData.IsSuccess)
            {
                await _repository.DeleteAsync(existedData.Data);

                return InternalServiceResponse<bool>.Success(true);
            }

            return InternalServiceResponse<bool>.Failure();
        }
    }
}