using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Persistence.Commons.Helpers.Abstracts
{
    public interface IRabbitMQConnectionHelper
    {
        Task<IConnection> ConnectAsync();
    }
}