using ATM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

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


    }
}
