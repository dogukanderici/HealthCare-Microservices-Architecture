using Core.IdentityServer.Parameters;
using Core.IdentityServer.Services.RabbitMQ.Events;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Core.IdentityServer.Services.RabbitMQ.MessageBus
{
    public class UserModifiedEventPublisher
    {
        private readonly string _hostname;
        private readonly int _port;
        private readonly string _exchangeName;
        private readonly string _userName;
        private readonly string _password;

        public UserModifiedEventPublisher(IOptions<RabbitMQOptions> options)
        {
            _hostname = options.Value.HostName;
            _port = options.Value.Port;
            _exchangeName = options.Value.ExchangeName;
            _userName = options.Value.UserName;
            _password = options.Value.Password;
        }

        public async Task<bool> PublisherAsync(UserModifiedEvent userModifiedEvent)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _hostname,
                    Port = _port,
                    UserName = _userName,
                    Password = _password
                };

                using var connection = await factory.CreateConnectionAsync();
                bool checkConnection = connection.IsOpen;

                if (!checkConnection)
                {
                    // logger eklenecek.
                    return false;
                }

                using var channel = await connection.CreateChannelAsync();
                bool checkChannel = channel.IsOpen;

                if (!checkChannel)
                {
                    // logger eklenecek.
                    return false;
                }

                await channel.ExchangeDeclareAsync(
                    _exchangeName, // Route adı.
                    ExchangeType.Fanout, // Tüm kuyruklara yayınlar.
                    durable: true // RabbitMQ yeniden başlatılsa bile route'un kaybolmasını engeller.
                    );

                // ExchangeType.Fanout ile Exchange'e abone olmuş tüm kuyruklara bu mesajı zaten yayınladığı için Queue tanımı yapılmasına gerewk yok.
                // Queue tanımı consumer sorumluluğundadır.

                var message = JsonConvert.SerializeObject(userModifiedEvent);
                var body = Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync(
                    exchange: _exchangeName,
                    routingKey: "", // Fanout exchange type kullanıldığından kullanılmaz.
                    body: body // Gönderilecek mesajın byte array hali.
                );

                // logger eklenecek.
                Console.WriteLine($"Published Modified User: {userModifiedEvent}");

                return true;
            }
            catch (Exception ex)
            {
                // logger eklenecek.
                Console.WriteLine($"Error publishing message: {ex.Message}");
                return false;
            }
        }
    }
}
