using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.UserEvent.Commands;
using HealthCare.Descriptions.Application.IntegrationServices.RabbitMQ;
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

namespace HealthCare.Descriptions.Persistence.IntegrationServices.RabbitMQ
{
    public class UserEventConsumer : BackgroundService
    {
        private readonly string _hostName;
        private readonly int _port;
        private readonly string _exchangeName; // Paket kargoya verilir fakat alıcıy değil ana dağıtım merkezine gider. Exchange = Ana Dağıtım Merkezi
        private readonly string _queueName; // Alıcı (Consumer) paketi alana kadar kargonun (veri) beklediği posta kutusudur.

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<UserEventConsumer> _logger;

        private IConnection _connection;
        private IChannel _channel;

        public UserEventConsumer(IOptions<RabbitMQOptions> rabbitMQOptions, IServiceScopeFactory scopeFactory, ILogger<UserEventConsumer> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;

            _hostName = rabbitMQOptions.Value.HostName;
            _port = rabbitMQOptions.Value.Port;
            _exchangeName = rabbitMQOptions.Value.ExchangeName;
            _queueName = rabbitMQOptions.Value.QueueName;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Paket kargoya verilir fakat alıcıy değil ana dağıtım merkezine gider. Exchange = Ana Dağıtım Merkezi
            // Exchange.Fanout = Herkese kopyala mantığıdır. Exchange'e gelen yani Ana Dağıtım Merkezine gelen tüm gönderiler bağlı Posta Kutularına (Queue) kopyalanır.
            // IdentityServer'ın gönderceği paketi başka servislerde de kullanılacağında Publisher kodu değiştirilmeden yazılabilir. (Publisher Fanout olarak ayarlı.)

            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = _hostName,
                Port = _port,
                UserName = "guest",
                Password = "guest"
            };

            try
            {
                _connection = await factory.CreateConnectionAsync(stoppingToken); // Connection oluşturulur.
                bool checkConnection = _connection.IsOpen;

                if (!checkConnection)
                    throw new Exception("RabbitMQ connection is not opened!");

                _channel = await _connection.CreateChannelAsync(); // Connection'dan Channel oluşturulur.
                bool checkChannel = _channel.IsOpen;

                if (!checkChannel)
                    throw new Exception("RabbitMQ connection channel is not openede!");

                // Sistem ilk çalıştığında ne Exchange (Dağıtım Merkezi) ne de Queue (Posta Kutusu) vardır. RabbitMQ'ya gidip oluşturulmasını ister. Varsa da olanı kullanır.
                // durable: true => RabbitMQ sunucusu kapatılsa bile exchange ve queue silme anlamına gelir.
                // Exchange yoksa oluştur, varsa olanı kullan için metot kullanılır.
                await _channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Fanout, durable: true, cancellationToken: stoppingToken);

                // Queue yoksa oluştur, varsa olanı kullan için metot kullanılır.
                await _channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: stoppingToken);

                // Bind metotu öncesi Exchange ve Queue vardır ama birbirlerini tanımazlar.
                // Queue (Posta Kutusu) Exchange'e (Dağıtım Merkezi) abone olmak için kullanılır.
                await _channel.QueueBindAsync(_queueName, _exchangeName, routingKey: "", cancellationToken: stoppingToken);

                // _channel üzerinden gelen paketleri (veri) takip etmek için tanımlanır.
                AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(_channel);

                // Paket (veri) geldiğinde yapılacak işlemler.
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray(); // Gelen veri (byte array) açılır.
                        var message = Encoding.UTF8.GetString(body); // Byte array okunabilir metne çevrilir.
                        var syncUserEvent = JsonConvert.DeserializeObject<SyncUserEvent>(message); // UserEvent sınıfına dönüştürülür.

                        // Gelen verinin CQRS tarafında işlenip yazılması için MediatR'a gönderilir.
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                            var command = new CreateSyncUserEventCommand
                            {
                                Id = syncUserEvent.Id,
                                Name = syncUserEvent.Name,
                                Surname = syncUserEvent.Surname,
                                UserName = syncUserEvent.UserName,
                                EMail = syncUserEvent.EMail
                            };

                            await mediator.Send(command, stoppingToken);
                        }

                        // BasicAckAsync metotu çalıştırılıncaya kadar kuyruktan veri silinmez. Kayıt işlemi bittikten sonra kuyruktan siler.
                        await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        // Mesaj işlenirken bir hata alınması durumunda BasicNackAsync ile onaylanmadığı RabbitMQ'ya bildirilir.
                        // requeue: false => Hatalı mesajı sil demektir. true yapıldığında kuyruğun en sonuna tekrar ekler.
                        // TODO - Tamamen silmek yerine Dead Letter Queue yönlendirmesi yapılabilir.
                        await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false, cancellationToken: stoppingToken);
                    }
                };

                // Verileri takip ederken ne yapması gerektiği tanımlarından sonra consumer için artık bunlarla çalış komutu verilir.
                await _channel.BasicConsumeAsync(
                        queue: _queueName,
                        autoAck: false, // Veri kuyruğa gönderildiği gibi silinmesi engeller.
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
                throw;
            }
        }
    }
}