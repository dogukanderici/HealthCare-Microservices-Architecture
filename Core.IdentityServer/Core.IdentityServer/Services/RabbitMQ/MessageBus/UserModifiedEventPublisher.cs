using Core.IdentityServer.Services.RabbitMQ.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Core.IdentityServer.Services.RabbitMQ.MessageBus
{
    public class UserModifiedEventPublisher
    {
        private readonly string _hostname = "RabbitMQ";
        private readonly int _port = 5672;
        private readonly string _exchangeName = "user_modified_exchange";
        private readonly string _queueName = "user_modified_queue";

        public async Task<bool> PublisherAsync(UserModifiedEvent userModifiedEvent)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _hostname,
                    Port = _port,
                    UserName = "guest",
                    Password = "guest"
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

                await channel.QueueDeclareAsync(
                    queue: _queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false
                    );

                await channel.QueueBindAsync(
                    queue: _queueName,
                    exchange: _exchangeName,
                    routingKey: ""
                    );

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
