using ATM.Models;
using ATM.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        //[Route("AddAccount")]
        //[HttpPost]
        //public async Task<ActionResult<Account>> AddAccount(Account account)
        //{
        //    try
        //    {
        //        if (account == null)
        //        {
        //            return Problem("Account provided is null.");
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred" + ex.Message);
        //    }
        //}


        [HttpPatch]
        public async Task<ActionResult<Account>> ChangePin([FromBody] string newPin, [FromBody] int accountID)
        {

            try
            {
                Account acc = await _accountService.ChangePin(accountID, newPin);
                return Ok(acc);
            }
            catch (Exception ex) 
            {
                return Problem(ex.Message);
            }


        }


    }
}
