using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task SaveAsync(Order order);
        Task SaveOutboxAsync(OutboxMessage message);
    }
}
