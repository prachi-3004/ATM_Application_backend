using ATM.Models;

namespace ATM.Services
{
    public interface IAccountService
    {
        public Task<List<Account>> GetAllAccounts();

        public Task<List<Transaction>> MiniStatementByAccount(int id);

        public Task<Account> ChangePin(int accountId, string newPin);

        //public Task<List<Account>> GetAccountByID(int id);

        //public Task<List<Account>> GetAccountsByCustomer(int id);
    }
}
