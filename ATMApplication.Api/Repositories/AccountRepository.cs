using ATMApplication.Api.DBContexts;
using ATMApplication.Api.Enums;
using ATMApplication.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMApplication.Api.Repositories
{
	public class AccountRepository : IAccountRepository
	{
		private readonly ATMContext _context;
		
		public AccountRepository(ATMContext context)
		{
			_context = context;
		}
		
		public async Task<Account> CreateAccount(Account account)
		{
			try
			{
				await _context.Accounts.AddAsync(account);
				return account;
			}
			catch (Exception ex)
			{
				throw new Exception("Something went wrong", ex);
			}
		}


		public async Task<Account> GetAccountById(int id)
		{
			Account? account =  await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
			
			if(account == null)
			{
				throw new Exception($"Account with Id: {id} not Found");
			}
			
			return account;
			
		}

		public async Task<List<Account>> GetAccountsByCustomerId(int customerId)
		{
			try
			{
				List<Account> accounts = await _context.Accounts.Where(a => a.CustomerId == customerId).ToListAsync();
				return accounts;
			}
			catch (Exception ex)
			{
				throw new Exception("Something went wrong", ex);
			}
		}


		public async Task<int> IncreaseBalance(int accountId, int incrementAmount)
		{
			Account account = await GetAccountById(accountId);
			account.Balance += incrementAmount;
			await _context.SaveChangesAsync();
			return account.Balance;
		}
		
		
		public async Task<int> DecreaseBalance(int accountId, int decrementAmount)
		{
			Account account = await GetAccountById(accountId);
			account.Balance -= decrementAmount;
			await _context.SaveChangesAsync();
			return account.Balance;
		}
		
		public async Task<int> DisableAccount(int accountId)
		{
            Account account = await GetAccountById(accountId);
			account.Status = (AccountStatus)1;
			return await _context.SaveChangesAsync();
        }

	}
	
	
}
