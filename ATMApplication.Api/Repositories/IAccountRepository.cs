using ATMApplication.Api.Models;

namespace ATMApplication.Api.Repositories
{
	public interface IAccountRepository
	{
		
		public Task<Account> CreateAccount(Account account);
		
		
		public Task<Account> GetAccountById(int id);
		
		
		public Task<List<Account>> GetAccountsByCustomerId(int customerId);
		
		
		public Task<int> IncreaseBalance(int accountId, int incrementAmount);
		
		
		public Task<int> DecreaseBalance(int accountId, int decrementAmount);
		
		
	}
	
}
