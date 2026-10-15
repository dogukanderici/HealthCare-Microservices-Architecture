using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.IntegrationServices.RabbitMQ.SyncedRoles;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.IntegrationServices.RabbitMQ.SyncedRoles
{
    public class SyncedRoleCommandService : ISyncedRoleCommandService
    {
        private readonly IRepository<SyncedRole> _repository;

        public SyncedRoleCommandService(IRepository<SyncedRole> repository)
        {
            _repository = repository;
        }

        public async Task<InternalServiceResponse<SyncedRole>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<SyncedRole> options = new DBQueryOptions<SyncedRole>();
            options.filter = x => x.Id == id;

            SyncedRole repoResponse = await _repository.GetByIdAsync(options);

            if (repoResponse == null)
                return InternalServiceResponse<SyncedRole>.Failure("Data not found!");

            return InternalServiceResponse<SyncedRole>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(SyncedRole entity)
        {
            Guid repoResponse = await _repository.CreateAsync(entity);

            return InternalServiceResponse<Guid>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(SyncedRole entity)
        {
            DateTimeOffset repoResponse = await _repository.UpdateAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<bool>> RemoveAsync(Guid id)
        {
            InternalServiceResponse<SyncedRole> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure(existedData.ServiceMessage);

            await _repository.DeleteAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}