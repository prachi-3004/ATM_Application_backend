using ATM.Models;

namespace ATM.Data.Repositories
{
    public interface IEmployeeRepository
    {
        public Employee GetEmployeeDetail(Login login);
    }
}
