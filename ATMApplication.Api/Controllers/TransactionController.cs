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
	class TransactionController : ControllerBase
	{
		
		private readonly ITransactionService _transactionService;
		
		public TransactionController(ITransactionService transactionService)
		{
			_transactionService = transactionService;
		}

        [Route("Add")]
        [Authorize(Roles = "customer")]
        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionDto transactionDto)
        {
            try
            {
                var result = await _transactionService.ProcessTransaction(transactionDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("MiniStatement/{id}")]
        [Authorize(Roles = "ADMIN,customer")]
        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> MiniStatement(int id)
        {
            try
            {
                var result = await _transactionService.GetTransactionsByAccount(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
	
	
	
	
}