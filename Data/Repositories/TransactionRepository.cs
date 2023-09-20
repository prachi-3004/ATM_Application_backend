using ATM.Models;
using Microsoft.EntityFrameworkCore;

namespace ATM.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ATMContext _context;

        public TransactionRepository( ATMContext context )
        {
            _context = context;
        }

        public async Task<int> AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetTransactionByID(int id)
        {
            var transaction = await _context.Transactions.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (transaction == null)
            {
                throw new Exception("Transaction not found!");
            }
            return transaction;
        }

        public async Task<List<Transaction>> GetTransactionsByAccount(int id)
        {
            var transactions = await _context.Transactions.Where(t =>  t.AccountId == id).ToListAsync();
            if (transactions == null)
            {
                throw new Exception("Account not found!");
            }
            return transactions;
        }

        public async Task<int> SaveDBChanges()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
