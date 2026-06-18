using HealthCare.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces
{
    public interface IRabbitMQEventPublisher
    {
        Task PublishAsync(RabbitMQEventPublishMessage rabbitMQEventPublishMessage, string exchangeName, string queueName, string routingKey);
    }
}
