using Microsoft.IdentityModel.Tokens;
using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        //public string GenerateJWTToken(OrderServiceUser user)
        //{
        //    var issuer = _configuration["Jwt:Issuer"];
        //    var audience = _configuration["Jwt:Audience"];
        //    var secret = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        //    var expires = DateTime.Now.AddMinutes(10);

        //    SecurityTokenDescriptor descriptor = new()
        //    {
        //        Issuer = issuer,
        //        Audience = audience,
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //        new("Id", user?.Id),
        //        new("EmailAddress", user?.EmailAddress),
        //        //new(ClaimTypes.Role, user.UserCategory.ToString())
        //        }),
        //        Expires = expires,
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    SecurityToken token = tokenHandler.CreateToken(descriptor);
        //    var jwtToken = tokenHandler.WriteToken(token);

        //    return jwtToken;
        //}
    }
}
