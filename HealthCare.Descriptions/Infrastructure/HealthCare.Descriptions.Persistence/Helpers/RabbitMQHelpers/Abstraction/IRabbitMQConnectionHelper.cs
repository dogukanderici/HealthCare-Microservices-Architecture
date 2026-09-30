using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Persistence.Helpers.RabbitMQHelpers.Abstraction
{
    public interface IRabbitMQConnectionHelper
    {
        Task<IConnection> ConnectAsync();
    }
}