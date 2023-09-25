using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;

namespace ATMApplication.Api.Services
{
    public interface IAccountService
    {
        public Task<int> AddAccount(AccountDto accountDto);

        public Task<List<Account>> GetAccountsByCustomerID(int id, TokenClaims tokenClaims);

        public Task<List<Account>> GetAllAccounts();

        public Task<int> DisableAccount(int id);

        public Task<Account> GetAccountByID(int id, TokenClaims tokenClaims);

        public Task<int> ChangePin(int id, string newPin, string oldPin);
    }
}
