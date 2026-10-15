using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicPolicies;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.DynamicPolicies
{
    public class DynamicPolicyCommandService : IDynamicPolicyCommandService
    {
        private readonly IRepository<DynamicPolicy> _repository;

        public DynamicPolicyCommandService(IRepository<DynamicPolicy> repository)
        {
            _repository = repository;
        }

        public async Task<InternalServiceResponse<DynamicPolicy>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<DynamicPolicy> options = new DBQueryOptions<DynamicPolicy>();
            options.filter = x => x.Id == id;

            DynamicPolicy repoResponse = await _repository.GetByIdAsync(options);

            if (repoResponse == null)
                return InternalServiceResponse<DynamicPolicy>.Failure("Data not found!");

            return InternalServiceResponse<DynamicPolicy>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(DynamicPolicy entity)
        {
            Guid repoResponse = await _repository.CreateAsync(entity);

            return InternalServiceResponse<Guid>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(DynamicPolicy entity)
        {
            DateTimeOffset repoResponse = await _repository.UpdateAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<bool>> RemoveAsync(Guid id)
        {
            InternalServiceResponse<DynamicPolicy> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure(existedData.ServiceMessage);

            await _repository.DeleteAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}