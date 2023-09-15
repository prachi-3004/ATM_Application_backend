using ATM_banking_system.Models;

namespace ATM_banking_system.Services
{
    public interface ITransactionService
    {
        public bool AddTransaction(Transaction transaction);
    }
}
