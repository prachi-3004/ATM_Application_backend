using ATM.Models;
using Microsoft.EntityFrameworkCore;

namespace ATM.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ATMContext _context;
        public AccountRepository( ATMContext context )
        {
            _context = context;
        }

        public async Task<Account> AddAccount(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> GetAccountByID(int id)
        {
            var account = await _context.Accounts.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (account == null)
            {
                throw new Exception("Account not found!");
            }
            return account;
        }

        public async Task<List<Account>> GetAccountsByCustomer(int id)
        {
            var customer = await _context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new Exception("Customer not found!");
            }
            return customer.Accounts.ToList();
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<int> UpdateAccount(Account updated_account)
        {
            var account = await _context.Accounts.Where(a => a.Id == updated_account.Id).FirstOrDefaultAsync();
            if (account == null)
            {
                throw new Exception("Account not found!");
            }
            //account.CustomerId = updated_account.CustomerId;
            account.Type = updated_account.Type;
            //account.DateOfCreation = updated_account.DateOfCreation;
            account.CardNumber = updated_account.CardNumber;
            account.Pin = updated_account.Pin;
            account.Balance = updated_account.Balance;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> GetAccountBalance(int id)
        {
            var account = await _context.Accounts.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (account == null)
            {
                throw new Exception("Account not found!");
            }
            return account.Balance;
        }

        public async Task<int> ChangeAccountBalance(int id, int amount)
        {
            var account = await _context.Accounts.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (account == null)
            {
                throw new Exception("Account not found!");
            }
            account.Balance += amount;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAccount(int id)
        {
            var account = await _context.Accounts.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (account == null)
            {
                throw new Exception("Account not found!");
            }
            _context.Accounts.Remove(account);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> SaveDBChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
