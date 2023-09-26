using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;

namespace ATMApplication.Api.Services
{
	public interface ICustomerService
	{
		
		public Task<int> CreateCustomer(CustomerDto customerDto);
		
		public Task<Customer> GetCustomerByID(int id, TokenClaims tokenClaims);
		
		public Task<Customer> GetCustomerByEmail(string email, TokenClaims tokenClaims);
		
		public Task<List<Customer>> GetAllCustomers();

		public Task<int> UpdateCustomer(int id, CustomerDto customerDto, TokenClaims tokenClaims);

		public Task<int> DeleteCustomer(string email);

    }
}
