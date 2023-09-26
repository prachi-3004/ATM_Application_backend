using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;
using ATMApplication.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATMApplication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAuthenticationService _authenticationService;

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
        public async Task<ActionResult<Account>> ChangePin(int id, [FromBody] PinChangeDto pinChangeDto)
        {
            try
            {
                var result = await _accountService.ChangePin(id, pinChangeDto.NewPin, pinChangeDto.OldPin);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Route("DisableAccount")]
        [Authorize(Roles = "ADMIN")]
        [HttpPatch]
        public async Task<ActionResult<int>> DisableAccount([FromBody] int id)
        {
            try
            {
                var result = await _accountService.DisableAccount(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
