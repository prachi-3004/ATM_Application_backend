using ATM.Data.Repositories;
using ATM.Models;

namespace ATM.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<int> ProcessTransaction(TransactionRequest request)
        {
            if (request.Type == "Transfer" && request.SenderId != null && request.RecipientId != null)
            {
                return await AddTransfer(request);
            }
            else if (request.Type == "Deposit" && request.RecipientId != null)
            {
                return await AddDeposit(request);
            }
            else if (request.Type == "Withdrawal" && request.SenderId != null)
            {
                return await AddWithdrawal(request);
            }
            else
            {
                throw new ArgumentException("Request can't be processed!");
            }
            
        }

        public async Task<int> AddTransfer(TransactionRequest request)
        {
            var account = await _accountRepository.GetAccountByID((int)request.SenderId);
            var account1 = await _accountRepository.GetAccountByID((int)request.RecipientId);
            if (account.Balance >= request.Amount)
            {
                Transaction transaction = new Transaction();
                Transaction transaction1 = new Transaction();
                transaction.Type = "Transfer Debit";
                transaction1.Type = "Transfer Credit";
                transaction.AccountId = request.SenderId;
                transaction1.AccountId = request.RecipientId;
                transaction.Amount = request.Amount;
                transaction1.Amount = request.Amount;
                transaction.Time = DateTime.Now;
                transaction1.Time = DateTime.Now;
                transaction.Account = account;
                transaction1.Account = account1;
                await _transactionRepository.AddTransaction(transaction);
                await _transactionRepository.AddTransaction(transaction1);
                transaction.LinkedId = transaction1.Id;
                transaction1.LinkedId = transaction.Id;
                await _transactionRepository.SaveDBChanges();
                account.Transactions.Add(transaction);
                account1.Transactions.Add(transaction1);
                await _accountRepository.ChangeAccountBalance(account.Id, -1*request.Amount);
                return await _accountRepository.ChangeAccountBalance(account1.Id, request.Amount);
            }
            else
            {
                throw new InvalidOperationException("Balance Unavailable!");
            }
        }

        public async Task<int> AddDeposit(TransactionRequest request)
        {
            var account = await _accountRepository.GetAccountByID((int)request.RecipientId);
            Transaction transaction = new Transaction();
            transaction.Type = "Deposit Credit";
            transaction.AccountId = account.Id;
            transaction.Amount = request.Amount;
            transaction.Time = DateTime.Now;
            transaction.Account = account;
            await _transactionRepository.AddTransaction(transaction);
            account.Transactions.Add(transaction);
            return await _accountRepository.ChangeAccountBalance(account.Id, request.Amount);
        }

        public async Task<int> AddWithdrawal(TransactionRequest request)
        {
            var account = await _accountRepository.GetAccountByID((int)request.SenderId);
            if (account.Balance >= request.Amount)
            {
                Transaction transaction = new Transaction();
                transaction.Type = "Withdrawal Debit";
                transaction.AccountId = account.Id;
                transaction.Amount = request.Amount;
                transaction.Time = DateTime.Now;
                transaction.Account = account;
                await _transactionRepository.AddTransaction(transaction);
                account.Transactions.Add(transaction);
                return await _accountRepository.ChangeAccountBalance(account.Id, -1*request.Amount);
            }
            else
            {
                throw new InvalidOperationException("Balance Unavailable!");
            }
        }

    }
}
