using Microsoft.Extensions.Logging;
using OrderService.Application.Events;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderService.Application.Services
{
    public class OrderServiceHandler : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger<OrderServiceHandler> _logger;
        private readonly IEventPublisher _eventPublisher;

        public OrderServiceHandler(IOrderRepository repository, ILogger<OrderServiceHandler> logger, IEventPublisher eventPublisher)
        {
          _repository = repository;  
          _logger = logger;
          _eventPublisher = eventPublisher;
        }
        public async Task<Guid> CreateOrderAsync(decimal amount)
        {
            var order = new Order(amount);
            await _repository.SaveAsync(order);

            var evt = new OrderCreatedEvent
            {
                OrderId = order.Id,
                Amount = order.Amount,
            };

            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = nameof(OrderCreatedEvent),
                Content = JsonSerializer.Serialize(evt),
                OccurredOn = DateTime.UtcNow,
                Processed = false
            };

            //await _eventPublisher.PublishAsync(evt);
            //await _repository.SaveOutboxAsync(outboxMessage);
            await _repository.SaveOrderWithOutboxAsync(order, outboxMessage);

            _logger.LogInformation("Created order with amount {amount}", amount);
            _logger.LogInformation("Ordercreated event published for {OrderId}", order.Id);

            return order.Id;
        }
    }
}
