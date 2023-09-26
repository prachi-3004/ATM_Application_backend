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
            _passwordHasher = new PasswordHasher<Account>();
		}

        public async Task<int> ProcessTransaction(TransactionDto transactionDto)
        {
            int? verify_id = null;
            if(transactionDto.SenderId!=null)
            {
                verify_id = transactionDto.SenderId;
            }
            else if(transactionDto.RecipientId!=null)
            {
                verify_id = transactionDto.RecipientId;
            }
            else
            {
                throw new ArgumentException("Invalid request!");
            }
            Account account = await _accountRepository.GetAccountByID((int)verify_id);
            if(_passwordHasher.VerifyHashedPassword(account, account.Pin, transactionDto.Pin) == 0)
            {
                throw new Exception("Invalid PIN!");
            }
            if (transactionDto.Type == "Transfer" && transactionDto.SenderId != null && transactionDto.RecipientId != null && transactionDto.SenderId!=transactionDto.RecipientId)
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
            throw new ArgumentException("Request can't be processed!");
        }

        public async Task<int> AddTransfer(TransactionDto transactionDto)
        {
            Transaction debitTransaction = new Transaction
            (
                TransactionType.TRANSFER_DEBIT,
                (int)transactionDto.SenderId,
                transactionDto.Amount
            );

            Transaction creditTransaction = new Transaction
            (
                TransactionType.TRANSFER_CREDIT,
                (int)transactionDto.RecipientId,
                transactionDto.Amount
            );

            await _accountRepository.ChangeBalance((int)transactionDto.SenderId, -1 * transactionDto.Amount);
            await _accountRepository.ChangeBalance((int)transactionDto.RecipientId, transactionDto.Amount);

            await _transactionRepository.AddTransaction(debitTransaction);
            await _transactionRepository.AddTransaction(creditTransaction);

            debitTransaction.TransactionId = debitTransaction.Id;
            creditTransaction.TransactionId = debitTransaction.Id;

            return await _transactionRepository.SaveDBChanges();
        }

        public async Task<int> AddDeposit(TransactionDto transactionDto)
        {
            Transaction transaction = new Transaction
            (
                TransactionType.DEPOSIT,
                (int)transactionDto.RecipientId,
                transactionDto.Amount
            );

            await _accountRepository.ChangeBalance((int)transactionDto.RecipientId, transactionDto.Amount);
            
            await _transactionRepository.AddTransaction(transaction);
            transaction.TransactionId = transaction.Id;

            return await _transactionRepository.SaveDBChanges();
        }

        public async Task<int> AddWithdrawal(TransactionDto transactionDto)
        {
            Transaction transaction = new Transaction
            (
                TransactionType.WITHDRAW,
                (int)transactionDto.SenderId,
                transactionDto.Amount
            );
            
            await _accountRepository.ChangeBalance((int)transactionDto.SenderId, -1 * transactionDto.Amount);

            await _transactionRepository.AddTransaction(transaction);
            transaction.TransactionId = transaction.Id;

            return await _transactionRepository.SaveDBChanges();
        }

        public async Task<List<Transaction>> GetTransactionsByAccount(int id)
        {
            return await _transactionRepository.GetTransactionsByAccount(id);
        }

    }
	
	
}
