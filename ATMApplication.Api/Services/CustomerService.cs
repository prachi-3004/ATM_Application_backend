using ATMApplication.Api.Dto;
using ATMApplication.Api.Models;
using ATMApplication.Api.Repositories;
using ATMApplication.Api.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ATMApplication.Api.Services
{
	public class CustomerService : ICustomerService
	{
		
		private readonly ICustomerRepository _customerRepository;
		private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
		private readonly PasswordHasher<Customer> _passwordHasher;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IAccountRepository accountRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _passwordHasher = new PasswordHasher<Customer>();
            _accountRepository = accountRepository;
        }

        public async Task<int> CreateCustomer(CustomerDto customerDto)
		{
			Customer customer = _mapper.Map<Customer>(customerDto);
			customer.Password = _passwordHasher.HashPassword(customer, customer.Password);
            return await _customerRepository.CreateCustomer(customer);
		}

		public async Task<Customer> GetCustomerByEmail(string email, TokenClaims tokenClaims)
		{
			if(email == tokenClaims.Email || tokenClaims.Role == "ADMIN")
			{
				return await _customerRepository.GetCustomerByEmail(email);
			}
			throw new Exception("Invalid Token");
		}

		public async Task<Customer> GetCustomerByID(int id, TokenClaims tokenClaims)
		{
			if(id.ToString() == tokenClaims.UserId || tokenClaims.Role == "ADMIN")
			{
				return await _customerRepository.GetCustomerByID(id);
			}
			throw new Exception("Invalid Token");
		}

        public async Task<List<Customer>> GetAllCustomers()
		{
			List<Customer> customers = new List<Customer>();
			foreach (Customer customer in await _customerRepository.GetAllCustomers())
			{
				if(customer.Status == CustomerStatus.Active)
				{
					customers.Add(customer);
				}
			}
			return customers;
		}

		public async Task<int> UpdateCustomer(int id, CustomerDto customerDto, TokenClaims tokenClaims)
		{
			if(id.ToString() == tokenClaims.UserId)
			{
				Customer customer = await _customerRepository.GetCustomerByID(id);
                Customer updated_customer = _mapper.Map<Customer>(customerDto);
				updated_customer.Id = customer.Id;
                updated_customer.Password = _passwordHasher.HashPassword(updated_customer, updated_customer.Password);
                return await _customerRepository.UpdateCustomer(updated_customer);
			}
			else if (tokenClaims.Role == "ADMIN")
			{
                Customer customer = await _customerRepository.GetCustomerByID(id);
                Customer updated_customer = _mapper.Map<Customer>(customerDto);
                updated_customer.Id = customer.Id;
				updated_customer.GovernmentId = customer.GovernmentId;
				updated_customer.Password = customer.Password;
                return await _customerRepository.UpdateCustomer(updated_customer);
            }
			throw new Exception("Invalid Token");
		}

		public async Task<int> DeleteCustomer(string email)
		{
			Customer customer = await _customerRepository.GetCustomerByEmail(email);
			var accounts = await _accountRepository.GetAccountsByCustomerID(customer.Id);
			foreach (var account in accounts)
			{
				if (account.Balance == 0)
				{
					throw new Exception($"Account with Id: {account.Id} can't be disabled!");
				}
				await _accountRepository.DisableAccount(account.Id);
			}
			return await _customerRepository.DeleteCustomer(customer.Id);
		}

    }
}
