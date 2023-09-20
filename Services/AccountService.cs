using ATM.Data.Repositories;
using ATM.Models;
using Microsoft.Identity.Client;

namespace ATM.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICustomerRepository _customerRepository;

        public AccountService(IAccountRepository accountRepository, ITransactionRepository transactionRepository, ICustomerRepository customerRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _customerRepository = customerRepository;
        }

        public async Task<int> AddAccount(Account account)
        {
            var added_account = await _accountRepository.AddAccount(account);
            var customer = await _customerRepository.GetCustomerByID(added_account.CustomerId);
            added_account.Customer = customer;
            customer.Accounts.Add(added_account);
            return await _accountRepository.SaveDBChanges();
        }

        public async Task<List<Account>> GetAccountsByCustomer(int id)
        {
            return await _accountRepository.GetAccountsByCustomer(id);
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
