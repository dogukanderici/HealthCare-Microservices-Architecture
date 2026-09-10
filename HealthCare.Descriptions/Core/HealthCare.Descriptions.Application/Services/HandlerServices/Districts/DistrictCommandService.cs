using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Districts;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.Districts
{
    public class DistrictCommandService : IDistrictCommandService
    {
        private readonly IRepository<District> _repository;
        private readonly IMapper _mapper;

        public DistrictCommandService(IRepository<District> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<District>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<District> options = new DBQueryOptions<District>();
            Expression<Func<District, bool>> filter = x => x.Id == id;
            options.filter = filter;

            District existedData = await _repository.GetByIdAsync(options);

            if (existedData == null)
            {
                return InternalServiceResponse<District>.Failure();
            }

            return InternalServiceResponse<District>.Success(existedData);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(District entity)
        {
            Guid id = await _repository.CreateAsync(entity);

            return InternalServiceResponse<Guid>.Success(id);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(District entity)
        {
            DateTimeOffset updatedDate = await _repository.UpdateAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(updatedDate);
        }

        public async Task<InternalServiceResponse<bool>> RemoveAsync(Guid id)
        {
            InternalServiceResponse<District> existedData = await GetDataForUpdateAsync(id);

            if (existedData.Data != null)
            {
                await _repository.DeleteAsync(existedData.Data);

                return InternalServiceResponse<bool>.Success(true);
            }

            return InternalServiceResponse<bool>.Failure();
        }
    }
}