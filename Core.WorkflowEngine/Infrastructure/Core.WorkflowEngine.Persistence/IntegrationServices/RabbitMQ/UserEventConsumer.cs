using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.RabbitMQCommands;
using Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Core.WorkflowEngine.Persistence.IntegrationServices.RabbitMQ
{
    public class UserEventConsumer : BackgroundService
    {
        private readonly string _hostName;
        private readonly int _port;
        private readonly string _exchangeName;
        private readonly string _queueName;

        private IConnection _connection;
        private IChannel _channel;

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<UserEventConsumer> _logger;

        public UserEventConsumer(IOptions<RabbitMQOptions> options, IServiceScopeFactory scopeFactory, IMapper mapper, ILogger<UserEventConsumer> logger)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            _logger = logger;

            _hostName = options.Value.HostName;
            _port = options.Value.Port;
            _exchangeName = options.Value.ExchangeName;
            _queueName = options.Value.QueueName;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consumer çalıştı!");

            // Exchange = Ana Dağıtım Merkezi (Publisher'ın yayınladığı mesajlar ilk olarak Exchange'e gelir.)
            // Queue = Posta kutusu
            // Exchange ve Queue channel'e tanıtılır.
            // Bind işlemi yapılana kadar Exchange ve Queue tanımlıdır ama birbirlerini tanımazlar. Bind ile Exchange ve Queue birbirine bağlanır.

            // Connection için factory nesnesi oluşturulur.
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = _hostName,
                Port = _port,
                UserName = "guest",
                Password = "guest"
            };

            try
            {
                // factory nesnesinden connection oluşturulur.
                _connection = await factory.CreateConnectionAsync(stoppingToken);
                bool checkConnection = _connection.IsOpen;

                _logger.LogInformation($"RabbitMQ Connection Status: {checkConnection}");

                if (!checkConnection)
                    throw new Exception("RabbitMQ connection is not opened!");

                // _connection'dan _channel oluşturulur.
                _channel = await _connection.CreateChannelAsync();
                bool checkChannel = _channel.IsOpen;

                _logger.LogInformation($"RabbitMQ Channel Status: {checkChannel}");

                if (!checkChannel)
                    throw new Exception("RabbitMQ channel is not opened!");

                // Exchange tanımlanır.
                // ExchangeType.Fanout => Ana Dağıtım Merkezine gelen tüm veriler bu merkeze bağlı tüm kuyruklara kopyalanır.
                // durable: true => RabbitMQ kapatıldığında exchange silinmemesini sağlar.
                await _channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Fanout, durable: true, cancellationToken: stoppingToken);

                // Queue tanımlanır.
                await _channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: stoppingToken);

                // Exchange ve Queue birbirlerine tanıtılır.
                // routingKey: "" => ExchangeType.Fanout tanımlandığı için key tanımlaöay gerek yoktur.
                await _channel.QueueBindAsync(_queueName, _exchangeName, routingKey: "", cancellationToken: stoppingToken);

                // _channel üzerinden gelen verileri takip edecek nesne oluşturulur.
                AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(_channel);
                // Veri geldiğinde consumer nesnesinin yapacağı işlemler.
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray(); // Publisher tarafından gelen byte array.
                        var message = Encoding.UTF8.GetString(body); // Byte array'in anlamı string'e dönüştürülür.
                        var syncUserEvent = JsonConvert.DeserializeObject<SyncUserEvent>(message); // Oluşturulan SyncUserEvent sınıfına dönüştürülür.

                        _logger.LogInformation("Published data is taken!");

                        // Gelen verinin db'ye kaydedilmesi için işlem mediator'e devredilir.
                        using (var scoped = _scopeFactory.CreateScope())
                        {
                            var mediator = scoped.ServiceProvider.GetRequiredService<IMediator>();
                            CreateSyncUserEventCommand command = _mapper.Map<CreateSyncUserEventCommand>(syncUserEvent);


                            _logger.LogInformation("Data is send mediator!");

                            await mediator.Send(command);
                        }


                        _logger.LogInformation("Data adding is approved!");

                        // BasicAckAsync çalıştırılıncaya kadar kuyruktan veriler silinmez. Sisteme verinin onaylandığı bilgisi geçilir.
                        await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        // İşlemlerde hata olması durumunda yapılacak işlemler.
                        // _channel'a bu işlemin tamamlanmadığı bilgisi verilir.
                        // requeue: false => Kuyruğun sonuna eklemeden silmesi belirtilir.
                        await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false, cancellationToken: stoppingToken);
                    }
                };

                _logger.LogInformation("Process is completed!");

                // Verileri takip eden nesneye yapacağı iş tanımlanır.
                await _channel.BasicConsumeAsync(
                        queue: _queueName,
                        autoAck: false,
                        consumer: consumer,
                        cancellationToken: stoppingToken
                    );

                // Kapatılma isteği gelip gelmediğini kontrol eder.
                // Kapatılma isteği geldiğinde true olur, döngü kırılır servis kapatma işlemlerini çalıştırır.
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}