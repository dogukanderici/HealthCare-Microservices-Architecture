using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces.HandlerServices.IntegrationServices.RabbitMQ.SyncedRoles
{
    public interface ISyncedRoleCommandService : ICommandBaseService<SyncedRole>
    {
    }
}
