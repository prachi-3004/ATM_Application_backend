using ATMApplication.Api.Models;
using ATMApplication.Api.Enums;

namespace ATMApplication.Api.Repositories
{
	public interface ITransactionRepository
	{

		public Task<int> AddTransaction(Transaction transaction);

		public Task<List<Transaction>> GetAllTransactions();

        public Task<Transaction> GetTransactionByID(int id);

        public Task<List<Transaction>> GetTransactionsByAccount(int id);

        public Task<int> SaveDBChanges();

    }
}
