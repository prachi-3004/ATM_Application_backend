using ATMApplication.Api.Models;

namespace ATMApplication.Api.Repositories
{
	
	public interface IEmployeeRepository
	{
		
		Task<Employee> GetEmployeeById(int id);
		
		Task<Employee> GetEmployeeByEmail(string email);
		
		Task<Employee> CreateEmployee(Employee employee);
		
	}
	
}