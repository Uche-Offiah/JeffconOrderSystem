using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Services
{
    public class OrderServiceHandler : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderServiceHandler(IOrderRepository repository)
        {
          _repository = repository;  
        }
        public async Task<Guid> CreateOrderAsync(decimal amount)
        {
            var order = new Order(amount);
            await _repository.SaveAsync(order);

            return order.Id;
        }
    }
}
