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
	[Route("api/[controller]")]
	public class CustomerController: ControllerBase
	{
		
		private readonly ICustomerService _customerService;
		
		private readonly IAuthenticationService _authenticationService;
		
		public CustomerController(ICustomerService customerService, IAuthenticationService authenticationService)
		{
			_customerService = customerService;
			_authenticationService = authenticationService;
		}
		
		[Authorize(Roles = "ADMIN")]
		[Route("Add")]
		[HttpPost]
		public async Task<ActionResult<int>> CreateCustomer(CustomerDto customerDto)
		{
			try
			{
				var customer = await _customerService.CreateCustomer(customerDto);
				return Ok(customer);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[Authorize(Roles = "ADMIN")]
        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomers();
                if (customers.Count == 0)
                {
                    return NoContent();
                }
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

		[Authorize(Roles = "ADMIN,customer")]
		[Route("GetByEmail/{email}")]
		[HttpGet]
		public async Task<IActionResult> GetCustomerByEmail(string email)
		{
			try
			{
                var tokenClaims = _authenticationService.GetTokenClaims();
                return Ok(await _customerService.GetCustomerByEmail(email, tokenClaims));
            }
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

        [Authorize(Roles = "ADMIN,customer")]
        [Route("GetByID/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCustomerByID(int id)
        {
            try
            {
                var tokenClaims = _authenticationService.GetTokenClaims();
                return Ok(await _customerService.GetCustomerByID(id, tokenClaims));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN,customer")]
        [Route("UpdateDetails/{email}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(string email, CustomerDto customerDto)
        {
            try
            {
                var tokenClaims = _authenticationService.GetTokenClaims();
                var result = await _customerService.UpdateCustomer(email, customerDto, tokenClaims);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [Route("Delete/{email}")]
        [HttpPut]
        public async Task<IActionResult> DeleteCustomer(string email)
        {
            try
            {
                var result = await _customerService.DeleteCustomer(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
	
	
	
}