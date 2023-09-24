using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;
using ATMApplication.Api.Repositories;
using ATMApplication.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace ATMApplication.Api.Controllers
{
	
	[ApiController]
	[Route("api/customers")]
	public class CustomerController: ControllerBase
	{
		
		private readonly ICustomerService _customerService;
		
		private readonly ICustomerRepository _customerRepository;
		
		private readonly IAuthenticationService _authenticationService;
		
		private readonly IMapper _mapper;
		
		public CustomerController(ICustomerService customerService, ICustomerRepository customerRepository, IAuthenticationService authenticationService , IMapper mapper)
		{
			_customerService = customerService;
			_customerRepository = customerRepository;
			_authenticationService = authenticationService;
			_mapper = mapper;
		}
		
		
		[Authorize(Roles = "ADMIN")]
		[HttpPost]
		public async Task<ActionResult<Customer>> CreateCustomer(CreateCustomerDto customerDto)
		{
			Customer customer = _mapper.Map<Customer>(customerDto);
			var result = await _customerRepository.CreateCustomer(customer);
			return Ok(result);
		}
		
		
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[Authorize(Roles = "ADMIN,customer")]
		[HttpGet]
		public async Task<IActionResult> GetCustomerByEmail(string email)
		{
			var tokenClaims = _authenticationService.GetTokenClaims();
			return Ok(await _customerService.GetCustomerByEmail(email, tokenClaims));
			// return Ok(await _customerService.GetCustomerByEmail(email, sub));
		}
		
		
		// [Authorize(Roles = "ADMIN,customer")]
		// [HttpGet]
		// public async Task<IActionResult> GetCustomerById(int id)
		// {
		// 	return Ok(await _customerRepository.GetCustomerById(id));
		// }
		
		
	}
	
	
	
}