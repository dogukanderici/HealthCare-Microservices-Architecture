using Autofac.Core;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Persistance.MessageBus.Publishers
{
    public class RabbitMQEventPublisher : IRabbitMQEventPublisher
    {
        private readonly ILogger<RabbitMQEventPublisher> _logger;

        private IConnection? _connection;
        private IChannel? _channel;

        public RabbitMQEventPublisher(ILogger<RabbitMQEventPublisher> logger)
        {
            _logger = logger;
        }

        public async Task PublishAsync(RabbitMQEventPublishMessage rabbitMQEventPublishMessage, string exchangeName, string queueName, string routingKey)
        {
            var factory = new ConnectionFactory()
            {
                HostName = RabbitMQEventConstant.Hostname,
                Port = RabbitMQEventConstant.Port,
                UserName = RabbitMQEventConstant.Username,
                Password = RabbitMQEventConstant.Password
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(
                exchange: exchangeName,
                type: ExchangeType.Direct,
                durable: true
                );

            await _channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false
                );

            await _channel.QueueBindAsync(
                queue: queueName,
                exchange: exchangeName,
                routingKey: routingKey
                );

            string message = JsonConvert.SerializeObject(rabbitMQEventPublishMessage.Payload);
            byte[] body = Encoding.UTF8.GetBytes(message);

            await _channel.BasicPublishAsync(
                exchange: exchangeName,
                routingKey: routingKey,
                body: body
                );
        }
    }
}
