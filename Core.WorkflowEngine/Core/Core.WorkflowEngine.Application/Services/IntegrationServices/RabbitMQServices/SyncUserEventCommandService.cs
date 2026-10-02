using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.IntegrationServices.RabbitMQServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Services.IntegrationServices.RabbitMQServices
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

            SyncUserEvent existedData = await _repository.GetDataAsync(dBQueryOptions);

            if (existedData == null)
                return InternalServiceResponse<SyncUserEvent>.Failure("Data not found");

            return InternalServiceResponse<SyncUserEvent>.Success(existedData);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(SyncUserEvent entity, CancellationToken cancellationToken)
        {
            Guid id = await _repository.CreateDataAsync(entity);

            return InternalServiceResponse<Guid>.Success(id);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(SyncUserEvent entity)
        {
            DateTimeOffset updatedDate = await _repository.UpdateDataAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(updatedDate);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            InternalServiceResponse<SyncUserEvent> existedData = await GetDataForUpdateAsync(id);

            if (existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure(existedData.ServiceMessage);

            await _repository.DeleteDataAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}