using ATM_banking_system.Models;
using ATM_banking_system.Data;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Linq;

namespace ATM_banking_system.Data.Repositories
{
    public class ATMDB_Repository : IATMDB_Repository
    {
        private readonly ATMContext _context;
        
        public ATMDB_Repository(ATMContext context)
        {
            _context = context;
        }
        public Customer GetCustomer(int id)
        {

            if (_context.Customers == null)
            {
                return null;
            }
            var customer = _context.Customers.Where(c => c.UserId == id).FirstOrDefault();

            if (customer == null)
            {
                return null;
            }

            return customer;
        }

        public Customer GetCustomerDetail(CustomerLogin login)
        {
            return _context.Customers.Where(c => c.UserName == login.UserName && c.Password == login.Password).SingleOrDefault();
        }
    }
}
