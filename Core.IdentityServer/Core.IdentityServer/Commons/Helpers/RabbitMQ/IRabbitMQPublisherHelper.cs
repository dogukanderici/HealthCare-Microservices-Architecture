using Core.IdentityServer.Services.RabbitMQ.Events;
using RabbitMQ.Client;
using System.Data.SqlTypes;

namespace Core.IdentityServer.Commons.Helpers.RabbitMQ
{
    public interface IRabbitMQPublisherHelper<T>
        where T : class, IRabbitMQEvent
    {
        Task<bool> PublisherAsync(T publishEvent, string routingKey);
    }
}