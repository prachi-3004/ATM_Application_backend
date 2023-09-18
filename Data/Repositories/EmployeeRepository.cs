using ATM.Models;

namespace ATM.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ATMContext _context;

        public EmployeeRepository( ATMContext context )
        {
            _context = context;
        }

        public Employee GetEmployeeDetail(Login login)
        {
            return _context.Employees.Where(e => e.UserName == login.UserName && e.Password == login.Password).SingleOrDefault();
        }
    }
}
