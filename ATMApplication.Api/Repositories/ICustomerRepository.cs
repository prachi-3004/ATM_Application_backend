using ATMApplication.Api.Models;

namespace ATMApplication.Api.Repositories
{
	
	public interface ICustomerRepository
	{
		
		
		Task<Customer> GetCustomerById(int id);
		
		
		Task<Customer> GetCustomerByEmail(string email);
		
		
		Task<Customer> CreateCustomer(Customer customer);
		
		
		Task<Customer> ValidateCustomer(string email, string password);
		
		
	}
	
	
}