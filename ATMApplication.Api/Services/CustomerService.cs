using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;
using ATMApplication.Api.Repositories;

namespace ATMApplication.Api.Services
{
	public class CustomerService : ICustomerService
	{
		
		private readonly ICustomerRepository _customerRepository;
		
		
		public CustomerService(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}
		
		
		public async Task<Customer> CreateCustomer(Customer customer)
		{
			return await _customerRepository.CreateCustomer(customer);
		}
		
		

		public async Task<Customer> GetCustomerByEmail(string email, TokenClaims tokenClaims)
		{
			
			if(email == tokenClaims.Email || tokenClaims.Role == "ADMIN")
			{
				return await _customerRepository.GetCustomerByEmail(email);
			}
			throw new Exception("Invalid Token");
			
		}

		public async Task<Customer> GetCustomerById(int Id, TokenClaims tokenClaims)
		{
			if(Id.ToString() == tokenClaims.UserId || tokenClaims.Role == "ADMIN")
			{
				return await _customerRepository.GetCustomerById(Id);
			}
			throw new Exception("Invalid Token");
			
		}
	}
}
