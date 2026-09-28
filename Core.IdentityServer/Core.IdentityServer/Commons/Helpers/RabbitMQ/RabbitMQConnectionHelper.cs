using Core.IdentityServer.Commons.Parameters;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Core.IdentityServer.Commons.Helpers.RabbitMQ
{
    public class RabbitMQConnectionHelper : IRabbitMQConnectionHelper
    {
        private IConnection _connection;
        private ConnectionFactory _factory;
        private SemaphoreSlim _semaphore = new(1, 1);

        public RabbitMQConnectionHelper(IOptions<RabbitMQOptions> options)
        {

            _factory = new ConnectionFactory()
            {
                HostName = options.Value.HostName,
                Port = options.Value.Port,
                UserName = options.Value.UserName,
                Password = options.Value.Password
            };
        }

        public async Task<IConnection> ConnectionAsync()
        {
            if (_connection != null && (_connection.IsOpen))
                return _connection;

            await _semaphore.WaitAsync();

            try
            {
                if (_connection != null && (_connection.IsOpen))
                    return _connection;

                _connection = await _factory.CreateConnectionAsync();

                return _connection;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}