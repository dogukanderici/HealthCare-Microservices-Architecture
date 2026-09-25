using HealthCare.Descriptions.Application.IntegrationServices.RabbitMQ;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.RabbitMQ
{
    public interface ISyncUserEventCommandService : ICommandBaseService<SyncUserEvent>
    {
    }
}
