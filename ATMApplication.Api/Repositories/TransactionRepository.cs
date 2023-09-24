using ATMApplication.Api.DBContexts;
using ATMApplication.Api.Enums;
using ATMApplication.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMApplication.Api.Repositories
{
	public class TransactionRepository : ITransactionRepository
	{
		
		private readonly ATMContext _context;
		
		public TransactionRepository(ATMContext context)
		{
			_context = context;
		}
		
		
		
		public async void ChangeTransactionStatus(int transactionId, TransactionStatus transactionStatus)
		{
			var transactions = await _context.Transactions.Where(t => t.TransactionId == transactionId).ToListAsync();
			foreach (var transaction in transactions)
			{
				transaction.Status = transactionStatus;
			}
		}

		public async Task<Transaction> Deposit(int accountId, int amount)
		{
			Transaction transaction = new Transaction
			(
				TransactionType.DEPOSIT,
				TransactionStatus.FAILED,
				accountId,
				amount,
				null,
				null
			);
			
			await _context.Transactions.AddAsync(transaction);
			
			return transaction;
			
		}

		public async Task<List<Transaction>> Transfer(int fromAccountId, int toAccountId, int amount)
		{
			
			Transaction debitTransaction = new Transaction
			(
				TransactionType.TRANSFERDEBIT,
				TransactionStatus.FAILED,
				fromAccountId,
				amount,
				null,
				null
			);
			
			Transaction creditTransaction = new Transaction
			(
				TransactionType.TRANSFERCREDIT,
				TransactionStatus.FAILED,
				toAccountId,
				amount,
				null,
				null
			);
			
			await _context.Transactions.AddAsync(debitTransaction);
			await _context.Transactions.AddAsync(creditTransaction);
			
			debitTransaction.TransactionId = debitTransaction.Id;
			creditTransaction.TransactionId = debitTransaction.Id;
			
			await _context.SaveChangesAsync();
			
			return new List<Transaction>() {debitTransaction, creditTransaction};
			
		}

		public async Task<Transaction> Withdraw(int accountId, int amount)
		{
			Transaction transaction = new Transaction
			(
				TransactionType.WITHDRAW,
				TransactionStatus.FAILED,
				accountId,
				amount,
				null,
				null
			);
			
			await _context.Transactions.AddAsync(transaction);
			
			return transaction;

		}
	}
}
