using Microsoft.EntityFrameworkCore.Metadata;
using OrderService.Application.Interfaces;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Messaging
{
    public class RabbitMqPublisher : IEventPublisher
    {
        private readonly ConnectionFactory _factory;
        public RabbitMqPublisher()
        {
           _factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
        }

        public async Task PublishAsync<T>(T @event)
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            var queueName = typeof(T).Name;

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false);

            var message = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queueName,
                body: body,
                mandatory: true
                );
        }

    }
}
