using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Persistence.Commons.Helpers.Abstracts;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Persistence.Commons.Helpers
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

        public async Task<IConnection> ConnectAsync()
        {
            if (_connection != null && _connection.IsOpen)
                return _connection;

            await _semaphore.WaitAsync();

            try
            {
                if (_connection != null && _connection.IsOpen)
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