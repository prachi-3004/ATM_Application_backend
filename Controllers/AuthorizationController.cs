using ATM_banking_system.Data;
using ATM_banking_system.Models;
using ATM_banking_system.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ATM_banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly CustomerService _customerService;

        public AuthorizationController(IConfiguration config, CustomerService customerService)
        {
            _config = config;
            _customerService = customerService;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(CustomerLogin login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);

                response = Ok(new LoginResponse { token = tokenString, User_Id = login.UserName, Role=0 });
            }

            return response;
        }

        private string GenerateJSONWebToken(Customer userInfo)
        {

            if (userInfo is null)
            {
                throw new ArgumentNullException(nameof(userInfo));
            }
            List<Claim> claims = new List<Claim>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            claims.Add(new Claim("UserName", userInfo.UserName));
            claims.Add(new Claim("role", "Customer"));
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(2),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Customer AuthenticateUser(CustomerLogin login)
        {
            Customer cust = _customerService.GetCustomerDetail(login);
            return cust;
        }

    }
}
