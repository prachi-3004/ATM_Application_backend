using ATMApplication.Api.Data;
using ATMApplication.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATMApplication.Api.Repositories
{

	public class EmployeeRepository : IEmployeeRepository
	{
		
		private readonly ATMContext _context;
		
		public EmployeeRepository(ATMContext context)
		{
			_context = context;
		}
		
		
		public async Task<Employee> CreateEmployee(Employee employee)
		{
			try
			{
				_context.Employees.Add(employee);
				await _context.SaveChangesAsync();
				return employee;
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to Insert Employee", ex);
			}
		}
		
		public async Task<Employee> GetEmployeeById(int id)
		{
			Employee? employee =  await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
			
			if(employee == null)
			{
				throw new Exception($"Employee with Id: {id} not Found");
			}
			
			return employee;
		}
		
		public async Task<Employee> GetEmployeeByEmail(string email)
		{
			Employee? employee =  await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
			
			if(employee == null)
			{
				throw new Exception($"Employee with Email: {email} not Found");
			}
			
			return employee;
		}
		
		//private bool IsPasswordCorrect(Employee employee, string password)
		//{
		//	return employee.Password == password;
		//}
		
		
		//public async Task<Employee> ValidateEmployee(string email, string password)
		//{
			
		//	Employee? employee =  await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
			
		//	if(employee == null || !IsPasswordCorrect(employee, password))
		//	{
		//		throw new Exception($"Invalid Email or Password");
		//	}
			
		//	return employee;
			
		//}
		
		
	}
	
	
}