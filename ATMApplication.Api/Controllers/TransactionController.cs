using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;
using ATMApplication.Api.Repositories;
using ATMApplication.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATMApplication.Api.Controllers
{
	
	[Route("api/transactions")]
	[ApiController]
	class TransactionController : ControllerBase
	{
		
		public ITransactionService _transactionService;
		public IMapper _mapper;
		
		
		public TransactionController(ITransactionService transactionService)
		{
			_transactionService = transactionService;
		}
		
		
	// 	[Authorize(Roles = "customer")]
	// 	[HttpPost("deposit")]
	// 	public async Task<ActionResult<Transaction>> Deposit()
	// 	{
			
			
			
	// 	}
		
		
	// 	[Authorize(Roles = "customer")]
	// 	[HttpPost("withdraw")]
	// 	public async Task<ActionResult<Transaction>> Withdraw()
	// 	{
			
			
			
	// 	}
		
		
	// 	[Authorize(Roles = "customer")]
	// 	[HttpPost("transfer")]
	// 	public async Task<ActionResult<Transaction>> Transfer()
	// 	{
			
			
			
	// 	}
		
	}
	
	
	
	
}