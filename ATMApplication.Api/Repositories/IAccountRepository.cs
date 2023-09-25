using ATMApplication.Api.Models;

namespace ATMApplication.Api.Repositories
{
	public interface IAccountRepository
	{
		
		public Task<int> CreateAccount(Account account);
		
		public Task<Account> GetAccountByID(int id);
		
		public Task<List<Account>> GetAccountsByCustomerID(int customerId);

		public Task<List<Account>> GetAllAccounts();

		public Task<int> UpdateAccount(Account updated_account);
		
		public Task<int> DisableAccount(int accountId);

        public Task<int> ChangeBalance(int id, int amount);

		public Task<int> SaveDBChanges();

    }
	
}
