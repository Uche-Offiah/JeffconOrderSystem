using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Services
{
    public class AuthService : IOrderAuth
    {
        public async Task<string> Login(VerifyUser request)
        {
            // implement the codes here
            return string.Empty;
        }
    }
}
