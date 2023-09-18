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
        public Customer GetCustomer(int id)
        {

            if (_context.Customers == null)
            {
                return null;
            }
            var customer = _context.Customers.Where(c => c.Id == id).FirstOrDefault();

            if (customer == null)
            {
                return null;
            }

            return customer;
        }

        public Customer GetCustomerDetail(Login login)
        {
            return _context.Customers.Where(c => c.UserName == login.UserName && c.Password == login.Password).SingleOrDefault();
        }
    }
}
