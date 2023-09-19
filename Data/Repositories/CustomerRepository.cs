using ATM.Models;
using ATM.Data;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Linq;

namespace ATM.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ATMContext _context;

        public CustomerRepository(ATMContext context)
        {
            _context = context;
        }
        public async Task<Customer> GetCustomer(int id)
        {

            if (_context.Customers == null)
            {
                return null;
            }
            return await _context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Customer> GetCustomerDetail(Login login)
        {
            return await _context.Customers.Where(c => c.UserName == login.UserName && c.Password == login.Password).SingleOrDefaultAsync();
        }
    }
}
