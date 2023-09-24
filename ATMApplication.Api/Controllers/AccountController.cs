using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;
using ATMApplication.Api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATMApplication.Api.Controllers
{
	
	[Route("api/accounts")]
	[ApiController]
	class AccountController : ControllerBase
	{
		
		public IAccountRepository _accountRepository;
		public IMapper _mapper;
		
		
		public AccountController(IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
		}
		
		
		[Authorize(Roles = "ADMIN")]
		[HttpPost]
		public async Task<ActionResult<Account>> CreateAccount(CreateAccountDto accountDto)
		{
			Account account = _mapper.Map<Account>(accountDto);
			return Ok(_accountRepository.CreateAccount(account));
		}
		
		
		
		
	}
	
	
	
	
}