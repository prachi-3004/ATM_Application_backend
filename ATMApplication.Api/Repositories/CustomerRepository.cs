using ATMApplication.Api.Data;
using ATMApplication.Api.Models;
using ATMApplication.Api.Enums;
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
		
		public async Task<int> CreateCustomer(Customer customer)
		{
			try
			{
				await _context.Customers.AddAsync(customer);
				return await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to add customer", ex);
			}
		}
		
		public async Task<Customer> GetCustomerByID(int id)
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
		
		public async Task<List<Customer>> GetAllCustomers()
		{
			return await _context.Customers.ToListAsync();
		}
		
		public async Task<int> UpdateCustomer(Customer updated_customer)
		{
			try
			{
                var customer = await _context.Customers.Where(c => c.Id == updated_customer.Id).FirstOrDefaultAsync();
                if (customer == null)
                {
                    throw new Exception($"Customer with Email: {updated_customer.Email} not Found");
                }
				customer.GovernmentId = updated_customer.GovernmentId;
				customer.Email = updated_customer.Email;
				customer.ContactNumber = updated_customer.ContactNumber;
				customer.Password = updated_customer.Password;
				customer.Name = updated_customer.Name;
				customer.Address = updated_customer.Address;
				customer.City = updated_customer.City;
				customer.DateOfBirth = updated_customer.DateOfBirth;
				return await _context.SaveChangesAsync();
            }
			catch (Exception ex)
			{
				throw new Exception("Failed to update customer!");
			}
		}

		public async Task<int> DeleteCustomer(int id)
		{
			var customer = await _context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new Exception("Customer not found!");
            }
			else if (customer.Status == (CustomerStatus)2)
			{
				throw new Exception("Customer already deleted!");
			}
			customer.Status = (CustomerStatus)2;
			customer.DeletedAt = DateTime.UtcNow;
			return await _context.SaveChangesAsync();
		}

    }
	
	
}