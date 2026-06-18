using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.UserModifiedEventCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Results.UserModifiedEventResults;
using HealthCare.Descriptions.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using Serilog.Core;
using System.Text;

namespace HealthCare.Descriptions.Persistance.MessageBus.Consumers
{
    public class UserModifiedEventConsumer : IUserModifiedEventConsumer, IDisposable
    {
        private readonly string _hostname = "RabbitMQ";
        private readonly int _port = 5672;
        private readonly string _exchangeName = "user_modified_exchange";
        private readonly string _queueName = "user_modified_queue";

        private readonly ILogger<UserModifiedEventConsumer> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        private IConnection? _connection;
        private IChannel? _channel;
        private bool _disposed = false;

        public UserModifiedEventConsumer(ILogger<UserModifiedEventConsumer> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public async Task ConsumerAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                Port = _port,
                UserName = "guest",
                Password = "guest",
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };

            _logger.LogInformation("RabbitMQ UserModifiedEventConsumer has started!");

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Fanout, durable: true);

            await _channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false);

            await _channel.QueueBindAsync(
                queue: _queueName,
                exchange: _exchangeName,
                routingKey: ""
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

                    GetUserModifiedEventByIdQueryResult userEvent = JsonConvert.DeserializeObject<GetUserModifiedEventByIdQueryResult>(message);

                    if (userEvent == null)
                    {
                        _logger.LogWarning("Received null or invalid user event!");

                        await _channel!.BasicAckAsync(ea.DeliveryTag, multiple: false);

                        return;
                    }

                    _logger.LogInformation($"Recieved Modified User Event : {message}");

                    await mediator.Send(mapper.Map<CreateUserModifiedEventCommand>(userEvent));

                    await _channel!.BasicAckAsync(ea.DeliveryTag, multiple: false);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occured whilie user modified event consume!");

                    try
                    {
                        await _channel!.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false);
                    }
                    catch (Exception ackEx)
                    {
                        _logger.LogError(ackEx, "Failed to Nack Message!");
                    }
                }
            };

            // Consumer başlatılır.
            await _channel.BasicConsumeAsync(
                queue: _queueName,
                autoAck: false, // true olursa mesaj alındığında otomatik silinir
                consumer: consumer
            );

            try
            {
                await Task.Delay(Timeout.Infinite, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                // cancellation token gelirse aşağısı çalışacak.
            }
            finally
            {
                try
                {
                    if (_channel != null && _channel.IsOpen)
                    {
                        await _channel.CloseAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occured while closing channel!");
                }

                try
                {
                    if (_connection != null && _connection.IsOpen)
                    {
                        await _connection.CloseAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occured while closing connection!");
                }
            }


            _logger.LogInformation("RabbitMQ UserModifiedEventConsumer has completed!");
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            try
            {
                _channel?.Dispose();
            }
            catch { }

            try
            {
                _connection?.Dispose();
            }
            catch { }
        }
    }
}
