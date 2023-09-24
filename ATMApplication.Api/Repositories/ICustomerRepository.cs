using ATMApplication.Api.Models;

namespace ATMApplication.Api.Repositories
{
	
	public interface ICustomerRepository
	{
		
		Task<Customer> GetCustomerByID(int id);
		
		Task<Customer> GetCustomerByEmail(string email);
		
		Task<int> CreateCustomer(Customer customer);
		
		Task<List<Customer>> GetAllCustomers();

        public Task<int> UpdateCustomer(Customer customer);

		public Task<int> DeleteCustomer(int id);
    }

}