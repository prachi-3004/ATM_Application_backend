using ATMApplication.Api.Models;
using ATMApplication.Api.Repositories;
using ATMApplication.Api.Dto;
using ATMApplication.Api.Enums;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace ATMApplication.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly PasswordHasher<Account> _passwordHasher;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, ITransactionRepository transactionRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _customerRepository = customerRepository;
            _passwordHasher = new PasswordHasher<Account>();
            _mapper = mapper;
        }

        public async Task<int> AddAccount(AccountDto accountDto)
        {
            Account account = new Account(accountDto.CustomerId, accountDto.Balance, accountDto.Pin, (AccountType)Enum.Parse(typeof(AccountType), accountDto.Type));
            account.Pin = _passwordHasher.HashPassword(account, account.Pin);
            await _accountRepository.CreateAccount(account);
            account.CardNumber = (1000000000000000 - account.Id).ToString();
            return await _accountRepository.SaveDBChanges();
        }

        public async Task<List<Account>> GetAccountsByCustomerID(int id, TokenClaims tokenClaims)
        {
            if (id.ToString() == tokenClaims.UserId || tokenClaims.Role == "ADMIN")
            {
                List<Account> accounts = new List<Account>();
                foreach(Account account in await _accountRepository.GetAccountsByCustomerID(id))
                {
                    if(account.Status == AccountStatus.ACTIVE)
                    {
                        accounts.Add(account);
                    }
                }
                return accounts;
            }
            throw new Exception("Invalid token!");
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();
            foreach (Account account in await _accountRepository.GetAllAccounts())
            {
                if (account.Status == AccountStatus.ACTIVE)
                {
                    accounts.Add(account);
                }
            }
            return accounts;
        }

        public async Task<int> DisableAccount(int id)
        {
            return await _accountRepository.DisableAccount(id);
        }

        public async Task<Account> GetAccountByID(int id, TokenClaims tokenClaims)
        {
            var account = await _accountRepository.GetAccountByID(id);
            if (account.CustomerId.ToString() == tokenClaims.UserId || tokenClaims.Role == "ADMIN")
            {
                return account;
            }
            throw new Exception("Invalid token!");
        }

        public async Task<int> ChangePin(int id, string newPin, string oldPin)
        {
            var account = await _accountRepository.GetAccountByID(id);
            if (_passwordHasher.VerifyHashedPassword(account, account.Pin, oldPin)==0)
            {
                throw new Exception("Invalid PIN!");
            }
            account.Pin = newPin;
            account.Pin = _passwordHasher.HashPassword(account, account.Pin);
            return await _accountRepository.SaveDBChanges();
        }
    }
}