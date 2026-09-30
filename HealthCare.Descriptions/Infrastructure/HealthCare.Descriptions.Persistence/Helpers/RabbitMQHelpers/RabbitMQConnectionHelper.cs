using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Persistence.Helpers.RabbitMQHelpers.Abstraction;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Persistence.Helpers.RabbitMQHelpers
{
    public class RabbitMQConnectionHelper : IRabbitMQConnectionHelper
    {
        private IConnection _connection;
        private ConnectionFactory _factory;
        private SemaphoreSlim _semaphore = new(1, 1); // Kaynağa erişebilecek thread sayısı belirtilir.

        public RabbitMQConnectionHelper(IOptions<RabbitMQOptions> options)
        {

            // RabbitMQ bağlantısı ayarlanır. Host,Port,Username ve Password bilgileri tanımlanır.
            _factory = new ConnectionFactory()
            {
                HostName = options.Value.HostName,
                Port = options.Value.Port,
                UserName = options.Value.UserName,
                Password = options.Value.Password
            };
        }

        public async Task<IConnection> ConnectAsync()
        {
            // Eğer oluşturulmuş bir bağlantı varsa ve bu bağlantı açıksa yeni bağlantı oluşturmaz varolan bağlantıyı döner.
            if (_connection != null && (_connection.IsOpen))
                return _connection;

            // Burada ikinci ve daha sonra gelen istekler burada bekler. İlk gelen istek bağlantıyı oluşturur ve işi bittikten sonra Release() ile serbest bırakır.
            await _semaphore.WaitAsync();
            // Daha sonra gelen her istek _connection != null kontrolünden bağlantı olduğunu görür ve yeni bağlantı oluşturmadan varolan bağlantıyı döner.

            try
            {
                // Ek bağlantı kontrolü.
                if (_connection != null && (_connection.IsOpen))
                    return _connection;

                // Thread'e ilk gelen istek bağlantı olmadığı için bağlantıyı oluşturur.
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