using ATM.Models;

namespace ATM.Data.Repositories
{
    public interface ITransactionRepository
    {
        public Task<int> AddTransaction(Transaction transaction);

        public Task<Transaction> GetTransactionByID(int id);

        public Task<List<Transaction>> GetAllTransactions();

        public Task<List<Transaction>> GetTransactionsByAccount(int id);
    }
}
