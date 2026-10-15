using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicRoles;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.DynamicRoles
{
    public class DynamicRoleCommandService : IDynamicRoleCommandService
    {
        private readonly IRepository<DynamicRole> _repository;

        public DynamicRoleCommandService(IRepository<DynamicRole> repository)
        {
            _repository = repository;
        }

        public async Task<InternalServiceResponse<DynamicRole>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<DynamicRole> dBQueryOptions = new DBQueryOptions<DynamicRole>();
            dBQueryOptions.filter = x => x.Id == id;

            DynamicRole repoResponse = await _repository.GetByIdAsync(dBQueryOptions);

            if (repoResponse == null)
                return InternalServiceResponse<DynamicRole>.Failure("Data not found!");

            return InternalServiceResponse<DynamicRole>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(DynamicRole entity)
        {
            Guid repoResponse = await _repository.CreateAsync(entity);

            return InternalServiceResponse<Guid>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(DynamicRole entity)
        {
            DateTimeOffset repoResponse = await _repository.UpdateAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<bool>> RemoveAsync(Guid id)
        {
            InternalServiceResponse<DynamicRole> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure(existedData.ServiceMessage);

            await _repository.DeleteAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}