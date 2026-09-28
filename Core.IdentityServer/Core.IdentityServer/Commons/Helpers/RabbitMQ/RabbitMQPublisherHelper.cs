using Core.IdentityServer.Commons.Parameters;
using Core.IdentityServer.Services.RabbitMQ.Events;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Core.IdentityServer.Commons.Helpers.RabbitMQ
{
    public class RabbitMQPublisherHelper<TEvent> : IRabbitMQPublisherHelper<TEvent>
        where TEvent : class, IRabbitMQEvent
    {
        public string _exchangeName { get; set; }

        private readonly IRabbitMQConnectionHelper _connectionHelper;

        public RabbitMQPublisherHelper(IOptions<RabbitMQOptions> options, IRabbitMQConnectionHelper connectionHelper)
        {
            _exchangeName = options.Value.ExchangeName;
            _connectionHelper = connectionHelper;
        }

        public async Task<bool> PublisherAsync(TEvent publishEvent, string routingKey)
        {
            try
            {
                var connection = await _connectionHelper.ConnectionAsync();

                using var channel = await connection.CreateChannelAsync();

                await channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Topic, durable: true, autoDelete: false);

                string jsonString = JsonConvert.SerializeObject(publishEvent);
                byte[] messageBody = Encoding.UTF8.GetBytes(jsonString);

                await channel.BasicPublishAsync(
                        exchange: _exchangeName,
                        routingKey: routingKey,
                        body: messageBody
                    );

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}