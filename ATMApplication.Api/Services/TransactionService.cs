using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;
using ATMApplication.Api.Repositories;

namespace ATMApplication.Api.Services
{
	
	public class TransactionService : ITransactionService
	{
		
		private readonly ITransactionRepository _transactionRepository;
		private readonly IAccountRepository _accountRepository;
		
		
		public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
		{
			_transactionRepository = transactionRepository;
			_accountRepository = accountRepository;
		}
		
		
		// public Task<List<Transaction>> Transfer(int fromAccountId, int toAccountId, int amount, TokenClaims tokenClaims)
		// {
			
			
			
		// 	// if(user == tokenClaims.Email || tokenClaims.Role == "ADMIN")
		// 	// {
		// 	// 	return await _customerRepository.GetCustomerByEmail(email);
		// 	// }
			
			
		// }
	}
	
	
}
