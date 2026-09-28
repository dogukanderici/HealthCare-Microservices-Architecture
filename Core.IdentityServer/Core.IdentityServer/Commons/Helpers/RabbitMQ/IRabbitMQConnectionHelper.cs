using RabbitMQ.Client;

namespace Core.IdentityServer.Commons.Helpers.RabbitMQ
{
    public interface IRabbitMQConnectionHelper
    {
        Task<IConnection> ConnectionAsync();
    }
}
