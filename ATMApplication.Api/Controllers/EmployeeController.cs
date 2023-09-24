using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;
using ATMApplication.Api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ATMApplication.Api.Controllers
{
	
	[ApiController]
	[Route("api/admin")]
	public class EmployeeController: ControllerBase
	{
		
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IMapper _mapper;
		
		public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
		{
			_employeeRepository = employeeRepository;
			_mapper = mapper;
		}
		
		
		[HttpPost]
		public async Task<ActionResult<Employee>> CreateEmployee(CreateEmployeeDto employee)
		{
			Employee emp = _mapper.Map<Employee>(employee);
			var result = await _employeeRepository.CreateEmployee(emp);
			return Ok(result);
		}
		
		[HttpGet]
		public async Task<IActionResult> GetEmployeeByEmail(string email)
		{
			var employee = await _employeeRepository.GetEmployeeByEmail(email);
			return Ok(employee);
		}
		
		
	}
	
	
	
}