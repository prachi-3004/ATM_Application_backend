using System.Security.Claims;
using ATMApplication.Api.Dto;
using ATMApplication.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ATMApplication.Api.Controllers
{
	
	
	[ApiController]
	[Route("api/[controller]")]
	public class AuthenticationController : ControllerBase
	{
		
		private readonly IAuthenticationService _authenticationService;
		
		public AuthenticationController(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
		}
		
		
		[HttpPost("admin/login")]
		public async Task<ActionResult<string>> LoginEmployee(LoginRequestBodyDto loginRequestBody)
		{
			string token = await _authenticationService.LoginEmployee(loginRequestBody.Email, loginRequestBody.Password);
			return Ok(token);
		}
		
		
		[HttpPost("customers/login")]
		public async Task<ActionResult<string>> LoginCustomer(LoginRequestBodyDto loginRequestBody)
		{
			string token = await _authenticationService.LoginCustomer(loginRequestBody.Email, loginRequestBody.Password);
			return Ok(token);
		}
		
		
	}
	
	
}