using ATM.Models;

namespace ATM.Services
{
    public interface IEmployeeService
    {
        public Employee GetEmployeeDetail(Login login);
    }
}
