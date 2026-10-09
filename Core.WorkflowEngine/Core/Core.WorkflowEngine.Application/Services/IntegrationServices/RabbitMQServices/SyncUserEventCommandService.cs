using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ.UserEvent;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.IntegrationServices.RabbitMQServices;
using Core.WorkflowEngine.Domain.Entities;
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
        private readonly IRepository<SyncedUser> _repository;
        private readonly IMapper _mapper;

        public SyncUserEventCommandService(IRepository<SyncedUser> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<SyncedUser>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<SyncedUser> dBQueryOptions = new DBQueryOptions<SyncedUser>();
            dBQueryOptions.filter = x => x.Id == id;

            SyncedUser existedData = await _repository.GetDataAsync(dBQueryOptions);

            if (existedData == null)
                return InternalServiceResponse<SyncedUser>.Failure(InternalServiceResponseConstants.DataNotFound);

            return InternalServiceResponse<SyncedUser>.Success(existedData);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(SyncedUser entity, CancellationToken cancellationToken)
        {
            Guid id = await _repository.CreateDataAsync(entity);

            return InternalServiceResponse<Guid>.Success(id);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(SyncedUser entity)
        {
            DateTimeOffset updatedDate = await _repository.UpdateDataAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(updatedDate);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            InternalServiceResponse<SyncedUser> existedData = await GetDataForUpdateAsync(id);

            if (existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure(existedData.ServiceMessage);

            await _repository.DeleteDataAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}