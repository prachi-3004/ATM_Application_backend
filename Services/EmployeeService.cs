using ATM.Data.Repositories;
using ATM.Models;

namespace ATM.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _empRepository;
        public EmployeeService(IEmployeeRepository empRepository)
        {
            _empRepository = empRepository;
        }

        public Employee GetEmployeeDetail(Login login)
        {
            return _empRepository.GetEmployeeDetail(login);
        }
    }
}
