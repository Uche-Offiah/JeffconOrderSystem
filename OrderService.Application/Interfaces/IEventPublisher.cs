using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync(object @event, Type type);
    }
}
