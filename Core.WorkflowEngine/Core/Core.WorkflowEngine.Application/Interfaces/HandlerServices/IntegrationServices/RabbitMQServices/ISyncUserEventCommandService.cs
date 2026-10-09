using Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ.UserEvent;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.IntegrationServices.RabbitMQServices
{
    public interface ISyncUserEventCommandService : IBaseCommandService<SyncedUser>
    {
    }
}