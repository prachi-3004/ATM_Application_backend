using ATM.Models;
using ATM.Data;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Linq;
using Microsoft.Identity.Client;

namespace ATM.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ATMContext _context;

        public CustomerRepository(ATMContext context)
        {
            _context = context;
        }
        public async Task<Customer> GetCustomerByID(int id)
        {
            var customer = await _context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new Exception("Customer not found!");
            }
            return customer;
        }

        public async Task<Customer> GetCustomerDetail(Login login)
        {
            var customer = await _context.Customers.Where(c => c.UserName == login.UserName && c.Password == login.Password).SingleOrDefaultAsync();
            if (customer == null)
            {
                throw new Exception("Customer not found!");
            }
            return customer;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
    
        public async Task<int> UpdateCustomer(Customer updated_customer)
        {
            var customer = await _context.Customers.Where(c => c.Id == updated_customer.Id).FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new Exception("Customer not found!");
            }
            customer.Name = updated_customer.Name;
            //customer.UserName = updated_customer.UserName;
            customer.Address = updated_customer.Address;
            customer.City = updated_customer.City;
            customer.Email = updated_customer.Email;
            customer.Contact = updated_customer.Contact;
            //customer.Password = updated_customer.Password;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateCredentials(int id, Login login)
        {
            var customer = await _context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new Exception("Customer not found!");
            }
            customer.UserName = login.UserName;
            customer.Password = login.Password;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new Exception("Customer not found!");
            }
            _context.Customers.Remove(customer);
            return await _context.SaveChangesAsync();
        }

    }
}
