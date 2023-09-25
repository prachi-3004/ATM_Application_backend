using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;
using ATMApplication.Api.Repositories;
using ATMApplication.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATMApplication.Api.Controllers
{
	
	[Route("api/[controller]")]
	[ApiController]
	class AccountController : ControllerBase
	{
		
		private readonly IAccountService _accountService;
        private readonly IAuthenticationService _authenticationService;
		private readonly IMapper _mapper;
		
		public AccountController(IAccountService accountService, IAuthenticationService authenticationService)
		{
            _accountService = accountService;
            _authenticationService = authenticationService;
		}

        [Route("AddAccount")]
		[Authorize(Roles = "ADMIN")]
		[HttpPost]
		public async Task<ActionResult<Account>> AddAccount(AccountDto accountDto)
		{
            try
            {
                var result = await _accountService.AddAccount(accountDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("GetAll")]
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAllAccounts()
        {
            try
            {
                var accounts = await _accountService.GetAllAccounts();
                if (accounts.Count == 0)
                {
                    return NoContent();
                }
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("GetAccountByID/{id}")]
        [Authorize(Roles = "ADMIN,customer")]
        [HttpGet]
        public async Task<ActionResult<Account>> GetAccountByID(int id)
        {
            try
            {
                var tokenClaims = _authenticationService.GetTokenClaims();
                var account = await _accountService.GetAccountByID(id, tokenClaims);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("GetAccountsByCustomer/{id}")]
        [Authorize(Roles = "ADMIN,customer")]
        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAccountsByCustomer(int id)
        {
            try
            {
                var tokenClaims = _authenticationService.GetTokenClaims();
                var accounts = await _accountService.GetAccountsByCustomerID(id, tokenClaims);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("ChangePin/{id}")]
        [Authorize(Roles = "customer")]
        [HttpPut]
        public async Task<ActionResult<Account>> ChangePin(int id, [FromBody] string newPin, [FromBody] string oldPin)
        {
            try
            {
                var result = await _accountService.ChangePin(id, newPin, oldPin);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
	
}