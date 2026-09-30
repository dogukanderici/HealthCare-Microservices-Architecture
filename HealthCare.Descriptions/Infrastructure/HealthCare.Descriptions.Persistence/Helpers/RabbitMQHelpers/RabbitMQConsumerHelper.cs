using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Persistence.Helpers.RabbitMQHelpers.Abstraction;
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

namespace HealthCare.Descriptions.Persistence.Helpers.RabbitMQHelpers
{
    public abstract class RabbitMQConsumerHelper<TEvent, TCommand> : BackgroundService
    {
        private readonly IRabbitMQConnectionHelper _connectionHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<RabbitMQConsumerHelper<TEvent, TCommand>> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        // IOptions ile appsetting'te tanımlanan değerleri alır.
        private string _exchangeName;
        private string _queueName;

        // Miras alınan sınıflarda override edilerek değerinin tanımlanması zorunlu kılınır.
        protected abstract string QueueName { get; }
        protected abstract string RoutingKey { get; }

        public RabbitMQConsumerHelper(IOptions<RabbitMQOptions> options, IRabbitMQConnectionHelper connectionHelper, IMapper mapper, ILogger<RabbitMQConsumerHelper<TEvent, TCommand>> logger, IServiceScopeFactory scopeFactory)
        {
            _connectionHelper = connectionHelper;
            _mapper = mapper;
            _logger = logger;
            _scopeFactory = scopeFactory;

            _exchangeName = options.Value.ExchangeName;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ConsumerAsync(QueueName, RoutingKey, stoppingToken);
        }

        private async Task ConsumerAsync(string queueName, string routingKey, CancellationToken stoppingToken)
        {
            try
            {
                // _connectionHelper'dan connection alınır.
                var connection = await _connectionHelper.ConnectAsync();

                if (!connection.IsOpen)
                    throw new Exception("RabbitMQ conneciton is not open!");

                _logger.LogInformation("RabbitMQ conneciton is completed!");
                // channel oluşturulur.
                using var channel = await connection.CreateChannelAsync();

                if (!channel.IsOpen)
                    throw new Exception("RabbitMQ channel is  not open!");

                _logger.LogInformation("RabbitMQ channel is created!");

                // Sistem ilk çalıştığında Exchange (Dağıtım Merkezi) ve Queue (Posta Kutusu) yoktur. RabbitMQ'ya gidip oluşturulması söyler.
                // Sonraki işlemelerde oluşturulanı kullanır yeniden oluşturmaz.
                // ExchangeType.Topic => Yayınlanan mesajların etiketle yayınlandığını belirtir.
                await channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Topic, durable: true, autoDelete: false, cancellationToken: stoppingToken);

                // Queue (Posta Kutusu) tanımı yapılır. Exchange ve Queue tanımlı olmasına rağmen birbirlerini tanımazlar.
                await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: stoppingToken);

                // Exchange ve Queue birbirlerinme tanıtılır.
                // routingKey => hangi etiketteki mesajların alınacağını belirtir.
                await channel.QueueBindAsync(queueName, _exchangeName, routingKey, cancellationToken: stoppingToken);

                // Consume işlemlerini yapacak nesne tanımlanır.
                var consumer = new AsyncEventingBasicConsumer(channel);

                // Consume nesnesinin mesaj ulaştığında hangi işlemleri yapacağı tanımlanır.
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    try
                    {
                        _logger.LogInformation("Event Message delivered!");

                        var body = ea.Body.ToArray(); // Byte array olarak gelen mesaj alınır.
                        var bodyString = Encoding.UTF8.GetString(body); // Byte array anlamlı bir string  mesaja ödnüştürülür.
                        var syncEvent = JsonConvert.DeserializeObject<TEvent>(bodyString); // String mesaj event class'ına dönüştürülür.

                        // DB işlemlerinin yapılması için Mediator tanımları yapılır.
                        using (var scopeFactory = _scopeFactory.CreateScope())
                        {
                            var mediator = scopeFactory.ServiceProvider.GetService<IMediator>();
                            var command = _mapper.Map<TCommand>(syncEvent);

                            await mediator.Send(command);
                        }

                        // BasicAckAsync çalışana kadar mesaj silinmez. DB işlemi bittikten sonra mesaj onaylandı olarak kuyruktan silinir.
                        await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);

                        _logger.LogInformation("Event Message completed!");
                    }
                    catch (Exception ex)
                    {
                        // Herhangi bir aksi durumda sisteme onaylanmadığı bildirilir.
                        // requeue => kuyruğun sonuna tekrar eklenmesini engeller.
                        _logger.LogError($"RabbitMQ Consumer Helper an error occured. Message: {ex.Message} \n Error: {ex}");
                        await channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false, cancellationToken: stoppingToken);
                    }
                };

                // Consume nesnesi işlemlere başlatılır.
                await channel.BasicConsumeAsync(
                        queue: queueName,
                        autoAck: false,
                        consumer: consumer,
                        cancellationToken: stoppingToken
                    );

                // Uyku modunda stoppingToken gelene kadar açık bırakılır.
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}