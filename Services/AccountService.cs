using ATM.Data.Repositories;
using ATM.Models;

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

    }
}
