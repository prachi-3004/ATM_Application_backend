using ATM.Data.Repositories;
using ATM.Models;
using Microsoft.Identity.Client;

namespace ATM.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AccountService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<int> AddAccount(Account account)
        {
            return await _accountRepository.AddAccount(account);
        }

        public async Task<Account> GetAccountByID(int id)
        {
            return await _accountRepository.GetAccountByID(id);
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            return await _accountRepository.GetAllAccounts();
        }

        public async Task<List<Transaction>> MiniStatementByAccount(int id)
        {
            var account = await _accountRepository.GetAccountByID(id);
            return account.Transactions.ToList();
        }

        public async Task<Account> ChangePin(int accountID, string newPin)
        {
            Account acc = await _accountRepository.GetAccountByID(accountID);
            acc.Pin = newPin;
            await _accountRepository.UpdateAccount(acc);
            return acc;
        }

        public async Task<int> CloseAccount(int id)
        {
            Account account = await _accountRepository.GetAccountByID(id);
            account.Type = "Closed";
            return await _accountRepository.UpdateAccount(account);
        }
    }
}
