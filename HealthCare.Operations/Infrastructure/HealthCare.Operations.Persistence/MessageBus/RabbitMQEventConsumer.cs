using AutoMapper;
using HealthCare.Operations.Application.Features.RabbitMQMapping;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Configurations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Persistence.MessageBus
{
    public class RabbitMQEventConsumer : IRabbitMQEventConsumer, IDisposable
    {
        private readonly ILogger<RabbitMQEventConsumer> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        private IConnection? _connection;
        private IChannel? _channel;
        private bool _disposed = false;

        public RabbitMQEventConsumer(ILogger<RabbitMQEventConsumer> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public async Task ConsumerAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = RabbitMQEventConstant.Hostname,
                Port = RabbitMQEventConstant.Port,
                UserName = RabbitMQEventConstant.Username,
                Password = RabbitMQEventConstant.Password,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(
                exchange: RabbitMQEventConstant.ExchangeName,
                ExchangeType.Direct,
                durable: true
                );

            await _channel.QueueDeclareAsync(
                queue: RabbitMQEventConstant.QueueName,
                exclusive: false,
                durable: true,
                autoDelete: false
                );

            await _channel.QueueBindAsync(
                exchange: RabbitMQEventConstant.ExchangeName,
                queue: RabbitMQEventConstant.QueueName,
                routingKey: RabbitMQEventConstant.RouteName
                );

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (Model, ea) =>
            {
                using var scope = _scopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    RabbitMQEventCommandMap commandEvent = JsonConvert.DeserializeObject<RabbitMQEventCommandMap>(message);

                    if (commandEvent == null)
                    {
                        _logger.LogWarning("Received null or invalid command event!");

                        await _channel!.BasicAckAsync(ea.DeliveryTag, multiple: false);

                        return;
                    }

                    _logger.LogInformation($"Received command event: {message}");
                }
                catch (Exception ex)
                {

                }
            };
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
