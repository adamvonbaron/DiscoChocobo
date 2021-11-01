using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

using RabbitMQ.Client;


namespace DiscoChocobo
{
    public class RabbitMQService : IHostedService, IDisposable
    {
        private ConnectionFactory connectionFactory { get; set; }
        private IConnection rabbitMQConnection { get; set; }
        private IModel rabbitMQChannel { get; set; }
        private RabbitMQConfig rabbitMQConfig { get; set; }
        public RabbitMQService(IConfiguration configuration)
        {
            rabbitMQConfig = configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>();

            // todo: possible to allow DI container to provide connectionFactory?
            connectionFactory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig.AMPQHostname
            };
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // todo: probably could add both these as singletons and inject where need be via services.AddSingleton<T>().....
            rabbitMQConnection = connectionFactory.CreateConnection();
            rabbitMQChannel = rabbitMQConnection.CreateModel();

            await Task.CompletedTask;
        }
        

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            rabbitMQChannel.Close();
            rabbitMQConnection.Close();

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            return;
        }
    }
}
