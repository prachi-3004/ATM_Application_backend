using ATMApplication.Api.Models;
using ATMApplication.Api.Enums;

namespace ATMApplication.Api.Repositories
{
	public interface ITransactionRepository
	{
		
		public Task<Transaction> Deposit(int accountId, int amount);
		
		public Task<Transaction> Withdraw(int accountId, int amount);
		
		public Task<List<Transaction>> Transfer(int fromAccountId, int toAccountId, int amount);
		
		
		public void ChangeTransactionStatus(int transactionId, TransactionStatus transactionStatus);
		
		
	}
}
