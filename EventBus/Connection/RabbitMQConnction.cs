﻿using RabbitMQ.Client;

namespace XmpManager.EventBus.Connection
{
    public class RabbitMQConnction : IRabbitMQConnection
    {
        private readonly IConnectionFactory factory;

        public RabbitMQConnction(string host)
        {
            this.factory = new ConnectionFactory() {  HostName= host };
        }

        public IConnection TryConnect()
        {
            const int retryCount = 5;
            var retries = 0;
            while (true)
            {
                try
                {
                    return factory.CreateConnection();
                }
                catch (Exception)
                {
                    retries++;
                    if (retries == retryCount) throw;
                    Thread.Sleep((int)Math.Pow(retries, 2) * (500 + new Random().Next(500)));
                }
            }
        }
    }
}
