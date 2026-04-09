using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(decimal amount);
        //Task<IEnumerable> GetOrdersAsync();
    }
}
