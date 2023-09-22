using ATM.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ATM.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly ICustomerService _customerService;
        private readonly IEmployeeService _employeeService;

        public AuthService(IConfiguration config, ICustomerService customerService, IEmployeeService employeeService)
        {
            _config = config;
            _customerService = customerService;
            _employeeService = employeeService;
        }

        public string GenerateJSONWebToken(int id, int role)
        {
            List<Claim> claims = new List<Claim>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            claims.Add(new Claim("username", "{id}"));
            claims.Add(new Claim("role", "{role}"));
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(10),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<int> AuthenticateUser(Login login)
        {
            int user_Id = -1;
            if (login.Role == 0)
            {
                Customer cust = await _customerService.GetCustomerDetail(login);
                if (cust != null)
                {
                    user_Id = cust.Id;
                }
            }
            else if (login.Role == 1)
            {
                Employee emp = _employeeService.GetEmployeeDetail(login);
                if (emp != null)
                {
                    user_Id = emp.Id;
                }
            }

            return user_Id;
        }

    }
}
