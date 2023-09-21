using ATM.Models;

namespace ATM.Services
{
    public interface IAccountService
    {
        public Task<int> AddAccount(Account account);

        public Task<List<Account>> GetAccountsByCustomer(int id);

        public Task<List<Account>> GetAllAccounts();

        public Task<Account> ChangePin(int accountId, string newPin);

        public Task<int> CloseAccount(int id);

        public Task<List<Account>> GetAccountByID(int id);

        //public Task<List<Account>> GetAccountsByCustomer(int id);
    }
}
