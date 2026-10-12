using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ.UserEvent;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.IntegrationServices.RabbitMQServices;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Services.IntegrationServices.RabbitMQServices
{
    public class SyncUserEventQueryService : ISyncUserEventQueryService
    {
        private readonly IRepository<SyncedUser> _repository;

        public SyncUserEventQueryService(IRepository<SyncedUser> repository)
        {
            _repository = repository;
        }

        public async Task<InternalServiceResponse<int>> CheckActiveUserAsync(Guid userId)
        {
            DBQueryOptions<SyncedUser> dBQueryOptions = new DBQueryOptions<SyncedUser>();
            dBQueryOptions.filter = x => (
                (x.Id == userId) &&
                (x.IsAvailable == true)
            );

            int activeUserCount = await _repository.GetAllDataCountAsync(dBQueryOptions);

            return InternalServiceResponse<int>.Success(activeUserCount);
        }
    }
}