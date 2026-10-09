using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Persistence.Commons.Helpers.Abstracts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Persistence.Commons.Helpers
{
    public abstract class RabbitMQConsumerHelper<TEvent, TEntity, TCommand> : BackgroundService
    {
        protected abstract string QueueName { get; }
        protected abstract string RoutingKey { get; }

        private readonly string _exchangeName;

        private readonly IRabbitMQConnectionHelper _connectionHelper;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<RabbitMQConsumerHelper<TEvent, TEntity, TCommand>> _logger;

        public RabbitMQConsumerHelper(IOptions<RabbitMQOptions> options, IRabbitMQConnectionHelper connectionHelper, IServiceScopeFactory scopeFactory, IMapper mapper, ILogger<RabbitMQConsumerHelper<TEvent, TEntity, TCommand>> logger)
        {
            _connectionHelper = connectionHelper;
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            _logger = logger;

            _exchangeName = options.Value.ExchangeName;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ConsumerAsync(QueueName, RoutingKey, stoppingToken);
        }

        private async Task ConsumerAsync(string queueName, string routingKey, CancellationToken cancellationToken)
        {
            try
            {
                var connection = await _connectionHelper.ConnectAsync();

                if (!connection.IsOpen)
                    throw new Exception("RabbitMQ connection is not open!");

                _logger.LogInformation("RabbitMQ connection completed!");

                using var channel = await connection.CreateChannelAsync();

                if (!channel.IsOpen)
                    throw new Exception("RabbitMQ channel is not open!");

                _logger.LogInformation("RabbitMQ channel completed!");

                await channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Topic, durable: true, autoDelete: false, cancellationToken: cancellationToken);

                await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: cancellationToken);

                await channel.QueueBindAsync(queueName, _exchangeName, routingKey, cancellationToken: cancellationToken);

                AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    try
                    {
                        _logger.LogInformation("Data delivered!");

                        var body = ea.Body.ToArray();
                        var bodyString = Encoding.UTF8.GetString(body);
                        var syncEvent = JsonConvert.DeserializeObject<TEvent>(bodyString);

                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var mediator = scope.ServiceProvider.GetService<IMediator>();
                            TEntity eventEntity = _mapper.Map<TEntity>(syncEvent);
                            TCommand command = _mapper.Map<TCommand>(eventEntity);

                            await mediator.Send(command);

                            _logger.LogInformation("Command process completed!");
                        }

                        await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false, cancellationToken: cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"An error occured while consumer received. Message: {ex.Message} \n Error: {ex}");
                        await channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false, cancellationToken: cancellationToken);
                    }
                };

                await channel.BasicConsumeAsync(
                        queue: queueName,
                        autoAck: false,
                        consumer: consumer,
                        cancellationToken: cancellationToken
                    );

                await Task.Delay(Timeout.Infinite, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}