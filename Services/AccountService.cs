using ATM.Data.Repositories;
using ATM.Models;

namespace ATM.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository=accountRepository;
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
