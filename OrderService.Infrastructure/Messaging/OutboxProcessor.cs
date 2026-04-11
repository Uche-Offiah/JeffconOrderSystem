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
using Polly;
using Polly.Retry;
using Microsoft.Extensions.Logging;

namespace OrderService.Infrastructure.Messaging
{
    public class OutboxProcessor : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventPublisher _publisher;
        private readonly ILogger<OutboxProcessor> _logger;

        public OutboxProcessor(IServiceProvider serviceProvider, IEventPublisher publisher, ILogger<OutboxProcessor> logger)
        {
            _serviceProvider = serviceProvider;
            _publisher = publisher;
            _logger = logger;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var messages = await context.OutboxMessages
                    .Where(x => !x.Processed)
                    .OrderBy(x => x.OccurredOn)
                    .Take(10)
                    .ToListAsync();

                foreach (var message in messages)
                {
                    var type = Type.GetType($"OrderService.Application.Events.{message.Type}, OrderService.Application");


                    var @event = JsonSerializer.Deserialize(message.Content, type);

                    try
                    {
                        await retryPolicy.ExecuteAsync(async () =>
                        {
                            await _publisher.PublishAsync(@event, type);
                        });

                        message.Processed = true;
                        _logger.LogInformation("Processing outbox message {Id}", message.Id);
                    }
                    catch (Exception ex)
                    {
                        message.RetryCount++;

                        if (message.RetryCount >= 3)
                        {
                            message.Processed = true;
                        }

                    }
                }

                await context.SaveChangesAsync();

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
