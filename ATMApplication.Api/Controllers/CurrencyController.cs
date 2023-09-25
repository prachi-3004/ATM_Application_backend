using Microsoft.AspNetCore.Mvc;
using ATMApplication.Api.Services;

namespace ATMApplication.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [Route("GetAll")]
        [HttpGet]
        public ActionResult<List<string>> GetCurrencies()
        {
            try
            {
                return Ok(_currencyService.GetCurrencies());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("GetRate/{code}")]
        [HttpGet]
        public ActionResult<decimal> GetRate(string code)
        {
            try
            {
                return Ok(_currencyService.GetRate(code));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
