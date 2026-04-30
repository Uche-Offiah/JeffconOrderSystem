using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.DTOs
{
    public class OrderServiceAccount
    {

	}
    public class VerifyUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }

    public class OrderServiceUser
    {
        public OrderServiceUser()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

    }

}
