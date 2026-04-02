using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Application.Interfaces;
using OrderService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Messaging
{
    public class OutboxProcessor : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventPublisher _publisher;

        public OutboxProcessor(IServiceProvider serviceProvider, IEventPublisher publisher)
        {
            _serviceProvider = serviceProvider;
            _publisher = publisher;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var messages = await context.OutboxMessages
                    .Where(x => !x.Processed)
                    .ToListAsync();

                foreach (var message in messages)
                {
                    var type = Type.GetType($"OrderService.Application.Events.{message.Type}");

                    var @event = JsonSerializer.Deserialize(message.Content, type);

                    await _publisher.PublishAsync(@event);

                    message.Processed = true;
                }

                await context.SaveChangesAsync();

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
