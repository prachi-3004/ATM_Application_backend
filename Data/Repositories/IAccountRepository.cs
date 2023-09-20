﻿using ATM.Models;

namespace ATM.Data.Repositories
{
    public interface IAccountRepository
    {
        public Task<Account> AddAccount(Account account);

        public Task<int> UpdateAccount(Account account);

        public Task<Account> GetAccountByID(int id);

        public Task<List<Account>> GetAllAccounts();

        public Task<List<Account>> GetAccountsByCustomer(int id);

        public Task<int> GetAccountBalance(int id);

        public Task<int> ChangeAccountBalance(int id, int amount);

        public Task<int> DeleteAccount(int id);

        public Task<int> SaveDBChanges();
    }
}
