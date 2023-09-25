using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;
using ATMApplication.Api.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ATMApplication.Api.Services
{
	
	public class AuthenticationService : IAuthenticationService
	{
		
		private static HttpContext _httpContext => new HttpContextAccessor().HttpContext;
		
		private readonly IEmployeeRepository _employeeRepository;
		private readonly ICustomerRepository _customerRepository;
        private readonly PasswordHasher<Customer> _passwordHasher;
        private readonly IConfiguration _configuration;
		
		public AuthenticationService(IEmployeeRepository employeeRepository, ICustomerRepository customerRepository, IConfiguration configuration)
		{
			_employeeRepository = employeeRepository;
			_customerRepository = customerRepository;
			_configuration = configuration;
            _passwordHasher = new PasswordHasher<Customer>();
        }
		
		private string GenerateToken(List<Claim> claimsForToken)
		{
			var securityKey = new SymmetricSecurityKey(
				Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
			var signingCredentials = new SigningCredentials(
				securityKey, SecurityAlgorithms.HmacSha256);
			
			
			var jwtSecurityToken = new JwtSecurityToken(
				_configuration["Authentication:Issuer"],
				_configuration["Authentication:Audience"],
				claimsForToken,
				DateTime.UtcNow,
				DateTime.UtcNow.AddHours(1),
				signingCredentials
			);
				
			var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
			
			return tokenToReturn;
		}
		
		public async Task<string> LoginEmployee(string email, string password)
		{
            Employee? employee = await _employeeRepository.GetEmployeeByEmail(email);
            if (employee == null || password != employee.Password)
            {
                throw new Exception($"Invalid Email or Password");
            }
            var claimsForToken = new List<Claim>
			{
				new Claim("userId", employee.Id.ToString()),
				new Claim("userEmail", employee.Email),
				new Claim("name", employee.Name),
				new Claim("userType", "Employee"),
				new Claim("role", employee.Role.ToString())
			};
			
			var token = GenerateToken(claimsForToken);
			
			return token;	
		}
		
		public async Task<string> LoginCustomer(string email, string password)
		{
            Customer? customer = await _customerRepository.GetCustomerByEmail(email);
            if (customer == null || _passwordHasher.VerifyHashedPassword(customer, customer.Password, password) == 0)
            {
                throw new Exception($"Invalid Email or Password");
            }
            var claimsForToken = new List<Claim>
			{
				new Claim("userId", customer.Id.ToString()),
				new Claim("userEmail", customer.Email),
				new Claim("name", customer.Name),
				new Claim("userType", "Customer"),
				new Claim("role", "customer")
			};
			
			var token = GenerateToken(claimsForToken);

			return token;
		}
		
		public TokenClaims GetTokenClaims()
		{
			var claims = _httpContext.User.Claims;
			TokenClaims res = new TokenClaims();
			foreach (Claim claim in claims)  
			{
				if(claim.Type == "userEmail")
				{
					res.Email = claim.Value;
				}
				else if(claim.Type == ClaimTypes.Role)
				{
					res.Role = claim.Value;
				}
				else if(claim.Type == "userId")
				{
					res.UserId = claim.Value;
				}
			}
			
			return res;
		}
		
	}
	
}