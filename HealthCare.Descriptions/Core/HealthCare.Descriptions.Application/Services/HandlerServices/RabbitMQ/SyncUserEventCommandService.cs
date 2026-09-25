using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.IntegrationServices.RabbitMQ;
using HealthCare.Descriptions.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.RabbitMQ
{
    public class SyncUserEventCommandService : ISyncUserEventCommandService
    {
        private readonly IRepository<SyncUserEvent> _repository;

        public SyncUserEventCommandService(IRepository<SyncUserEvent> repository)
        {
            _repository = repository;
        }

        public async Task<InternalServiceResponse<SyncUserEvent>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<SyncUserEvent> dBQueryOptions = new DBQueryOptions<SyncUserEvent>();
            dBQueryOptions.filter = x => x.Id == id;

            SyncUserEvent result = await _repository.GetByIdAsync(dBQueryOptions);

            if (result == null)
            {
                return InternalServiceResponse<SyncUserEvent>.Failure();
            }

            return InternalServiceResponse<SyncUserEvent>.Success(result);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(SyncUserEvent entity)
        {
            Guid createdId = await _repository.CreateAsync(entity);

            return InternalServiceResponse<Guid>.Success(createdId);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(SyncUserEvent entity)
        {
            DateTimeOffset updatedDate = await _repository.UpdateAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(updatedDate);
        }

        public async Task<InternalServiceResponse<bool>> RemoveAsync(Guid id)
        {
            InternalServiceResponse<SyncUserEvent> existedData = await GetDataForUpdateAsync(id);

            if (existedData.Data == null)
            {
                return InternalServiceResponse<bool>.Success(false);
            }

            await _repository.DeleteAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}