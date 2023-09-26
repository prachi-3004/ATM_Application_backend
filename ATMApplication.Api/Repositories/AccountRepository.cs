using ATMApplication.Api.Data;
using ATMApplication.Api.Enums;
using ATMApplication.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ATMApplication.Api.Repositories
{
	public class AccountRepository : IAccountRepository
	{
		private readonly ATMContext _context;
		
		public AccountRepository(ATMContext context)
		{
			_context = context;
		}
		
		public async Task<int> CreateAccount(Account account)
		{
			try
			{
				await _context.Accounts.AddAsync(account);
				return await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception("Something went wrong", ex);
			}
		}


		public async Task<Account> GetAccountByID(int id)
		{
			Account? account =  await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
			
			if(account == null || account.Status != AccountStatus.ACTIVE)
			{
				throw new Exception($"Account with Id: {id} not found");
			}
			
			return account;
		}

		public async Task<List<Account>> GetAccountsByCustomerID(int customerId)
		{
			try
			{
				List<Account> accounts = await _context.Accounts.Where(a => a.CustomerId == customerId).ToListAsync();
				return accounts;
			}
			catch (Exception ex)
			{
				throw new Exception("Request failed!", ex);
			}
		}

		public async Task<List<Account>> GetAllAccounts()
		{
			return await _context.Accounts.ToListAsync();
		}
		
		public async Task<int> UpdateAccount(Account updated_account)
		{
            try
            {
                var account = await GetAccountByID(updated_account.Id);
                if (account == null)
                {
                    throw new Exception($"Account with ID: {updated_account.Id} not Found");
                }
				account.Pin = updated_account.Pin;
                //account.Type = updated_account.Type;
				//account.Currency = updated_account.Currency;
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update customer!");
            }
        }

		public async Task<int> DisableAccount(int accountId)
		{
            Account account = await GetAccountByID(accountId);
			account.Status = (AccountStatus)1;
			account.DeletedAt = DateTime.UtcNow;
			return await _context.SaveChangesAsync();
        }

		public async Task<int> ChangeBalance(int id, int amount)
		{
			Account account = await GetAccountByID(id);
			account.Balance += amount;
			return await _context.SaveChangesAsync();
		}

		public async Task<int> SaveDBChanges()
		{
			return await _context.SaveChangesAsync();
		}

	}
	
	
}
