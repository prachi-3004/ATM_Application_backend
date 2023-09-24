using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;

namespace ATMApplication.Api.Services
{
	public interface ICustomerService
	{
		
		
		public Task<Customer> CreateCustomer(Customer customer);
		
		
		public Task<Customer> GetCustomerById(int Id, TokenClaims tokenClaims);
		
		
		public Task<Customer> GetCustomerByEmail(string email, TokenClaims tokenClaims);
		
		
	}
}
