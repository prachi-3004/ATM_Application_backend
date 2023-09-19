using ATM.Models;

namespace ATM.Services
{
    public interface ITransactionService
    {
        public Task<int> ProcessTransaction(TransactionRequest request);

        public Task<int> AddTransfer(TransactionRequest request);

        public Task<int> AddDeposit(TransactionRequest request);

        public Task<int> AddWithdrawal(TransactionRequest request);
    }
}
