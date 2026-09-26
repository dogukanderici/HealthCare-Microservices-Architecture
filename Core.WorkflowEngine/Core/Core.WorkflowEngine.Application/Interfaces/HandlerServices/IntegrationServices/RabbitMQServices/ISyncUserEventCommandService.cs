using Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ;
using Core.WorkflowEngine.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.IntegrationServices.RabbitMQServices
{
    public interface ISyncUserEventCommandService : IBaseCommandService<SyncUserEvent>
    {
    }
}