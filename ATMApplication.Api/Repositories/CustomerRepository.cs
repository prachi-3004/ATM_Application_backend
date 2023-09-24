using ATMApplication.Api.DBContexts;
using ATMApplication.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATMApplication.Api.Repositories
{

	public class CustomerRepository : ICustomerRepository
	{
		
		private readonly ATMContext _context;
		
		public CustomerRepository(ATMContext context)
		{
			_context = context;
		}
		
		
		public async Task<Customer> CreateCustomer(Customer customer)
		{
			try
			{
				await _context.Customers.AddAsync(customer);
				return customer;
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to Insert Customer", ex);
			}
		}
		
		public async Task<Customer> GetCustomerById(int id)
		{
			Customer? customer =  await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
			
			if(customer == null)
			{
				throw new Exception($"Customer with Id: {id} not Found");
			}
			
			return customer;
			
		}
		
		public async Task<Customer> GetCustomerByEmail(string email)
		{
			
			Customer? customer =  await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
			
			if(customer == null)
			{
				throw new Exception($"Customer with Email: {email} not Found");
			}
			
			return customer;
			
		}
		
		
		private bool IsPasswordCorrect(Customer customer, string password)
		{
			return customer.Password == password;
		}
		
		
		public async Task<Customer> ValidateCustomer(string email, string password)
		{
			
			Customer? customer =  await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
			
			if(customer == null || !IsPasswordCorrect(customer, password))
			{
				throw new Exception($"Invalid Email or Password");
			}
			
			return customer;
			
		}
		
		
		
		
	}
	
	
}