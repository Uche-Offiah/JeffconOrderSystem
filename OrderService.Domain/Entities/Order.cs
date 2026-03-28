using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreationDate { get; set; }

        public Order(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");
            Amount = amount;
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

    }
}
