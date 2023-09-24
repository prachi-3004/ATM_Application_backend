using System.Security.Claims;
using ATMApplication.Api.Dto;

namespace ATMApplication.Api.Services
{
	
	
	public interface IAuthenticationService
	{
		
		public Task<string> LoginEmployee(string email, string password);
		
		public Task<string> LoginCustomer(string email, string password);
		
		public TokenClaims GetTokenClaims();
		
	}
	
	
	
}