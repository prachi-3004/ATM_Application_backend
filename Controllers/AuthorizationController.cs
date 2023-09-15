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
        private readonly ICustomerService _customerService;

        public AuthorizationController(IConfiguration config, ICustomerService customerService)
        {
            _config = config;
            _customerService = customerService;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Login login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user, login.Role);

                response = Ok(new LoginResponse { token = tokenString, User_Id = user, Role=0 });
            }

            return response;
        }

        private string GenerateJSONWebToken(string username, string role)
        {
            List<Claim> claims = new List<Claim>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            claims.Add(new Claim("username", username));
            claims.Add(new Claim("role", role));
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(10),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string AuthenticateUser(Login login)
        {
            string username = "Unauthorized";
            if (login.Role == "Customer")
            {
                Customer cust = _customerService.GetCustomerDetail(login);
                if (cust != null)
                {
                    username = cust.UserName;
                }
            }
            //else if (login.Role == "Employee")
            //{
            //    Employee emp = 
            //}

            return username;
        }

    }
}
