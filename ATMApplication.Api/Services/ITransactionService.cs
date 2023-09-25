using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;

namespace ATMApplication.Api.Services
{
	public interface ITransactionService
	{

        public Task<int> ProcessTransaction(TransactionDto transactionDto);

        public Task<int> AddTransfer(TransactionDto transactionDto);

        public Task<int> AddDeposit(TransactionDto transactionDto);

        public Task<int> AddWithdrawal(TransactionDto transactionDto);

        public Task<List<Transaction>> GetTransactionsByAccount(int id);

    }
}
