using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable> GetAllOrdersAsync()
        //{
        //    return await _context.
        //}

        public async Task SaveOrderWithOutboxAsync(Order order, OutboxMessage message)
        {
            //using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Orders.Add(order);
                _context.OutboxMessages.Add(message);

                await _context.SaveChangesAsync();

               // await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                //await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
