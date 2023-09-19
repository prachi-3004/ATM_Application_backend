using ATM.Models;

namespace ATM.Services
{
    public interface IAccountService
    {

        public Task<Account> ChangePin(int accountId, string newPin)


    }
}
