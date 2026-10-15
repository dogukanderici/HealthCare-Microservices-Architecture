using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.IntegrationServices.RabbitMQ;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.RabbitMQ
{
    public class SyncUserEventCommandService : ISyncUserEventCommandService
    {
        private readonly IRepository<SyncedUser> _repository;

        public SyncUserEventCommandService(IRepository<SyncedUser> repository)
        {
            _repository = repository;
        }

        public async Task<InternalServiceResponse<SyncedUser>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<SyncedUser> dBQueryOptions = new DBQueryOptions<SyncedUser>();
            dBQueryOptions.filter = x => x.Id == id;

            SyncedUser result = await _repository.GetByIdAsync(dBQueryOptions);

            if (result == null)
            {
                return InternalServiceResponse<SyncedUser>.Failure();
            }

            return InternalServiceResponse<SyncedUser>.Success(result);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(SyncedUser entity)
        {
            Guid createdId = await _repository.CreateAsync(entity);

            return InternalServiceResponse<Guid>.Success(createdId);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(SyncedUser entity)
        {
            DateTimeOffset updatedDate = await _repository.UpdateAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(updatedDate);
        }

        public async Task<InternalServiceResponse<bool>> RemoveAsync(Guid id)
        {
            InternalServiceResponse<SyncedUser> existedData = await GetDataForUpdateAsync(id);

            if (existedData.Data == null)
            {
                return InternalServiceResponse<bool>.Success(false);
            }

            await _repository.DeleteAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}