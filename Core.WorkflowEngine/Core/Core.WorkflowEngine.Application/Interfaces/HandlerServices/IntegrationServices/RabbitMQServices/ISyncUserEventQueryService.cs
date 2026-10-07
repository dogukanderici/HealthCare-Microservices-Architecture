using Core.WorkflowEngine.Application.Commons.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.IntegrationServices.RabbitMQServices
{
    public interface ISyncUserEventQueryService
    {
        Task<InternalServiceResponse<int>> CheckActiveUserAsync(Guid userId);
    }
}