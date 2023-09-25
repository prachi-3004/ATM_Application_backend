using ATMApplication.Api.Dto;
using ATMApplication.Api.Enums;
using ATMApplication.Api.Models;
using ATMApplication.Api.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ATMApplication.Api.Services
{
	
	public class TransactionService : ITransactionService
	{
		
		private readonly ITransactionRepository _transactionRepository;
		private readonly IAccountRepository _accountRepository;
        private readonly PasswordHasher<Account> _passwordHasher;
		
		public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
		{
			_transactionRepository = transactionRepository;
			_accountRepository = accountRepository;
		}

        public async Task<int> ProcessTransaction(TransactionDto transactionDto)
        {
            if (transactionDto.Type == "Transfer" && transactionDto.SenderId != null && transactionDto.RecipientId != null)
            {
                return await AddTransfer(transactionDto);
            }
            else if (transactionDto.Type == "Deposit" && transactionDto.RecipientId != null)
            {
                return await AddDeposit(transactionDto);
            }
            else if (transactionDto.Type == "Withdrawal" && transactionDto.SenderId != null)
            {
                return await AddWithdrawal(transactionDto);
            }
            else
            {
                throw new ArgumentException("Request can't be processed!");
            }
        }

        public async Task<int> AddTransfer(TransactionDto transactionDto)
        {
            Transaction debitTransaction = new Transaction
            (
                null,
                TransactionType.TRANSFER_DEBIT,
                TransactionStatus.FAILED,
                (int)transactionDto.SenderId,
                transactionDto.Amount,
                null
            );

            Transaction creditTransaction = new Transaction
            (
                null,
                TransactionType.TRANSFER_CREDIT,
                TransactionStatus.FAILED,
                (int)transactionDto.RecipientId,
                transactionDto.Amount,
                null
            );
            
            await _transactionRepository.AddTransaction(debitTransaction);
            await _transactionRepository.AddTransaction(creditTransaction);

            debitTransaction.TransactionId = debitTransaction.Id;
            creditTransaction.TransactionId = debitTransaction.Id;
            
            await _accountRepository.ChangeBalance((int)transactionDto.SenderId, -1 * transactionDto.Amount);
            await _accountRepository.ChangeBalance((int)transactionDto.RecipientId, transactionDto.Amount);

            return await _transactionRepository.SaveDBChanges();
        }

        public async Task<int> AddDeposit(TransactionDto transactionDto)
        {
            Transaction transaction = new Transaction
            (
                null,
                TransactionType.DEPOSIT,
                TransactionStatus.FAILED,
                (int)transactionDto.RecipientId,
                transactionDto.Amount,
                null
            );

            await _transactionRepository.AddTransaction(transaction);
            transaction.TransactionId = transaction.Id;

            return await _accountRepository.ChangeBalance((int)transactionDto.RecipientId, transactionDto.Amount);
        }

        public async Task<int> AddWithdrawal(TransactionDto transactionDto)
        {
            Transaction transaction = new Transaction
            (
                null,
                TransactionType.WITHDRAW,
                TransactionStatus.FAILED,
                (int)transactionDto.SenderId,
                transactionDto.Amount,
                null
            );

            await _transactionRepository.AddTransaction(transaction);
            transaction.TransactionId = transaction.Id;

            return await _accountRepository.ChangeBalance((int)transactionDto.SenderId, -1 * transactionDto.Amount);
        }

        public async Task<List<Transaction>> GetTransactionsByAccount(int id)
        {
            return await _transactionRepository.GetTransactionsByAccount(id);
        }

    }
	
	
}
